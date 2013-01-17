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

Zetbox.Cli.exe %config% -fallback -deploy-update -generate -syncidentities
IF ERRORLEVEL 1 GOTO FAIL

cd ..\..

msbuild zetbox.Parties.sln
IF ERRORLEVEL 1 GOTO FAIL2

cd bin\Debug

Zetbox.Cli.exe %config% -import Data\Workflow.Data.xml -import ..\..\Data\Parties.xml -import ..\..\Data\Accounting.Data.xml -import ..\..\Data\Invoicing.Data.xml -import ..\..\Data\Invoicing.Workflow.xml
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
