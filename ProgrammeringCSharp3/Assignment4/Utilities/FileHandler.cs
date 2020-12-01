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
            CheckforErrors(filePath.Split('\\').Last());
            return filePath.Split('\\').Last().Split('.').First();
        }

        /// <summary>
        /// Get a files file extention, througth the files file name
        /// </summary>
        /// <param name="filePath">The file name</param>
        /// <returns>the file extention as a string</returns>
        public static string GetFileExtension(string filePath)
        {
            CheckforErrors(filePath.Split('\\').Last());
            return filePath.Split('\\').Last().Split('.').Last();
        }

        /// <summary>
        /// Check for errors on file name, if found, throw errors
        /// </summary>
        /// <param name="filePath">The filePath to check for errors</param>
        private static void CheckforErrors(string filePath)
        {
            if (filePath == string.Empty)
            {
                throw new ArgumentException("No file path specified, can´t retreive file name");
            }
            if (!filePath.Contains("."))
            {
                throw new ArgumentException("Please check the file name, it is not possible to retreive the file name");
            }
            if (filePath.Contains(".") && !IsFileNameComplete(filePath))
            {
                throw new ArgumentException("Please check the file name, it is not possible to retreive the file name, either no name or file extention");
            }
        }

        /// <summary>
        /// Checks that the filePath contains file name and file extention
        /// </summary>
        /// <param name="filePath">The filePath to check for errors</param>
        /// <returns></returns>
        private static bool IsFileNameComplete(string filePath)
        {
            return filePath.Split('.')[0] != string.Empty && filePath.Split('.')[1] != string.Empty;
        }
    }
}
