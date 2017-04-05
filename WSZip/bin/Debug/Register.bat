Rem Registrar la DLL
Pause

rem D:
rem cd "D:\PROYECTOS\SGA.NET\TEST\WSTools\WSZip\bin\Debug"

cd %~dp0

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" WSZip.Dll /codebase /tlb:WSZip.tlb
Pause

