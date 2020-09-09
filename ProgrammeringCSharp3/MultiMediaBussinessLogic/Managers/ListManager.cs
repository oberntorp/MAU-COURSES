using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// ListManager is a generic class that now handles all operations of all Managers
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    class ListManager<T> : IListManager<T>
    {
        List<T> objectsInList = null;
        public int Count => objectsInList.Count();

        /// <summary>
        /// Initialize the list in the ListManagers constructor
        /// </summary>
        public ListManager()
        {
            objectsInList = new List<T>();
        }

        /// <summary>
        /// Adds an itemn to the list
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <returns>true/false reflecting success</returns>
        public bool Add(T itemToAdd)
        {
            int countBeforeInsert = Count;
            if (itemToAdd != null)
            {
                objectsInList.Add(itemToAdd);
            }

            return WasAddSuccessfull(countBeforeInsert);
        }

        /// <summary>
        /// Checks that add was successfull
        /// </summary>
        /// <param name="countBeforeInsert">count before insert to compare too if less, then an item was added</param>
        /// <returns>true/false reflecting success</returns>
        private bool WasAddSuccessfull(int countBeforeInsert)
        {
            return Count > countBeforeInsert;
        }

        /// <summary>
        /// Change an item at a given position
        /// </summary>
        /// <param name="changedItem">item being changed</param>
        /// <param name="indexToChangeAt">at what index the changed item is inserted</param>
        /// <returns>true/false reflecting success</returns>
        public bool ChangeAt(T changedItem, int indexToChangeAt)
        {
            T oldValue = objectsInList.ElementAt(indexToChangeAt);
            if (CheckIndex(indexToChangeAt) && changedItem != null)
            {
                objectsInList[indexToChangeAt] = changedItem;
            }

            return WasChangeSuccessfull(indexToChangeAt, oldValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedAt">index of index to check for successfull change</param>
        /// <param name="oldValue">the old value before change, used to check that a change has really happened</param>
        /// <returns>true/false reflecting success</returns>
        private bool WasChangeSuccessfull(int changedAt, T oldValue)
        {
            return objectsInList[changedAt].ToString() != oldValue.ToString();
        }

        /// <summary>
        /// Checks that an index exists
        /// </summary>
        /// <param name="index">the index to check that it exists</param>
        /// <returns>true/false reflecting success</returns>
        public bool CheckIndex(int index)
        {
            return (index >= 0) && (index < objectsInList.Count);
        }

        /// <summary>
        /// Delete all items from the ListManager
        /// </summary>
        public void DeleteAll()
        {
            objectsInList.Clear();
        }

        /// <summary>
        /// Delete an item at a specified index
        /// </summary>
        /// <param name="indexToDeleteAt"></param>
        /// <returns></returns>
        public bool DeleteAt(int indexToDeleteAt)
        {
            int countBeforeDelete = Count;
            if (CheckIndex(indexToDeleteAt))
            {
                objectsInList.RemoveAt(indexToDeleteAt);
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
        /// Get an object from the list at a specified index
        /// </summary>
        /// <param name="indexToGetAt"></param>
        /// <returns></returns>
        public T GetAt(int indexToGetAt)
        {
            return objectsInList[indexToGetAt];
        }

        public string[] ToStringArray()
        {
            throw new NotImplementedException();
        }

        public List<string> ToStringList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serializes the items in the listmanager
        /// </summary>
        /// <param name="filePath">´path at which the xml file is to be saved</param>
        public void XMLSerialize(string filePath)
        {
            Utilities.SerializerUtility.SerializeXMLFile(filePath, objectsInList);
        }

        /// <summary>
        /// Deserializes an xml file back into the listmanager items
        /// </summary>
        /// <param name="fileName"></param>
        public void XMLDeserialize(string fileName)
        {
            AddDeserializedObjectsToList(Utilities.SerializerUtility.DeserializeXMLFile<List<T>>(fileName));
        }

        /// <summary>
        /// Adds the deserialized items to the listManager
        /// </summary>
        /// <param name="deserializedList"></param>
        private void AddDeserializedObjectsToList(List<T> deserializedList)
        {
            if (deserializedList.Count > 0)
            {
                foreach (T deserializedListItem in deserializedList)
                {
                    Add(deserializedListItem);
                }
            }
        }
    }
}
