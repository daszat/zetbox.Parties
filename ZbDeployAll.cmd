@echo off
echo ********************************************************************************
echo Deploys changes in the basic modules into the database.
echo Changes to the object model are generated.
echo Use this to apply upstream changes.
echo ********************************************************************************

set fallbackconfig=
set config=

if .%1. == .. GOTO GOON
set fallbackconfig=%1
set config=%1

if .%2. == .. GOTO GOON
set config=%2

:GOON

call "ZbInstall.cmd" %fallbackconfig%

cd bin\Debug

Zetbox.Server.Service.exe %fallbackconfig% -deploy-update -generate -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

cd ..\..

msbuild zetbox.Parties.sln
IF ERRORLEVEL 1 GOTO FAIL2

cd bin\Debug

Zetbox.Server.Service.exe %config% -import Data\Workflow.Data.xml
IF ERRORLEVEL 1 GOTO FAIL


echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
cd ..\..
GOTO EOF

:FAIL
cd ..\..
:Fail2
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Deploy
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
