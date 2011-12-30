
function save-rm($path) {
	foreach($p in $path) {
		if(Test-Path $p) { rm $p -Recurse -Force | Out-Null }
	}
}

function save-mkdir($path) {
	foreach($p in $path) {
		if(!(Test-Path $p)) { mkdir $p | Out-Null }
	}
}

function die($msg) {
	Write-Host "Please pass the destination folder as argument"
	exit 1
}

if ($args.Length -ne 1) {
	die("Please pass the destination folder as argument")
}

$DESTDIR = $args[0]

if (![Directory]::Exists($DESTDIR)) {
	die("Destination folder ("+$DESTDIR+") does not exist")
}

Write-Host "Deploying binaries"

$CCNET_REPO=Get-Location

$SOURCEDIR="$CCNET_REPO-artifacts\Latest\bin\Debug"
$SOURCEHTTPDIR="$CCNET_REPO-artifacts\Latest\bin\Debug\HttpService"
$SOURCEHTTPFILESDIR="$CCNET_REPO\Libs\Kistl.Server.HttpService"

$MODULES="App.ZBox", "Core", "Core.Generated", "EF", "EF.Generated", "WPF", "Microsoft"
$EXE = @{"Server"="Kistl.Server.Service.exe"; "Client" = "Kistl.Client.WPF.exe" }

# Create path structure
save-rm $DESTDIR\bin, $DESTDIR\inetpub\bin
save-mkdir $DESTDIR\bin, $DESTDIR\inetpub\bin, $DESTDIR\logs, $DESTDIR\Configs, $DESTDIR\AppConfigs

# Fetch binaries
save-mkdir $DESTDIR\inetpub\Server
foreach($component in "Client", "Server") {
	"Fetching " + $component | Out-Host
	save-rm $DESTDIR\bin\$component, $DESTDIR\inetpub\$component 
	save-mkdir $DESTDIR\bin\$component 
	
	# Copy exe
	$e = $EXE.get_Item($component)
	cp $SOURCEDIR\$e $DESTDIR\bin\$component

	# Copy template config
	cp $SOURCEDIR\$e.config $DESTDIR\AppConfigs\$e.config.install
	# if not exist - rename
	if(!(Test-Path $DESTDIR\AppConfigs\$e.config)) { cp $DESTDIR\AppConfigs\$e.config.install $DESTDIR\AppConfigs\$e.config } 
	
	foreach($m in "Common", $component) {
		foreach($i in $MODULES) {
			$src = "$SOURCEDIR\$m\$i"
			if(Test-Path $src) {
				save-mkdir $DESTDIR\bin\$component\$m\$i
				cp $src\* $DESTDIR\bin\$component\$m\$i -Recurse | Out-Null
				
				if($component -eq "Server") {
					# only deploy server to inetpub
                    save-mkdir $DESTDIR\inetpub\$m\$i
                    cp $src\* $DESTDIR\inetpub\$m\$i | Out-Null
				}
			}
		}
	}
}

# fetch bootstrapper
"Fetching Bootstapper" | Out-Host
save-rm $DESTDIR\bin\Bootstrapper, $DESTDIR\inetpub\Bootstrapper
save-mkdir $DESTDIR\bin\Bootstrapper, $DESTDIR\inetpub\Bootstrapper
cp $SOURCEDIR\Bootstrapper\* $DESTDIR\bin\Bootstrapper -Recurse
cp $SOURCEDIR\Bootstrapper\*.exe $DESTDIR\inetpub\Bootstrapper -Recurse

# fetch http service
"Fetching HTTP Service" | Out-Host
rm $DESTDIR\inetpub\* -include bin,Site.Master*,Global.asax*,*.aspx*,*.svc*,App_* -Recurse -Force
save-mkdir $DESTDIR\inetpub\bin
cp $SOURCEHTTPDIR\* $DESTDIR\inetpub\bin -Recurse
cp $SOURCEHTTPFILESDIR\* $DESTDIR\inetpub\ -Include Site.Master,Global.asax,*.aspx,*.svc,App_* -Recurse
cp $SOURCEHTTPFILESDIR\Web.config $DESTDIR\inetpub\Web.config.install

# splice in our app-configs
foreach($appconfig in get-childitem $CCNET_REPO\Configs\$Env:zenv\AppConfigs -filter *.config) {
        $basename=[System.IO.Path]::GetFilenameWithoutExtension($appconfig);
        foreach($f in get-childitem $DESTDIR $basename -recurse) {
			cp $appconfig.FullName ($f.Directory.FullName + "\" + $basename + ".config")
		}
}
