
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VISOR
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWSVisor
    {
        /// <summary>
        /// 
        /// </summary>
        void RegistrarEvento(string eventName,
                                    string eventSource,
                                    int eventID,
                                    short eventCategory,
                                    int messageEventType,
                                    params string[] messageEventArgs);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IWSVisor2
    {
        /// <summary>
        /// 
        /// </summary>
        void RegistrarEvento2(string eventName,
                                    string eventSource,
                                    int eventID,
                                    short eventCategory,
                                    int messageEventType,
                                    params string[] messageEventArgs);
    }

}
