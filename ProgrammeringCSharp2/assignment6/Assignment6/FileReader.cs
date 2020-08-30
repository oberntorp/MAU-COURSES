using Assignment6.InvoiceInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    /// <summary>
    /// This class handles Reading and Trimming information from fille that has been read
    /// </summary>
    class FileReader
    {
        private InvoiceMapper mapper;

        /// <summary>
        /// Constructor, getting an invoiceMapper object from the main window
        /// </summary>
        /// <param name="invoiceMapper">invoice mapper to use when reading files</param>
        public FileReader(InvoiceMapper invoiceMapper)
        {
            mapper = invoiceMapper;
        }

        /// <summary>
        /// Reads a file
        /// </summary>
        /// <param name="pathOfFile">filePath to read from</param>
        /// <returns></returns>
        public string[] ReadFile(string pathOfFile)
        {
            return TrimFileLines(File.ReadAllLines(pathOfFile).ToList());
        }

        /// <summary>
        /// THe file may contain newlines and spaces, to make life easier, get rid of these
        /// </summary>
        /// <param name="fileLinesToProcess">fileLines to process</param>
        /// <returns></returns>
        private string[] TrimFileLines(List<string> fileLinesToProcess)
        {
            for(int i = fileLinesToProcess.Count()-1; i >= 0; i--)
            {
                if(string.IsNullOrEmpty(fileLinesToProcess[i]))
                {
                    fileLinesToProcess.RemoveAt(i);
                }
                else
                {
                    fileLinesToProcess[i] = fileLinesToProcess[i].Trim();
                }
            }

            return fileLinesToProcess.ToArray();
        }
    }
}
