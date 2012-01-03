$ErrorActionPreference='Stop'

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
	Write-Host $msg
	exit 1
}

if ($args.Length -ne 3) {
	die("Usage: deploy_binaries.ps1 AppConfigSourceFolder ConfigSourceFolder BinarySourceFolder")
}

$APPCONFSRCDIR = $args[0]
$CONFSRCDIR = $args[1]
$DESTDIR = $args[2]

if (![System.IO.Directory]::Exists($DESTDIR)) {
	die("Destination folder ("+$DESTDIR+") does not exist")
}

Write-Host "Deploying binaries"

$CCNET_REPO=Get-Location

$SOURCEDIR="$CCNET_REPO-artifacts\Latest\bin\Debug"
$SOURCEHTTPDIR="$CCNET_REPO-artifacts\Latest\bin\Debug\HttpService"
$SOURCEHTTPFILESDIR="$CCNET_REPO\Libs\Kistl\HttpService"

$MODULES="App.Parties", "App.ZBox", "Core", "Core.Generated", "EF", "EF.Generated", "WPF", "Microsoft"
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

	foreach($m in "Common", $component) {
		foreach($i in $MODULES) {
			$src = "$SOURCEDIR\$m\$i"
			if(Test-Path $src) {
				save-mkdir $DESTDIR\bin\$component\$m\$i
				cp  -force -recurse $src\* $DESTDIR\bin\$component\$m\$i | Out-Null
				
				if($component -eq "Server") {
					# only deploy server to inetpub
                    save-mkdir $DESTDIR\inetpub\$m\$i
                    cp -force -recurse $src\* $DESTDIR\inetpub\$m\$i | Out-Null
				}
			}
		}
	}
}

# fetch bootstrapper
"Fetching Bootstapper" | Out-Host
save-rm $DESTDIR\bin\Bootstrapper, $DESTDIR\inetpub\Bootstrapper
save-mkdir $DESTDIR\bin\Bootstrapper, $DESTDIR\inetpub\Bootstrapper
cp $SOURCEDIR\Bootstrapper\* $DESTDIR\bin\Bootstrapper -Recurse -Force
cp $SOURCEDIR\Bootstrapper\*.exe $DESTDIR\inetpub\Bootstrapper -Recurse -Force

# fetch http service
"Fetching HTTP Service" | Out-Host
save-mkdir $DESTDIR\inetpub\
rm $DESTDIR\inetpub\* -include bin,Site.Master*,Global.asax*,*.aspx*,*.svc*,App_* -Recurse -Force
cp $SOURCEHTTPDIR\* $DESTDIR\inetpub\ -Recurse -Force
cp $SOURCEHTTPFILESDIR\* $DESTDIR\inetpub\ -Include Site.Master,Global.asax,*.aspx,*.svc,App_* -Recurse -Force

# splice in our configs
save-mkdir $DESTDIR\Configs
cp -Force -Recurse $CONFSRCDIR\* $DESTDIR\Configs | Out-Null
cp $APPCONFSRCDIR\Web.config $DESTDIR\inetpub\Web.config -Force
foreach($appconfig in get-childitem $APPCONFSRCDIR -filter *.config) {
        $basename=[System.IO.Path]::GetFilenameWithoutExtension($appconfig);
        foreach($f in get-childitem $DESTDIR $basename -recurse) {
			cp $appconfig.FullName ($f.Directory.FullName + "\" + $basename + ".config") -Force
		}
}
