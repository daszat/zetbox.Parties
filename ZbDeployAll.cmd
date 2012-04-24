@echo off
echo ********************************************************************************
echo Deploys changes in the basic modules into the database.
echo Changes to the object model are generated.
echo Use this to apply upstream changes.
echo ********************************************************************************

set config=

if .%1. == .. GOTO GOON

set config=%1

:GOON

call "ZbInstall.cmd" %config%

cd bin\Debug

Kistl.Server.Service.exe %config% -deploy-update -generate
IF ERRORLEVEL 1 GOTO FAIL

Kistl.Server.Service.exe %config% -import Data\Invoicing.Data.xml -import Data\Accounting.Data.xml -import Data\Parties.xml
IF ERRORLEVEL 1 GOTO FAIL

echo ********************************************************************************
echo ************************************ Success ***********************************
echo ********************************************************************************
cd ..\..
GOTO EOF

:FAIL
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX FAIL XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
echo                                  Aborting Deploy
rem return error without closing parent shell
cd ..\..
echo A | choice /c:A /n

:EOF
