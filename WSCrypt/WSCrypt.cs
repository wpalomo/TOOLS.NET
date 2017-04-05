
//------------------------------------------------------------------
// Gestion de encriptado / desencriptado.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;

namespace CRYPT
{
    /// <summary> Funciones para encriptar / desencriptar cadenas. </summary>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSCrypt")]

    public class WSCrypt : TOOLS._Tools
    {
        /// <summary> Encripta la cadena que le envíamos en el parámetro de entrada. </summary>
        /// 
        /// <param name="_cadenaAencriptar"></param>
        /// 
        /// <returns></returns>
        /// 
        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);

            return result;
        }

        /// <summary> Desencripta la cadena que le envíamos en el parámetro de entrada. </summary>
        /// 
        /// <param name="_cadenaAdesencriptar"></param>
        /// 
        /// <returns></returns>
        /// 
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);

            return result;
        }
    }
}
