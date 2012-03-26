@echo off
"%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe" /t:BeforeBuild .\Kistl.Parties.Common\Kistl.Parties.Common.csproj /v:minimal
echo Done