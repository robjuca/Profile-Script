@echo off
 chdir /d D:\
 
title rebuild rrLibrary at "D:\Visual Studio 2022\Projects\Library\rrSoft 2024"
echo RR LIBRARY COMPILE
CALL "D:\Visual Studio 2022\Projects\Library\rrSoft 2024\00-rrLibraryBuild.bat"
echo.
echo.

title cleanup folders
echo CLEANUP SCRIPT BIN
cd "D:\Visual Studio 2022\Projects\Profile Script\Bin"
erase /q /s rr.Library*.*
echo DONE
echo.
echo.

title update folders
echo LOAD LIBRARY TO SCRIPT BIN
cd "D:\Visual Studio 2022\Projects\Profile Script\Bin"
copy /y "D:\Visual Studio 2022\Projects\Library\rrSoft 2024\Bin\*.*"
echo DONE
echo.
echo.

title update addons
echo ERASE ALL IN ADDONS
cd "D:\SPAD.neXt\AddOns"
erase /q /s *.dll
echo.
echo.
echo LOAD LIBRARY TO ADDONS
copy /y "D:\Visual Studio 2022\Projects\Library\rrSoft 2024\Bin\*.dll"
echo DONE
echo.
echo.
echo.
echo.

 
pause