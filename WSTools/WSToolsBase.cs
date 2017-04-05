
//------------------------------------------------------------------
// Funciones base de la Solución TOOLS.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace TOOLS
{
    /// <summary> Propiedades y métodos base para TOOLS. </summary>
    /// 
    /// <remarks>
    /// Necesario declarar para interoperabilidad para que
    /// los proyectos que hereden lo pudedan hacer también.
    /// </remarks>

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSTools")]

    public class _Tools
    {
        //------------------------------------------------------------------
        // Propiedades internas para para el proyecto WSMail.
        //------------------------------------------------------------------

        /// <summary> Propiedades del servidor de correo. </summary>
        protected MailMessage mailMessage = new MailMessage();

        /// <summary> Propiedades del mensaje de correo. </summary>
        protected SmtpClient smtpClient = new SmtpClient();

        //------------------------------------------------------------------
        // Propiedades internas para para el proyecto WSZip.
        //------------------------------------------------------------------

        /// <summary> Grado de compresión de ficheros. </summary>
        public enum CompressionLev
        {
            /// <summary> </summary>
            FASTEST = 1,
            /// <summary> </summary>
            NOCOMPRESSION = 2,
            /// <summary> </summary>
            OPTIMAL = 0
        }

        //------------------------------------------------------------------
        // Propiedades internas para para el proyecto WSFtp.
        //------------------------------------------------------------------

        /// <summary> Conexión con el Host Ftp para Upload / Download archivos. </summary>
        protected FtpWebRequest FtpRequest;

        /// <summary> Conexión con el Host Ftp para descargar archivos. </summary>
        protected FtpWebResponse FtpResponse;

        /// <summary> Canal de comunicación con el Host Ftp. </summary>
        protected Stream FtpFileStream;

        /// <summary> Canal de comunicación con el Host Ftp para lectura. </summary>
        protected StreamReader FtpStreamReader;

        //------------------------------------------------------------------
        // Propiedades internas para para el proyecto WSVisor.
        //------------------------------------------------------------------

        /// <summary> Nivel de mensaje. </summary>
        public enum EventLevel
        {
            /// <summary> </summary>
            ERROR = 1,
            /// <summary> </summary>
            FAILUREAUDIT = 16,
            /// <summary> </summary>
            INFORMATION = 4,
            /// <summary> </summary>
            SUCCESSAUDIT = 8,
            /// <summary> </summary>
            WARNING = 2
        }

        //-----------------------------------------------------------------------------------

        //------------------------------------------------------------------
        // Propiedades públicas de gestión de errores.
        //------------------------------------------------------------------

        /// <summary> Tamaño buffer comunicación. </summary>
        protected const int _BYTE_BUFFER = 2048;

        private Char resultado;
        /// <summary> Resultado de llamadas a función. </summary>
        public Char Resultado
        {
            get { return resultado;  }
            set { resultado = value; }
        }

        private int usrerrorc;
        /// <summary> Código de error de programa. </summary>
        public int UsrErrorC
        {
            get { return usrerrorc; }
            set { usrerrorc = value; }
        }

        private string usrerror;
        /// <summary> Mensajes de error. </summary>
        public String UsrError
        {
            get { return usrerror; }
            set { usrerror = value; }
        }

        private string usrerrore;
        /// <summary> Mensaje de error extendido. </summary>
        public String UsrErrorE
        {
            get { return usrerrore; }
            set { usrerrore = value; }
        }

        /// <summary> Constructor por defecto. </summary>
        /// 
        public _Tools()
        {
            UsrError = "OK";
            UsrErrorC = 0;
            UsrErrorE = "";
            UsrErrorE = "";
            resultado = 'N';
        }

    }
}
