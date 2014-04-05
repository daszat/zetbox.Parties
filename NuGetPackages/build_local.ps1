# script for test-building local packages
# execute this in bin\Debug

$git_output = & git log --decorate=full --simplify-by-decoration --pretty=oneline develop

foreach ($line in $git_output) {
	if ($line -Match "tag: jenkins/([^-)]*)") {
		$version = $Matches[1] + "-zdev" + (Get-Date -format yyyyMMddHHmmss)
		break
	}
}

ls "*.nuspec" | %{
	"Preparing $_" | Out-Host
	(gc $_) -Replace '\$version\$', $version | Out-File $_
	..\..\.nuget\nuget.exe pack $_.FullName
}
