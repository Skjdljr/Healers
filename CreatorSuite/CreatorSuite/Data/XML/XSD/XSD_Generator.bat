REM @AUTHOR Joel Faiella
@echo off

rem check to make sure they pass in a file
if [%1]==[] goto wtf

set check=%~x1

rem check to make sure the file they pass is a .xml file
if "%check%" NEQ ".xml" goto wtf

:main
set outPutLocation=%~dp0
set fileName=%~nn1
set fileNameandExtension=%~xn1
set fullPathXSDFile=%outPutLocation%%fileName%.xsd

echo Output location: %outPutLocation%
echo File to convert: %fileNameandExtension%
echo Full path to XSD: %fullPathXSDFile%

REM switch to the visual studio dev cmd line
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"
xsd %1%

REM create the class
xsd /c "%fullPathXSDFile%"
echo Generated schema: %fileName%.xsd and class %fileName%.cs

goto eof

:wtf
echo Please drag a .xml file onto this bat to create the schema and class

:eof 
pause