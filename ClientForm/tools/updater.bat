@echo off

rem if ERRORLEVEL 1 goto error

net stop PaperService
rem setlocal
REM >output.txt (
cd /d %~dp0
pushd..
set parent=%cd%
popd
rem timeout 5
echo Parent is %parent%
echo Current is %cd%
set /p Build=<"%parent%\Version.json"
echo Build is: %Build%


7za a -tzip "%parent%\Archive\%Build%\backup" "%parent%\Service.exe" "%parent%\Service.pdb" "%parent%\Version.json" 

7za x "%parent%\temp\*.zip" -o"%parent%\" -aoa
rem del %parent%\temp\*.zip
REM )
rem pause

net start PaperService
