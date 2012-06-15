@echo off
echo ********************************************************************************
echo Publish the basic modules for committing and deployment.
echo This updates the Modules and generated code in the source directory.
echo Use this to publish local changes in the basic modules.
echo ********************************************************************************

set config=

if .%1. == .. GOTO GOON

set config=%1

:GOON

cd bin\debug

Zetbox.Server.Service.exe %config% -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

rem publish schema data for parties project
Zetbox.Server.Service.exe %config% -publish ..\..\Modules\Parties.xml -ownermodules Parties;Invoicing;Accounting
IF ERRORLEVEL 1 GOTO FAIL

rem export Invoicing Module data
Zetbox.Server.Service.exe %config% -export ..\..\Data\Invoicing.Data.xml -schemamodules Invoicing -ownermodules Invoicing
IF ERRORLEVEL 1 GOTO FAIL

rem export Accounting Module data
Zetbox.Server.Service.exe %config% -export ..\..\Data\Accounting.Data.xml -schemamodules Accounting -ownermodules Accounting
IF ERRORLEVEL 1 GOTO FAIL

rem export test data
Zetbox.Server.Service.exe %config% -export ..\..\Data\Parties.xml -schemamodules Parties;Invoicing;Accounting
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
echo                                Aborting Publish
rem return error without closing parent shell
cd ..\..
echo A | choice /c:A /n

:EOF
pause
