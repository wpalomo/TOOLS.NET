
//------------------------------------------------------------------
// Gestión de Ftp.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace FFTP
{
    /// <summary> Funciones de Ftp. </summary>
    /// 
    /// <remarks> Namespace FTP. Atención NO usar FTP como nombre de Namespace. </remarks>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSFtp")]

    public class WSFtp : TOOLS._Tools
    {
        //------------------------------------------------------------------
        // Propiedades internas para la gestión de Ftp.
        //------------------------------------------------------------------

        /// <summary> IP del Host Ftp. </summary>
        private string ftphost = null;
        /// <summary> </summary>
        public string FTPHost
        {
            get { return ftphost; }
            set { ftphost = value; }
        }

        /// <summary> Usuario del Host Ftp. </summary>
        private string ftpuser = null;
        /// <summary> </summary>
        public string FTPUser
        {
            get { return ftpuser; }
            set { ftpuser = value; }
        }

        /// <summary> Contraseña del Host Ftp. </summary>
        private string ftppwd = null;
        /// <summary> </summary>
        public string FTPPwd
        {
            protected get { return ftppwd; }
            set { ftppwd = value; }
        }

        /// <summary> Puerto del Host Ftp. </summary>
        private int ftpport = (int)FtpPort.PORTFTP;
        /// <summary> </summary>
        public int FTPPort
        {
            get { return ftpport; }
            set { ftpport = value; }
        }

        //------------------------------------------------------------------
        // Propiedades públicas para la gestión de Ftp.
        //------------------------------------------------------------------

        private bool usepassive;
        /// <summary> </summary>
        public bool UsePassive
        {
            set { usepassive = value; }
            get { return usepassive; }
        }

        private bool usebinary;
        /// <summary> </summary>
        public bool UseBinary
        {
            set { usebinary = value; }
            get { return usebinary; }
        }

        private bool keepalive;
        /// <summary> </summary>
        public bool KeepAlive
        {
            set { keepalive = value; }
            get { return keepalive; }
        }

        /// <summary> Números de puerto. </summary>
        public enum FtpPort
        {
            /// <summary> </summary>
            PORTFTP = 21,
            /// <summary> </summary>
            PORTWEB = 80
        }

        //------------------------------------------------------------------
        // Métodos públicos de la clase.
        //------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteFile"> Nombre del fichero remoto. </param>
        /// 
        /// <returns> Resultado (S/N/C) </returns>
        /// 
        public Char FtpFileExists(string remoteFile)
        {
            // string fileInfo;                     Innecesario.

            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Retorno de comunicación con el Host Ftp.
                FtpFileStream = FtpResponse.GetResponseStream();

                // Canal de lectura del Host Ftp. - Innecesario.
                // FtpStreamReader = new StreamReader(FtpFileStream);
                // fileInfo = FtpStreamReader.ReadToEnd();

                // Cerrar canales de comunicación Ftp.
                FtpFileStream.Close();
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (WebException ex) when (ex.Status.ToString()=="ProtocolError")
            {
                // Error de Ftp.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'N';
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Eliminar un fichero remoto. </summary>
        /// 
        /// <param name="remoteFile"> Fichero remoto a eliminar. </param>
        /// 
        /// <returns></returns>
        /// 
        public Char FtpDeleteFile(string remoteFile)
        {
            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Cerrar canales de comunicación Ftp.
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Renombrar un archivo remoto. </summary>
        /// 
        /// <param name="remotePathFile"> Path del archivo remoto a renombrar. </param>
        /// <param name="remoteFileNew"> Nuevo nombre del archivo remoto. </param>
        /// 
        /// <returns></returns>
        /// 
        public Char FtpRenameFile(string remotePathFile, string remoteFileNew)
        {
            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remotePathFile);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.Rename;
                FtpRequest.RenameTo = remoteFileNew;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Cerrar canales de comunicación Ftp.
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Crear un directorio remoto. </summary>
        /// 
        /// <param name="remoteDir"> Directorio remoto a crear. </param>
        /// 
        /// <returns></returns>
        /// 
        public Char FtpCreateDirectory(string remoteDir)
        {
            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteDir);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Cerrar canales de comunicación Ftp.
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Eliminar un directorio remoto. </summary>
        /// 
        /// <param name="remoteDir"> Directorio remoto a eliminar. </param>
        /// 
        /// <returns></returns>
        /// 
        public Char FtpDeleteDirectory(string remoteDir)
        {
            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteDir);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Cerrar canales de comunicación Ftp.
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Enviar un fichero al servidor Ftp. </summary>
        /// 
        /// <param name="localFile"> Nombre del fichero local. </param>
        /// <param name="remoteFile"> Nombre del fichero remoto. </param>
        /// <param name="crearFichero"> Sobreescribir destino si ya existe. </param>
        /// 
        /// <returns> Resultado (S/N/C) </returns>
        /// 
        public Char FtpUploadFile(string localFile, string remoteFile, int crearFichero = (int)FileMode.Create)
        {
            FileStream localFileStream;
            byte[] byteBuffer;
            int bytesEnviados;

            Resultado = 'N';

            try
            {
                if(String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd==null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Retorno de comunicación con el Host Ftp.
                FtpFileStream = FtpRequest.GetRequestStream();

                // Canal de comunicación con el fichero local.
                localFileStream = new FileStream(localFile, FileMode.Open);
                byteBuffer = new byte[_BYTE_BUFFER];

                bytesEnviados = localFileStream.Read(byteBuffer, 0, _BYTE_BUFFER);
                while(bytesEnviados > 0)
                {
                    FtpFileStream.Write(byteBuffer,0, bytesEnviados);
                    bytesEnviados = localFileStream.Read(byteBuffer, 0, _BYTE_BUFFER);
                }

                // Cerrar canales de comunicación Ftp.
                localFileStream.Close();
                FtpFileStream.Close();
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de Ftp.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Descargar un fichero al servidor Ftp. </summary>
        /// 
        /// <param name="remoteFile"> Nombre del fichero remoto. </param>
        /// <param name="localFile"> Nombre del fichero local. </param>
        /// <param name="crearFichero"> Sobreescribir destino si ya existe. </param>
        /// 
        /// <returns> Resultado (S/N/C) </returns>
        /// 
        public Char FtpDownloadFile(string remoteFile, string localFile, int crearFichero = (int)FileMode.Create)
        {
            FileStream localFileStream;
            byte[] byteBuffer;
            int bytesLeidos;

            Resultado = 'N';

            try
            {
                if (String.IsNullOrEmpty(FTPHost) || string.IsNullOrEmpty(FTPUser) || FTPPwd == null)
                {
                    UsrError = "KO - Faltan parámetros de conexión al Host Ftp";
                    UsrErrorC = -1;
                    Resultado = 'N';

                    return Resultado;
                }

                // Conexión con el Host Ftp.
                FtpRequest = (FtpWebRequest)FtpWebRequest.Create(FTPHost + "/" + remoteFile);
                FtpRequest.Credentials = new NetworkCredential(FTPUser, FTPPwd);
                FtpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                // Canal de comunicación con el Host Ftp.
                FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();

                // Retorno de comunicación con el Host Ftp.
                FtpFileStream = FtpResponse.GetResponseStream();

                // Canal de comunicación con el fichero local.
                localFileStream = new FileStream(localFile, FileMode.Create);
                byteBuffer = new byte[_BYTE_BUFFER];

                // Leer datos del fichero remoto y grabar en fichero local.
                bytesLeidos = FtpFileStream.Read(byteBuffer, 0, _BYTE_BUFFER);

                while(bytesLeidos > 0)
                {
                    localFileStream.Write(byteBuffer, 0, _BYTE_BUFFER);
                    bytesLeidos = FtpFileStream.Read(byteBuffer, 0, _BYTE_BUFFER);
                }

                // Cerrar canales de comunicación Ftp.
                localFileStream.Close();
                FtpFileStream.Close();
                FtpRequest = null;
                FtpResponse.Close();

                Resultado = 'S';
                UsrError = "OK";
            }

            catch (Exception ex)
            {
                // Error de Ftp.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                Resultado = 'C';
            }

            return Resultado;
        }

        /// <summary> Inicializar la instancia Ftp. </summary>
        /// 
        /// <remarks>
        /// Por compatibilidad con llamadas desde 32 bits.
        /// De forma genérica debe llamarse al constructor.
        /// </remarks>
        /// 
        /// <param name="IPHost"></param>
        /// <param name="User"></param>
        /// <param name="Pwd"></param>
        /// <param name="puerto"></param>
        /// <param name="usePassive"></param>
        /// <param name="useBinary"></param>
        /// <param name="keepAlive"></param>
        /// 
        public void InicializeFtp(string IPHost, string User, string Pwd,
                                  int puerto = 0,
                                  bool usePassive = true,
                                  bool useBinary = true,
                                  bool keepAlive = true)
        {
            FTPHost = IPHost;
            FTPUser = User;
            FTPPwd = Pwd;
            FTPPort = puerto;

            UsePassive = usePassive;
            UseBinary = useBinary;
            KeepAlive = keepAlive;

            return;
        }

        //------------------------------------------------------------------
        // Constructores de la clase.
        //------------------------------------------------------------------

        /// <summary> Constructor por defecto de la clase. </summary>
        public WSFtp()
        {
        }

        /// <summary> Constructor alternativo de la clase. </summary>
        /// 
        /// <param name="IPHost"></param>
        /// <param name="User"></param>
        /// <param name="Pwd"></param>
        /// <param name="puerto"></param>
        /// <param name="usePassive"></param>
        /// <param name="useBinary"></param>
        /// <param name="keepAlive"></param>
        /// 
        public WSFtp(string IPHost, string User, string Pwd,
                     int puerto = 0,
                     bool usePassive = true,
                     bool useBinary = true,
                     bool keepAlive = true)
        {
            InicializeFtp(IPHost, User, Pwd, puerto, usePassive, useBinary, keepAlive);
        }
    }
}
