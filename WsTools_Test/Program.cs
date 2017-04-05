
using System;
using System.Collections.Generic;
using CRYPT;
using FFTP;
using ZIP;
using VISOR;
using VARIOS;

namespace WsTools_Test
{
   class wsTestM
    {
        public string TestMailR2K9()
        {
            MAIL.WSMail oWS = new MAIL.WSMail();

            oWS.InitializeHost("mail.renovatio2k9.net",
                              "abraham@renovatio2k9.net",
                              "avcortes2012",
                              587, false);

            oWS.SendSimpleEMail("abraham@renovatio2k9.net",
                                "abbe.cortes@gmail.com",
                                "SGA",
                                "Test e-mail .NET (R2K9)",
                                "Este es el test de envío de correo electrónico desde c# en .NET",
                                strAdjunto: "D:/INTERCAMBIO/MP2000.PDF");

            return oWS.UsrError;
        }

        public string TestMailGMail()
        {
            MAIL.WSMail oWS = new MAIL.WSMail();

            oWS.InitializeHost("smtp.gmail.com",
                              "r2k9antir@gmail.com",
                              "Renovatio2K9",
                              587, true);


            oWS.SendSimpleEMail("r2k9antir@gmail.com",
                                "abraham@renovatio2k9.net",
                                "SGA",
                                "Test e-mail .NET (R2K9)",
                                "Este es el test de envío de correo electrónico desde c# en .NET",
                                strAdjunto: @"D:\PROYECTOS\ANTIR\SOURCE\PROCAOT\COMUNICACIONES\000049\20170217174228.PDF");

            return oWS.UsrError;
        }

        public string TestMailGMail2()
        {
            MAIL.WSMail oWS = new MAIL.WSMail();

            oWS.InitializeHost("smtp.gmail.com",
                              "abbe.cortes@gmail.com",
                              "??????????",
                              587, true);


            oWS.SendFullEMail("SGA@R2K9.NET",
                               new List<string>() { "abraham@renovatio2k9.net", "avcortes@yahoo.es" },
                               "Test e-mail .NET (R2K9)",
                               "Este es el test de envío de correo electrónico desde c# en .NET",
                               lstAdjuntos: new List<string>() { "D:/INTERCAMBIO/MP2000.PDF", "D:/INTERCAMBIO/MP2001.PDF" });

            return oWS.UsrError;
        }

        public string TestMailGMail3()
        {
            MAIL.WSMail oWS = new MAIL.WSMail();

            oWS.InitializeHost("smtp.gmail.com",
                              "abbe.cortes@gmail.com",
                              "?????????",
                              587, true);

            oWS.SendFullEMail("abbe.cortes@gmail.com",
                              "Test e-mail",
                              "Este es el test de envío de correo electrónico desde c# en .NET",
                              "SGA",
                              2, 1, 1, 2,
                              "abbe.cortes@hotmail.es", "avcortes@yahoo.es",
                              "abraham@renovatio2k9.net",
                              "abbecortes@gmail.com",
                              "D:/INTERCAMBIO/MP2000.PDF", "D:/INTERCAMBIO/MP2001.PDF");

            return oWS.UsrError;
        }

        public string TestZip()
        {
            ZIP.WSZip oZP = new ZIP.WSZip();

            string cIn = @"D:/INTERCAMBIO/ANTIR/SOURCE/PROCAOT/COMUNICACIONES/000049/000049.ini";
            string cOut = @"D:/INTERCAMBIO/ANTIR/SOURCE/PROCAOT/COMUNICACIONES/000049/000049.zip";
            oZP.SimpleCompress(cIn,  cOut);

            cIn = @"D:\INTERCAMBIO\ZIP\";
            cOut = @"D:\INTERCAMBIO\ZIP\COPIA.zip";

            oZP.SimpleCompress(cIn, cOut);

            return oZP.UsrError;
        }

        public string TestZip2()
        {
            ZIP.WSZip oZP = new ZIP.WSZip();

            string cIn = @"D:\INTERCAMBIO\ZIP";
            string cOut = @"D:\INTERCAMBIO\COPIA.zip";

            cIn = @"D:\INTERCAMBIO\ZIP\*.*";
            cOut = @"D:\INTERCAMBIO\COPIA.zip";

            oZP.FullCompress(cOut, true, 1, cIn, @"*.PDF");

            return oZP.UsrError;
        }

