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

cd bin\Debug

Zetbox.Cli.exe %config% -fallback -generate -updatedeployedschema -repairschema
IF ERRORLEVEL 1 GOTO FAIL

Zetbox.Cli.exe %configs% -generate-resources=Parties;Invoicing;Accounting;Products
IF ERRORLEVEL 1 GOTO FAIL

rem *********** Assets ***********
xcopy /s /y ..\CodeGen\Assets\*.* ..\..\Zetbox.Parties.Assets


rem publish schema data for parties project
Zetbox.Cli.exe %config% -publish ..\..\Modules\Parties.xml -ownermodules Parties;Invoicing;Accounting;Products
IF ERRORLEVEL 1 GOTO FAIL


rem export Invoicing Module data
Zetbox.Cli.exe %config% -export ..\..\Data\Invoicing.Data.xml -schemamodules Invoicing -ownermodules Invoicing
IF ERRORLEVEL 1 GOTO FAIL

rem export Invoicing Workflow data
Zetbox.Cli.exe %config% -export ..\..\Data\Invoicing.Workflow.xml -schemamodules Workflow -ownermodules Invoicing
IF ERRORLEVEL 1 GOTO FAIL

rem export Accounting Module data
Zetbox.Cli.exe %config% -export ..\..\Data\Accounting.Data.xml -schemamodules Accounting -ownermodules Accounting
IF ERRORLEVEL 1 GOTO FAIL

rem export Products Module data
Zetbox.Cli.exe %config% -export ..\..\Data\Products.Data.xml -schemamodules Products -ownermodules Products
IF ERRORLEVEL 1 GOTO FAIL

rem export test data
Zetbox.Cli.exe %config% -export ..\..\Data\Parties.xml -schemamodules Parties;Invoicing;Accounting;Products
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
cd ..\..
rem return error without closing parent shell
echo A | choice /c:A /n

:EOF
pause
