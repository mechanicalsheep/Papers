@echo off

::checkChoco
if exist "C:\ProgramData\chocolatey\choco.exe" (echo choco is installed
goto end
) else (echo choco is not installed
goto installChoco)

:installChoco
echo attempting to install chocolatey.
@"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"

echo CHOCO IS NOW INSTALLED


setlocal enabledelayedexpansion
set g_strInternalOutputFullFilename=%TEMP%\Output.log

REG QUERY "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP" /s|FIND "v4" > "!g_strInternalOutputFullFilename!"
IF %ErrorLevel% EQU 0 (
    echo DotNet4.0 already installed
Goto :SkipDotNet
) else (
    echo .NET not installed
)
::Install DotNet 4.0 Install
rem Start /B /I /WAIT %~dp0\dotNetFx40_Full_x86_x64.exe /q /norestart
choco install dotnetfx -y
goto end
:SkipDotNet

:end
pause