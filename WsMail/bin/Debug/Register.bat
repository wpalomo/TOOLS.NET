Rem Registrar la DLL
Pause

D:
cd "D:\PROYECTOS\SGA.NET\TEST\WSTools\WSCrypt\bin\Debug"

cd %~dp0

"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" WSMail.Dll /codebase /tlb:WSCrypt.tlb
Pause

