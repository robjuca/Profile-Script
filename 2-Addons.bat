@echo off
 chdir /d D:\
 
 title cleanup folders
echo CLEANUP SCRIPT BIN
cd "D:\Visual Studio 2022\Projects\Profile Script\Bin"
erase /q /s rr.s*.*
erase /q /s rr.p*.*
echo DONE
echo.
echo.


rem PROVIDER RESOURCES
echo 0-PROVIDER RESOURCES
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Provider\Resources\Provider Resources.sln" /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.


REM rem PROVIDER SERVICES
echo 1-PROVIDER SERVICES
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Provider\Services\Provider Services.sln"  /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.



rem PROVIDER MESSAGE
echo 2-PROVIDER MESSAGE
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Provider\Message\Provider Message.sln"  /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.

rem PROVIDER PRESENTATION
echo 3-PROVIDER PRESENTATION
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Provider\Presentation\Provider Presentation.sln"  /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.



title rebuild Script Profile at "D:\Visual Studio 2022\Projects\Profile Script\"
rem PROCESS DISPATCHER
echo 4-PROCESS DISPATCHER
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"  "D:\Visual Studio 2022\Projects\Profile Script\Process\Dispatcher\Process Dispatcher.sln"  /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.
echo.



rem  Handler Model
echo 5-  HANDLER MODEL
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Handler\Model\Handler Model.sln" /t:rebuild /verbosity:minimal /nologo 
	
	 
echo.
echo.
echo.




REM rem PLATES OPERATION GROUND
echo PLATES OPERATION GROUND
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"  "D:\Visual Studio 2022\Projects\Profile Script\Plates\Operation\Ground\Plate Ground.sln" /t:rebuild /verbosity:minimal /nologo 
	 
	 

echo.
echo.
echo.
echo.

REM REM rem PLATES OPERATION OUTSIDE
echo PLATES OPERATION OUTSIDE
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"  "D:\Visual Studio 2022\Projects\Profile Script\Plates\Operation\Outside\Plate Outside.sln" 	/t:rebuild /verbosity:minimal /nologo 
	 
	 

echo.
echo.
echo.
echo.


REM REM rem PLATES OPERATION DOOR
REM echo PLATES OPERATION DOOR
	REM "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"  "D:\Visual Studio 2022\Projects\Profile Script\Plates\Operation\Door\Plate Door.sln" 	/t:rebuild /verbosity:minimal /nologo 
	 
	 

REM echo.
REM echo.
REM echo.
REM echo.


REM rem PLATES INITIALIZATION DEVICE
echo PLATES INITIALIZATION DEVICE
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "D:\Visual Studio 2022\Projects\Profile Script\Plates\Initialization\Device\Plate Device.sln"	 /t:rebuild /verbosity:minimal /nologo 
	 
	

echo.
echo.
echo.
echo.


rem SHELL BOOTSTRAPPER
echo SHELL BOOTSTRAPPER
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"   "D:\Visual Studio 2022\Projects\Profile Script\Shell\Bootstrapper\Shell Bootstrapper.sln"	 /t:rebuild /verbosity:minimal /nologo 
	
echo.
echo.
echo.
echo.

rem SHELL CREATION
echo SHELL CREATION
	"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"   "D:\Visual Studio 2022\Projects\Profile Script\Shell\Creation\Shell Creation.sln"	 /t:rebuild /verbosity:minimal /nologo 
	
echo.
echo.
echo.
echo.

pause