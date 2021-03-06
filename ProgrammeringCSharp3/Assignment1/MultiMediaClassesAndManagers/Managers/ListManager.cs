﻿using MultiMediaClassesAndManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiMediaClassesAndManagers.Managers
{
    /// <summary>
    /// ListManager is a generic class that now handles all operations of all Managers
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    public class ListManager<T> : IListManager<T>
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
            int countBeforeAdd = Count;
            if (itemToAdd != null)
            {
                objectsInList.Add(itemToAdd);
            }

            return WasAddSuccessfull(countBeforeAdd);
        }

        /// <summary>
        /// Checks that add was successfull
        /// </summary>
        /// <param name="countBeforeAdd">count before insert to compare too if less, then an item was added</param>
        /// <returns>true/false reflecting success</returns>
        private bool WasAddSuccessfull(int countBeforeAdd)
        {
            return Count > countBeforeAdd;
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
        /// <returns>true/false reflecting Success</returns>
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
        /// <param name="indexToGetAt">index of item to get (where located in list)</param>
        /// <returns></returns>
        public T GetAt(int indexToGetAt)
        {
            return objectsInList[indexToGetAt];
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
        /// <param name="filePath">Path where to get XML from</param>
        public void XMLDeserialize(string filePath)
        {
            AddDeserializedObjectsToList(Utilities.SerializerUtility.DeserializeXMLFile<List<T>>(filePath));
        }

        /// <summary>
        /// Adds the deserialized items to the listManager
        /// </summary>
        /// <param name="deserializedList">list of deserialized items to save</param>
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

        /// <summary>
        /// Get all items of internal list
        /// </summary>
        /// <returns>List of items</returns>
        public List<T> GetAllItems()
        {
            return objectsInList;
        }
    }
}
