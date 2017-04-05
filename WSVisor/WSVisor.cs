
//------------------------------------------------------------------
// Gestion del visor de eventos de Wndows.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.ServiceProcess;

namespace VISOR
{
    /// <summary> Funciones para gestión del visor de sucesos de Windows. </summary>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSVisor")]

    public class WSVisor : TOOLS._Tools
    {
        /// <summary> Generar un evento en el registro de eventos. </summary>
        /// 
        /// <param name="eventName"> Nombre del evento. </param>
        /// <param name="eventSource"> Origen del evento. </param>
        /// <param name="eventID"> ID del evento a registrar. </param>
        /// <param name="eventCategory"> Categoría del evento a registrar. </param>
        /// <param name="messageEventType"> Tipo de mensaje. </param>
        /// <param name="messageEventArgs"> Lista de mensajes. </param>
        /// 
        /// <returns></returns>
        /// 
        public void RegistrarEvento(string eventName,
                                    string eventSource,
                                    int eventID,
                                    short eventCategory,
                                    int messageEventType,
                                    params string[] messageEventArgs)
        {
            StringBuilder message = new StringBuilder();
            System.Diagnostics.EventLog logEvent = new System.Diagnostics.EventLog();

            try
            {
                if (!EventLog.SourceExists(eventSource)) EventLog.CreateEventSource(eventSource, "Application");

                message.Append(System.DateTime.Now.ToString("dd-MMMyyyy")).Append(" ").Append(DateTime.Now.ToString("hh:mm:ss tt")).Append(Environment.NewLine);

                foreach (string s in messageEventArgs) message.Append(s).Append(Environment.NewLine);

                logEvent.Log = ""; //eventName;
                logEvent.Source = eventSource;
                logEvent.MachineName = Environment.MachineName.ToString();

                logEvent.WriteEntry(message.ToString(), (EventLogEntryType)messageEventType, eventID, eventCategory);
            }

            catch (Exception ex)
            {
                // Error de programa.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;
            }

            return;
        }

        /// <summary> Generar un evento en el registro de eventos.
        /// Version 32 Bits.
        /// </summary>
        /// 
        /// <param name="eventName"> Nombre del evento. </param>
        /// <param name="eventSource"> Origen del evento. </param>
        /// <param name="eventID"> ID del evento a registrar. </param>
        /// <param name="eventCategory"> Categoría del evento a registrar. </param>
        /// <param name="messageEventType"> Tipo de mensaje. </param>
        /// <param name="message"> Mensaje a registrar. </param>
        /// 
        /// <returns></returns>
        /// 
        public void RegistrarEvento32(string eventName,
                                    string eventSource,
                                    int eventID,
                                    short eventCategory,
                                    int messageEventType,
                                    string message)
        {
            string[] messageParaArgs = { message };

            RegistrarEvento(eventName, eventSource, eventID, eventCategory, messageEventType, messageParaArgs);
        }
    }
};
