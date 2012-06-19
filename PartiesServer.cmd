@echo off
echo ********************************************************************************
echo Run the development server.
echo ********************************************************************************

cd bin\debug
Zetbox.Server.Service.exe
cd ..\..

pause