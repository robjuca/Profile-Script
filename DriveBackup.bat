@echo off
 chdir /d D:\
 
 title cleanup folders
echo CLEANUP DRIVE wait will get some time
rmdir "G:\My Drive\Documents\robjuca\Visual Studio\Backup" /s /q 
echo DONE
echo.
echo.
echo.

 title copy folders
echo COPY TO DRIVE wait will get some time
mkdir "G:\My Drive\Documents\robjuca\Visual Studio\Backup"
xcopy "D:\Visual Studio 2022\Projects"  "G:\My Drive\Documents\robjuca\Visual Studio\Backup"  /s /e /q
echo DONE
echo.
echo.
echo.





pause