        public string TestFTP()
        {
            WSFtp oFTP = new WSFtp("ftp://ftp.renovatio2k9.net", "renovati@renovatio2k9.net", "lO5TUsLbOe", 21);

            oFTP.FtpCreateDirectory(@"\Clientes\antir\Out\Test");
            oFTP.FtpUploadFile(@"D:\INTERCAMBIO\ANTIR\SALIALBC21.PDF", @"\Clientes\antir\Out\Test\SALIALBC21.PDF");
            oFTP.FtpFileExists(@"\Clientes\antir\Out\Test\SALIALBC21.PDF");
            oFTP.FtpRenameFile(@"\Clientes\antir\Out\Test\SALIALBC21.PDF", @"SALIALBC21NEW.PDF");
            oFTP.FtpDownloadFile(@"\Clientes\antir\Out\Test\SALIALBC21NEW.PDF", @"D:\INTERCAMBIO\ANTIR\SALIALBC21NEW.PDF");
            oFTP.FtpDeleteFile(@"\Clientes\antir\Out\Test\SALIALBC21NEW.PDF");
            oFTP.FtpDeleteDirectory(@"\Clientes\antir\Out\Test");

            return oFTP.UsrError;
        }

        public bool TestLog()
        {
            VISOR.WSVisor oLog = new WSVisor();
            string processName = System.Diagnostics.Process.GetCurrentProcess().ToString();
            int processID = System.Diagnostics.Process.GetCurrentProcess().Id;

            //oLog.RegistrarEvento("Application", processName, processID, 0, (int)TOOLS._Tools.EventLevel.INFORMATION, "mensaje1", "mensaje2");
            //oLog.RegistrarEvento("Application", this.GetType().FullName, processID, 0, (int)TOOLS._Tools.EventLevel.INFORMATION, "mensaje1", "mensaje2");
            oLog.RegistrarEvento("Application",
                                 "SGA.WSSga.articulos",
                                 System.Diagnostics.Process.GetCurrentProcess().Id, 0,
                                 (int)TOOLS._Tools.EventLevel.INFORMATION, "INICIO");
            return true;
        }

        public bool TestGeneric()
        {
            int? aaa = 7;
            int bbb;

            //if (!aaa.HasValue) aaa = 0;
            bbb = aaa ?? 0;

            // Test de pila genérica.
            WSVarios.GenericStack<int> oTools = new WSVarios.GenericStack<int>();

            oTools.Push(3);
            oTools.Push(32);
            oTools.Push(38);

            for (; oTools.Elementos > 0; ) Console.WriteLine(oTools.Pop().ToString());
            Console.ReadKey();

            // Test de cola genérica.
            WSVarios.GenericQueue<int> oQueue = new WSVarios.GenericQueue<int>();

            oQueue.Push(91);
            oQueue.Push(99);
            oQueue.Push(3);

            for (; oQueue.Elementos > 0; ) Console.WriteLine(oQueue.Pop().ToString());
            Console.ReadKey();

            // Test conversor genérico de tipos.
            WSVarios.GenericConversion<String> oConv = new WSVarios.GenericConversion<string>();
            DateTime dVar = DateTime.Now.ToLocalTime();

            Console.WriteLine(WSVarios.ConvertirTipoS<String>(dVar));
            Console.WriteLine(oConv.ConvertirTipo(dVar));

            Console.ReadKey();

            return true;
        }
    }
}

/// <summary> Para testear las diversas funciones. </summary>
/// 
class Program
{
    static void Main(string[] args)
    {
        WsTools_Test.wsTestM oTS = new WsTools_Test.wsTestM();

        //WSCrypt oCR = new WSCrypt();

        //Console.WriteLine(oTS.TestMailR2K9());
        //Console.ReadKey();

        //Console.WriteLine(oTS.TestMailGMail());
        //Console.ReadKey();

        //Console.WriteLine(oTS.TestMailGMail2());
        //Console.ReadKey();

        //Console.WriteLine(oTS.TestMailGMail3());
        //Console.ReadKey();

        //string cadena = oCR.Encriptar(Console.ReadLine());
        //Console.WriteLine(cadena);
        //Console.ReadKey();
        //Console.WriteLine(oCR.DesEncriptar(cadena));
        //Console.ReadKey();
        //Console.WriteLine(oTS.TestZip2());
        //Console.ReadKey();
        //Console.WriteLine(oTS.TestFTP());
        //Console.ReadKey();

        //Console.WriteLine(oTS.TestLog());
        //Console.ReadKey();

        Console.WriteLine(oTS.TestGeneric());
        Console.ReadKey();
    }
}

