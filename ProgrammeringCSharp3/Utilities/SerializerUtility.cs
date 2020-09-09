using System;
using System.IO;
using System.Xml.Serialization;

namespace Utilities
{
    public class SerializerUtility
    {
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
