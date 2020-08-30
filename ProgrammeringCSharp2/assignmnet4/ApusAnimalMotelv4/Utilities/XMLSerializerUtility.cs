using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ApusAnimalMotel.Utilities
{
    class XMLSerializerUtility
    {
        /// <summary>
        /// Serializes an object to an XML file
        /// </summary>
        /// <param name="filePath">file path of file to save data</param>
        /// <param name="objectToSerialize">Animl object to serialize</param>
        public static void SerializeXMLFile<T>(string filePath, T objectToSerialize)
        {
            XmlSerializer xmlSrializer = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(filePath);
            try
            {
                xmlSrializer.Serialize(textWriter, objectToSerialize);
            }
            finally
            {
                if (textWriter != null)
                {
                    textWriter.Close();
                }
            }
        }

        /// <summary>
        /// Deserializes an object to an XML file
        /// </summary>
        /// <param name="filePath">file path of file to save data</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeXMLFile<T>(string filePath)
        {
            object result = null;
            XmlSerializer xmlSrializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(filePath);
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The supplied file {filePath} could not be found.");
                }
                else
                {
                    result = (T)xmlSrializer.Deserialize(textReader);
                }
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }

            return (T)result;
        }
    }
}
