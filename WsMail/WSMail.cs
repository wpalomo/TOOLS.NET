
//------------------------------------------------------------------
// Gestión de e-mail.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace MAIL
{
    /// <summary> Clase para enviar e-mails desde programas. </summary>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSMail")]

    public class WSMail : TOOLS._Tools
    {
        //------------------------------------------------------------------
        // Propiedades públicas para el proyecto WSMail.
        //------------------------------------------------------------------

        private int priority;
        /// <summary> Prioridad del mensaje. </summary>
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private bool bodyhtml;
        /// <summary> Body en Html. </summary>
        public bool BodyHtml
        {
            get { return bodyhtml; }
            set { bodyhtml = value; }
        }

        //------------------------------------------------------------------
        // Métodos públicos de la clase.
        //------------------------------------------------------------------

        /// <summary> Función simple de envío de e-mail. </summary>
        /// 
        /// <remarks>
        /// Función de uso general. 
        /// Valido en entornos de 32 bits y 64 bits.
        /// </remarks>
        /// 
        /// <param name="strFrom"> Remitente. </param>
        /// <param name="strTo"> Destinatario. </param>
        /// <param name="strName"> Nombre a mostrar. </param>
        /// <param name="strSubject"> Asunto. </param>
        /// <param name="strBody"> Cuerpo del mensaje. </param>
        /// <param name="strCC"> Destinatarios con copia normal. </param>
        /// <param name="strCCO"> Destinatarios con copia oculta. </param>
        /// <param name="strAdjunto"> Archivo adjunto. </param>
        /// 
        /// <returns> Resultado (S / N) </returns>
        /// 
        public Char SendSimpleEMail(string strFrom,
                                    string strTo,
                                    string strName = "",
                                    string strSubject = "",
                                    string strBody = "",
                                    string strCC = "",
                                    string strCCO = "",
                                    string strAdjunto = "")
        {
            Char retorno = 'N';

            try
            {
                UsrError = "OK - Enviar e-Mail";
                UsrErrorC = 1;
                UsrErrorE = "";

                if(string.IsNullOrEmpty(strFrom))
                {
                    UsrError = "KO - Remitente en blanco";
                    UsrErrorC = -1;

                    return retorno;
                }

                if (string.IsNullOrEmpty(strTo))
                {
                    UsrError = "KO - Destinatario en blanco";
                    UsrErrorC = -2;

                    return retorno;
                }

                // Asignar las propiedades del mensaje.
                mailMessage.From = new MailAddress(strFrom, strName);
                mailMessage.To.Add(new MailAddress(strTo));
                if(String.IsNullOrEmpty(strCC)==false) mailMessage.CC.Add(new MailAddress(strCC));
                if (String.IsNullOrEmpty(strCCO)==false) mailMessage.Bcc.Add(new MailAddress(strCCO));

                mailMessage.Subject = strSubject;
                mailMessage.Body = strBody;

                // Agregar datos adjuntos.
                if (String.IsNullOrEmpty(strAdjunto)==false)
                    if(System.IO.File.Exists(strAdjunto))
                        mailMessage.Attachments.Add(new Attachment(strAdjunto));

                // Propiedades generales del mensaje.
                mailMessage.IsBodyHtml = false;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                mailMessage.Sender = new MailAddress(strFrom);

                // Enviar el mensaje.
                smtpClient.Send(mailMessage);
                retorno = 'S';
            }

            catch (Exception ex)
            {
                // Error de e-mail.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                retorno = 'C';
            }

            return retorno;
        }

        /// <summary> Función simple de envío de e-mail. </summary>
        /// 
        /// <remarks>
        /// Función de uso general. 
        /// Valido en entornos de 64 bits.
        /// </remarks>
        /// 
        /// <param name="strFrom"> Remitente. </param>
        /// <param name="lstTo"> Destinatario. </param>
        /// <param name="strName"> Nombre a mostrar. </param>
        /// <param name="strSubject"> Asunto. </param>
        /// <param name="strBody"> Cuerpo del mensaje. </param>
        /// <param name="lstToCC"> Destinatarios con copia normal. </param>
        /// <param name="lstToCCO"> Destinatarios con copia oculta. </param>
        /// <param name="lstAdjuntos"> Archivos adjuntos. </param>
        /// 
        /// <returns> Resultado (S / N) </returns>
        /// 
        public Char SendFullEMail(string strFrom,
                                  List<string> lstTo,
                                  string strName = "",
                                  string strSubject = "",
                                  string strBody = "",
                                  List<string> lstToCC = null,
                                  List<string> lstToCCO = null,
                                  List<string> lstAdjuntos = null)
        {
            Char retorno = 'N';

            try
            {
                UsrError = "OK - Enviar e-Mail";
                UsrErrorC = 1;
                UsrErrorE = "";

                if (string.IsNullOrEmpty(strFrom))
                {
                    UsrError = "KO - Remitente en blanco";
                    UsrErrorC = -1;

                    return retorno;
                }

                if (lstTo.Equals(null) || (lstTo.Count <= 0))
                {
                    UsrError = "KO - Destinatario en blanco";
                    UsrErrorC = -2;

                    return retorno;
                }

                // Asignar las propiedades del mensaje: Destinatarios.
                for (int i = 0; i < lstTo.Count; i++) mailMessage.To.Add(new MailAddress(lstTo[i]));

                // Asignar las propiedades del mensaje: Destinatarios con copia normal.
                if(lstToCC != null)
                    for (int i = 0; i < lstToCC.Count; i++) mailMessage.CC.Add(new MailAddress(lstToCC[i]));

                // Asignar las propiedades del mensaje: Destinatarios con copia oculta.
                if (lstToCCO != null)
                    for (int i = 0; i < lstToCCO.Count; i++) mailMessage.Bcc.Add(new MailAddress(lstToCCO[i]));

                // Asignar las propiedades del mensaje: Archivos adjuntos.
                if (lstAdjuntos != null)
                    for (int i = 0; i < lstAdjuntos.Count; i++)
                        if (System.IO.File.Exists(lstAdjuntos[i]))
                            mailMessage.Attachments.Add(new Attachment(lstAdjuntos[i]));

                // Asignar propiedades generales del mensaje.
                mailMessage.From = new MailAddress(strFrom, strName);           // Remitente.
                mailMessage.Subject = strSubject;                               // Asunto.
                mailMessage.Body = strBody;                                     // Cuerpo del mensaje.

                // Propiedades generales del mensaje.
                mailMessage.IsBodyHtml = false;                                 // Formato HTML.
                mailMessage.Priority = MailPriority.Normal;                     // Prioridad.
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                // Enviar el mensaje.
                smtpClient.Send(mailMessage);
                retorno = 'S';
            }

            catch (Exception ex)
            {
                // Error de e-mail.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                retorno = 'C';
            }

            return retorno;
        }

        /// <summary> Función simple de envío de e-mail. </summary>
        /// 
        /// <remarks>
        /// Función de uso general. 
        /// Valido en entornos de 32 bits y 64 bits.
        /// 
        /// Formato de ParameterList:
        ///     nD: Nº de destinatarios.
        ///     d1, d2, ---, dn: Lista de destinatarios.
        ///     nX: Nº de destinatarios con copia.
        ///     x1, x2, ..., xn: Lista de destinatarios con copia.
        ///     nY: Nª de destinatarios con copia oculta.
        ///     y1, y2, ..., yn: Lista de destinatarios con copia oculta.
        ///     nZ: Nº de archivos adjuntos.
        ///     z1, z2, ..., zn: Lista de archivos adjuntos.
        /// </remarks>
        /// 
        /// <param name="strFrom"> Remitente. </param>
        /// <param name="strSubject"> Asunto. </param>
        /// <param name="strBody"> Cuerpo del mensaje. </param>
        /// <param name="strName"> Nombre del remitente. </param>
        /// <param name="ToParameterNo"> Nº de destinatarios. </param>
        /// <param name="CcParameterNo"> Nº de destinatarios con copia normal. </param>
        /// <param name="CcoParameterNo"> Nº de destinatarios con copia oculta. </param>
        /// <param name="AttachParameterNo"> Nº de archivos adjuntos. </param>
        /// <param name="ParameterList"> Lista de parámetros (See Up). </param>
        /// 
        /// <returns></returns>
        /// 
        public Char SendFullEMail(string strFrom,
                                  string strSubject,
                                  string strBody,
                                  string strName = "",
                                  int ToParameterNo = 1,
                                  int CcParameterNo = 0,
                                  int CcoParameterNo = 0,
                                  int AttachParameterNo = 0,
                                  params object[] ParameterList)
        {
            Char retorno = 'N';
            int CurrentParamNo = 0;

            try
            {
                UsrError = "OK - Enviar e-Mail";
                UsrErrorC = 1;
                UsrErrorE = "";

                if (string.IsNullOrEmpty(strFrom))
                {
                    UsrError = "KO - Remitente en blanco";
                    UsrErrorC = -1;

                    return retorno;
                }

                if ((ToParameterNo + CcoParameterNo + CcoParameterNo + AttachParameterNo)<=0)
                {
                    UsrError = "KO - Número de parámetros erróneo";
                    UsrErrorC = -2;

                    return retorno;
                }

                if ((ToParameterNo + CcoParameterNo + CcoParameterNo + AttachParameterNo) != ParameterList.Count())
                {
                    UsrError = "KO - No hay suficientes parámetros";
                    UsrErrorC = -2;

                    return retorno;
                }

                // Asignar las propiedades del mensaje: Destinatarios.
                for (int i = 0; i < ToParameterNo; i++, CurrentParamNo++) 
                    mailMessage.To.Add(new MailAddress((string)ParameterList[i]));

                // Asignar las propiedades del mensaje: Destinatarios con copia normal.
                for (int i = 0; i < CcParameterNo; i++, CurrentParamNo++)
                    mailMessage.CC.Add(new MailAddress((string)ParameterList[CurrentParamNo + i]));

                // Asignar las propiedades del mensaje: Destinatarios con copia oculta.
                for (int i = 0; i < CcoParameterNo; i++, CurrentParamNo++)
                    mailMessage.Bcc.Add(new MailAddress((string)ParameterList[CurrentParamNo + i]));

                // Asignar las propiedades del mensaje: Archivos adjuntos.
                for (int i = 0; i < AttachParameterNo; i++)
                    if (System.IO.File.Exists((string)ParameterList[CurrentParamNo + i]))
                            mailMessage.Attachments.Add(new Attachment((string)ParameterList[CurrentParamNo + i]));

                // Asignar propiedades generales del mensaje.
                mailMessage.From = new MailAddress(strFrom, strName);           // Remitente.
                mailMessage.Subject = strSubject;                               // Asunto.
                mailMessage.Body = strBody;                                     // Cuerpo del mensaje.

                // Propiedades generales del mensaje.
                mailMessage.IsBodyHtml = false;                                 // Formato HTML.
                mailMessage.Priority = MailPriority.Normal;                     // Prioridad.
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                // Enviar el mensaje.
                smtpClient.Send(mailMessage);
                retorno = 'S';
            }

            catch (Exception ex)
            {
                // Error de e-mail.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                retorno = 'C';
            }

            return retorno;
        }

        /// <summary> Inicializar las propiedades del servidor de correo. </summary>
        /// 
        /// <param name="strHost"></param>
        /// <param name="strUser"></param>
        /// <param name="strPwd"></param>
        /// <param name="nPort"></param>
        /// <param name="enableSSL"></param>
        /// 
        /// <returns> Resultado (S / N) </returns>
        /// 
        public Char InitializeHost(string strHost, string strUser, string strPwd, int nPort, bool enableSSL)
        {
            Char retorno = 'S';

            try
            {
                UsrError = "OK - Inicializar Host";
                UsrErrorC = 0;
                UsrErrorE = "";

                smtpClient.Host = strHost;
                smtpClient.Port = nPort;
                smtpClient.EnableSsl = enableSSL;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(strUser, strPwd);
            }

            catch (Exception ex)
            {
                // Error de Host.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                retorno = 'C';
            }

            return retorno;
        }

        /// <summary> Al finalizar el proceso. </summary>
        /// 
        public void Finalizar()
        {
            mailMessage.Dispose();
            smtpClient.Dispose();
        }
    }
}
