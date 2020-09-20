using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// A small helper class for getting the file name and file extention of files
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Get a files file name througth the files path
        /// </summary>
        /// <param name="filePath">the path of the file</param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            return filePath.Split('\\').Last().Split('.').First();
        }

        /// <summary>
        /// Get a files file extention, througth the files file name
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        public static string GetFileExtension(string filePath)
        {

            return filePath.Split('\\').Last().Split('.').Last();
        }
    }
}
