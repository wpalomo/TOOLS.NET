Rem Desregistrar la DLL
Pause

rem D:
rem cd "D:\PROYECTOS\SGA.NET\TEST\WSTools\WSMail\bin\Debug"

cd %~dp0

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" WSMail.Dll /unregister
Pause
