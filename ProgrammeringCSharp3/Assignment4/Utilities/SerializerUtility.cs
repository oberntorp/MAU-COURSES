using System;
using System.IO;
using System.Xml.Serialization;

namespace Utilities
{
    /// <summary>
    /// Load and Saves classes to XML files
    /// </summary>
    public class SerializerUtility
    {
        /// <summary>
        /// Serialize the given object to xml
        /// </summary>
        /// <typeparam name="T">The type of object being serialized</typeparam>
        /// <param name="filePathOfSavedFile">Where to save the XML file</param>
        /// <param name="objectToSerialize">The object being saved in XML</param>
        public static void SerializeXMLFile<T>(string filePathOfSavedFile, T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(filePathOfSavedFile);

            try
            {
                xmlSerializer.Serialize(textWriter, objectToSerialize);
            }
            finally
            {
                textWriter?.Close();
            }
        }

        /// <summary>
        /// Serialize the given object from xml
        /// </summary>
        /// <typeparam name="T">The type of object being deserialized</typeparam>
        /// <param name="filePathOfSavedFile">Where the file to deserialixe is stored</param>
        /// <returns>THe object that has been the result of the deserialization</returns>
        public static T DeserializeXMLFile<T>(string filePathOfSavedFile)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(filePathOfSavedFile);

            try
            {

                if (!File.Exists(filePathOfSavedFile))
                {
                    throw new FileNotFoundException($"The supplied file {filePathOfSavedFile} could not be found.");
                }
                else
                {
                    return (T)xmlSerializer.Deserialize(textReader);
                }
            }
            finally
            {
                textReader?.Close();
            }
        }
    }
}
