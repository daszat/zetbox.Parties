@echo off
echo ********************************************************************************
echo Run the development server.
echo ********************************************************************************

cd bin\debug
Kistl.Server.Service.exe
cd ..\..

pause