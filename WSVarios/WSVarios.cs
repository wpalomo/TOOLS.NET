
//------------------------------------------------------------------
// Herramientas varias de uso general.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace VARIOS
{
    /// <summary> Funciones varias de uso general. </summary>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSVarios")]

    public class WSVarios : TOOLS._Tools
    { 
        /// <summary> Conversión automática de tipos. Versión con función estática. </summary>
        /// 
        /// <param name="o"> Variable de entrada a convertir. </param>
        /// 
        /// <returns> Variable convertida al nuevo tipo. </returns>
        /// 
        /// <remarks> Al declararse como estática no necesita ser instanciada. </remarks>
        /// 
        static public TipoNuevo ConvertirTipoS<TipoNuevo>(object o)
        {
            TipoNuevo oOut;

            try
            {
                oOut = (TipoNuevo)Convert.ChangeType(o, typeof(TipoNuevo));
            }

            catch
            {
                oOut = (TipoNuevo)Activator.CreateInstance(typeof(TipoNuevo));
            }

            return oOut;
        }

        /// <summary> Conversión automática de tipos. </summary>
        /// 
        /// <typeparam name="TipoNuevo"> Tipo destino del objeto recibido. </typeparam>
        /// 
        public class GenericConversion<TipoNuevo>
        {
            /// <summary> Conversión automática de tipos. </summary>
            /// 
            /// <param name="o"> Variable de entrada a convertir. </param>
            /// 
            /// <returns> Variable convertida al nuevo tipo. </returns>
            /// 
            public TipoNuevo ConvertirTipo(object o)
            {
                TipoNuevo oOut;

                try
                {
                    oOut = (TipoNuevo)Convert.ChangeType(o, typeof(TipoNuevo));
                }

                catch
                {
                    // Error en conversión de tipos.
                    oOut = (TipoNuevo)Activator.CreateInstance(typeof(TipoNuevo));
                }

                return oOut;
            }
        }

        /// <summary> Gestión de pilas LIFO. </summary>
        /// 
        /// <typeparam name="CualquierTipo"></typeparam>
        /// 
        public class GenericStack<CualquierTipo>
        {
            /// <summary> Devuelve el número de elementos de la pila. </summary>
            public int Elementos
            {
                get { return m_data.Count; }
            }

            /// <summary> Pila de objetos LIFO </summary>
            private Stack<CualquierTipo> m_data = new Stack<CualquierTipo>();

            /// <summary> Añadir un objeto a la pila. </summary>
            /// 
            /// <param name="data"> Objeto a agregar a la pila. </param>
            /// 
            /// <returns> Nº de objetos de la pila. </returns>
            /// 
            public int Push(CualquierTipo data)
            {
                m_data.Push(data);
                return m_data.Count;
            }

            /// <summary> Devolver el elemento superior de la pila. </summary>
            /// 
            /// <returns> Elemento superior de la pila. </returns>
            /// 
            public CualquierTipo Pop()
            {
                return m_data.Pop();
            }
        }

        /// <summary> Gestión de pilas FIFO. </summary>
        /// 
        /// <typeparam name="CualquierTipo"></typeparam>
        /// 
        public class GenericQueue<CualquierTipo>
        {
            /// <summary> Devuelve el número de elementos de la cola. </summary>
            public int Elementos
            {
                get { return m_data.Count; }
            }

            /// <summary> Pila de objetos LIFO </summary>
            private Queue<CualquierTipo> m_data = new Queue<CualquierTipo>();

            /// <summary> Añadir un objeto a la pila. </summary>
            /// 
            /// <param name="data"> Objeto a agregar a la pila. </param>
            /// 
            /// <returns> Nº de objetos de la pila. </returns>
            /// 
            public int Push(CualquierTipo data)
            {
                m_data.Enqueue(data);

                return m_data.Count;
            }

            /// <summary>Devolver el elemento inferior de la pila </summary>
            /// 
            /// <returns> Elemento inferior de la pila. </returns>
            /// 
            public CualquierTipo Pop()
            {
                return m_data.Dequeue();
            }
        }
    }
}
