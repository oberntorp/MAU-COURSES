using ApusAnimalMotel.Interfaces;
using ApusAnimalMotel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.Managers
{
    /// <summary>
    /// ListManager is a generic class that now handles all operations of all Managers
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    public class ListManager<T> : IListManager<T>
    {

        List<T> objects = null;

        public int Count => objects.Count;

        /// <summary>
        /// Constructor of ListManager, it instanciates objects
        /// </summary>
        public ListManager()
        {
            objects = new List<T>();
        }

        /// <summary>
        /// Adds an Item to the ListManager
        /// </summary>
        /// <param name="itemToAdd">The item to add</param>
        /// <returns>True/False, if successfull</returns>
        public bool Add(T itemToAdd)
        {
            int countBeforeInsert = Count;
            if (itemToAdd != null)
            {
                objects.Add(itemToAdd);
            }

            return WasAddSuccessfull(countBeforeInsert);
        }

        /// <summary>
        /// Checks if an Item was added currctly to the ListManager
        /// </summary>
        /// <param name="itemToAdd">The item to add</param>
        /// <returns>True/False, if successfull</returns>
        private bool WasAddSuccessfull(int countBeforeInsert)
        {
            return Count > countBeforeInsert;
        }

        /// <summary>
        /// Changes an item in ListManager at a specified index
        /// </summary>
        /// <param name="changedItem">the item that was changed</param>
        /// <param name="indexToChangeAt">was index to change in the ListManager</param>
        /// <returns>True/False if successfull</returns>
        public bool ChangeAt(T changedItem, int indexToChangeAt)
        {
            T oldValue = objects.ElementAt(indexToChangeAt);
            if (CheckIndex(indexToChangeAt) && changedItem != null)
            {
                objects[indexToChangeAt] = changedItem;
            }

            return WasChangeSuccessfull(indexToChangeAt, oldValue);
        }

        /// <summary>
        /// Checks that the change was done successfully
        /// </summary>
        /// <param name="changedAt">At wat index the change occured</param>
        /// <param name="oldValue">the oldValue, it is use to check that the change was successfull</param>
        /// <returns>True/False if successfull</returns>
        private bool WasChangeSuccessfull(int changedAt, T oldValue)
        {
            return objects[changedAt].ToString() != oldValue.ToString();
        }

        /// <summary>
        /// Checks that the given index is valid
        /// </summary>
        /// <param name="index">the index to change</param>
        /// <returns>True/False if valid</returns>
        public bool CheckIndex(int index)
        {
            return (index >= 0) && (index < objects.Count);
        }

        public void DeleteAll()
        {
            objects.Clear();
        }

        public bool DeleteAt(int indexToDeleteAt)
        {
            int countBeforeDelete = Count;
            if (CheckIndex(indexToDeleteAt))
            {
                objects.RemoveAt(indexToDeleteAt);
            }

            return WasDeleteSuccessfull(countBeforeDelete);
        }

        /// <summary>
        /// Checks that the delete was done successfully, by checking the Count before deletion against after deletion
        /// </summary>
        /// <param name="countBeforeDelete">The number of item in the ListManager before deletion</param>
        /// <returns>True/False if successfull</returns>
        private bool WasDeleteSuccessfull(int countBeforeDelete)
        {
            return countBeforeDelete > Count;
        }

        /// <summary>
        /// Gets oan item hold by the ListManager at a specific index
        /// </summary>
        /// <param name="indexToGetAt">the indexto get the item from</param>
        /// <returns></returns>
        public T GetAt(int indexToGetAt)
        {
            return objects[indexToGetAt];
        }

        /// <summary>
        /// Gets all items of ListManager and returns them as an Array of strings
        /// </summary>
        /// <returns>The items of the ListManager as an Array of strings</returns>
        public string[] ToStringArray()
        {
            string[] stringArrayToReturn = new string[Count];

            for (int i = 0; i < Count; i++)
            {
                stringArrayToReturn[i] = GetAt(i).ToString();
            }

            return stringArrayToReturn;
        }

        /// <summary>
        /// Gets all items of ListManager and returns them as an List of strings
        /// </summary>
        /// <returns>The items of the ListManager as a List of strings</returns>
        public List<string> ToStringList()
        {
            List<string> stringListToReturn = new List<string>();

            for (int i = 0; i < Count; i++)
            {
                stringListToReturn.Add(GetAt(i).ToString());
            }

            return stringListToReturn;
        }

        /// <summary>
        /// Calls SerializeBinaryFileto serialize data to a binary file
        /// </summary>
        /// <param name="filePath">path of file to save data to</param>
        public void BinarySerialize(string filePath)
        {
            BinSerializerUtility.SerializeBinaryFile(filePath, objects);
        }

        /// <summary>
        /// Calls BinaryDeserialize to deserialize data from an binary file
        /// </summary>
        /// <param name="filePath">path of file to save data to</param>
        public void BinaryDeserialize(string filePath)
        {
            List<T> deserializedList = AddDeserializedObjectsToListManager(BinSerializerUtility.DeserializeBinaryFile<List<T>>(filePath));
        }

        /// <summary>
        /// Calls SerializeXMLFile to serialize data to an xml file
        /// </summary>
        /// <param name="filePath">path of file to save data to</param>
        public void XMLSerialize(string filePath)
        {
            XMLSerializerUtility.SerializeXMLFile(filePath, objects);
        }

        /// <summary>
        /// Calls DeserializeXMLFile to serialize data from an xml file
        /// </summary>
        /// <param name="filePath">path of file to save data to</param>
        /// <returns></returns>
        public void XMLDeserialize(string filePath)
        {
            List<T> deserializedList = AddDeserializedObjectsToListManager(XMLSerializerUtility.DeserializeXMLFile<List<T>>(filePath));
        }

        /// <summary>
        /// Adds a deserialized list to the listmanager
        /// </summary>
        /// <param name="deserializedList"></param>
        private List<T> AddDeserializedObjectsToListManager(List<T> deserializedList)
        {
            if (deserializedList.Count > 0)
            {
                foreach (T deserializedListItem in deserializedList)
                {
                    Add(deserializedListItem);
                }
            }

            return deserializedList;
        }
    }
}
