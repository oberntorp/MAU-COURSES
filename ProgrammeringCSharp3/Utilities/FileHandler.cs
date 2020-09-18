using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class FileHandler
    {
        public static string GetFileName(string filePath)
        {
            return filePath.Split('\\').Last();
        }

        public static string GetFileExtension(string fileName)
        {
            return fileName.Split('.').Last();
        }
    }
}
