
//------------------------------------------------------------------
// Gestión de empaquetado / desempaquetado de ficheros.
//------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Compression;

namespace ZIP
{
    /// <summary> Funciones para comprimir / descomprimir archivos. </summary>
    /// 
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("TOOLS.WSZip")]

    public class WSZip : TOOLS._Tools
    {
        //------------------------------------------------------------------
        // Propiedades públicas de la clase.
        //------------------------------------------------------------------

        /// <summary> Comprimir un archivo. </summary>
        /// 
        /// <param name="pathFileToCompress"> Path del archivo a comprimir. </param>
        /// <param name="pathCompressedFile"> Path del archivo comprimido. </param>
        /// <param name="createFile"> Crear fichero destino. </param>
        /// <param name="CompressionLvl"> Nivel de compresión. </param>
        /// <param name="BaseDir"> Incluir el directorio base. </param>
        /// 
        /// <returns> Resultado (S/N/C). </returns>
        /// 
        public Char SimpleCompress(string pathFileToCompress,
                                   string pathCompressedFile,
                                   bool createFile = false,
                                   int CompressionLvl = (int)CompressionLevel.Fastest,
                                   bool BaseDir = false)
        {
            Char resultado = 'S';
            FileAttributes atributos;

            try
            {
                UsrError = "OK";
                UsrErrorC = 1;
                UsrErrorE = "";

                atributos = File.GetAttributes(pathFileToCompress);
                if (atributos.HasFlag(FileAttributes.Directory))
                {
                    // Es un directorio.
                    if (System.IO.Path.GetDirectoryName(pathFileToCompress)==System.IO.Path.GetDirectoryName(pathCompressedFile))
                    {
                        // Si origen es un directorio no puede ser el mismo que el del Zip destino.
                        UsrError = "KO - Origen y destino NO pueden estar en la misma carpeta";
                        UsrErrorC = -1;
                        resultado = 'N';

                        return resultado;
                    }

                    if (System.IO.File.Exists(pathCompressedFile) && createFile==true)
                        System.IO.File.Delete(pathCompressedFile);

                    ZipFile.CreateFromDirectory(pathFileToCompress, pathCompressedFile, (CompressionLevel)CompressionLvl, BaseDir);
                }
                else
                {
                    // Es un fichero.
                    if (System.IO.File.Exists(pathFileToCompress) == false)
                    {
                        UsrError = String.Format("KO - No existe el fichero {0} ", pathFileToCompress);
                        UsrErrorC = -2;
                        resultado = 'N';

                        return resultado;
                    }

                    ZipArchive FileZip = ZipFile.Open(pathCompressedFile, ZipArchiveMode.Update);
                    FileZip.CreateEntryFromFile(pathFileToCompress, Path.GetFileName(pathFileToCompress), (CompressionLevel)CompressionLvl);
                    FileZip.Dispose();
                }
            }

            catch (Exception ex)
            {
                // Error en Zip / UnZip.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                resultado = 'C';
            }

            return resultado;
        }

        /// <summary> </summary>
        /// 
        /// <param name="pathCompressedFile"></param>
        /// <param name="createFile"> Crear fichero destino. </param>
        /// <param name="CompressionLvl"> Nivel de compresión. </param>
        /// <param name="ListaDeFicheros"> Lista de ficheros / carpetas a comprimir. </param>
        /// 
        /// <returns></returns>
        /// 
        public Char FullCompress(string pathCompressedFile,
                                 bool createFile = false,
                                 int CompressionLvl = (int)CompressionLevel.Fastest,
                                 params object [] ListaDeFicheros)
        {
            Char resultado = 'N';

            ZipArchive FileZip;
            string currentFile;
            string currentDirectory;
            int totalArchivosProcesados = 0;

            try
            {
                UsrError = "KO";
                UsrErrorC = 0;
                UsrErrorE = "";

                if (ListaDeFicheros.Count()<=0)
                {
                    // Sin archivos a comprimir.
                    UsrError = "KO - No hay archivos a comprimir";
                    UsrErrorC = -1;
                    resultado = 'N';

                    return resultado;
                }

                // Eliminar, si cal, el fichero Zip destino.
                if (System.IO.File.Exists(pathCompressedFile) && createFile == true)
                    System.IO.File.Delete(pathCompressedFile);

                FileZip = ZipFile.Open(pathCompressedFile, ZipArchiveMode.Update);

                for (int i = 0; i< ListaDeFicheros.Length; i++)
                {
                    currentDirectory = Path.GetDirectoryName((string)ListaDeFicheros[i]);
                    currentFile = Path.GetFileName((string)ListaDeFicheros[i]);

                    // Agregar la carpeta de trabajo si el parámetros contiene solo el nombre de fichero.
                    if (String.IsNullOrEmpty(currentDirectory)) currentDirectory = Directory.GetCurrentDirectory();

                    foreach(string currentFileToCompress in System.IO.Directory.GetFiles(currentDirectory, currentFile))
                    {
                        FileZip.CreateEntryFromFile(currentFileToCompress,
                                                    Path.GetFileName(currentFileToCompress),
                                                    (CompressionLevel)CompressionLvl);
                        totalArchivosProcesados += 1;
                    }
                }

                FileZip.Dispose();
                resultado = 'S';
                UsrError = string.Format("OK - Total archivos comprimidos {0}", totalArchivosProcesados);
            }

            catch (Exception ex)
            {
                // Error en Zip / UnZip.
                UsrErrorC = ex.HResult;
                UsrError = ex.Message;
                UsrErrorE = ex.StackTrace;

                resultado = 'C';
            }

            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        public WSZip()
        { }
    }
}
