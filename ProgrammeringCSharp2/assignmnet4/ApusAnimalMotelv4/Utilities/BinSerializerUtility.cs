using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApusAnimalMotel.Utilities
{
    class BinSerializerUtility
    {
        /// <summary>
        /// Serializes an object to an binary file
        /// </summary>
        /// <param name="filePath">file path of file to save data</param>
        /// <param name="animalToSerialize">Animl object to serialize</param>
        public static void SerializeBinaryFile<T>(string filePath, T animalToSerialize)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Create);
                BinaryFormatter binarFormatter = new BinaryFormatter();
                binarFormatter.Serialize(fileStream, animalToSerialize);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// Deserializes an object from an binary file
        /// </summary>
        /// <param name="filePath">file path of file to save data</param>
        /// <returns>Deserialized object</returns>
        public static T DeserializeBinaryFile<T>(string filePath)
        {
            object result = null;
            FileStream fileStream = null;
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The supplied file {filePath} could not be found.");
                }
                else
                {
                    fileStream = new FileStream(filePath, FileMode.Open);
                    BinaryFormatter binarFormatter = new BinaryFormatter();
                    result = (T)binarFormatter.Deserialize(fileStream);
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return (T)result;
        }
    }
}
