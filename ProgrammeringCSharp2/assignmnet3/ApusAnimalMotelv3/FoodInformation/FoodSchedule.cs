using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.FoodInformation
{
    public class FoodSchedule
    {
        List<string> foodDescriptionList = null;

        public int FoodDefinitionListCount
        {
            get
            {
                return foodDescriptionList.Count;
            }
        }

        /// <summary>
        /// Constructor of FoodSchedule, it instansiates the foodScheduleList
        /// </summary>
        public FoodSchedule()
        {
            foodDescriptionList = new List<string>();
        }

        /// <summary>
        /// Constructor of FoodSchedule, it copies the list to the foodDescriptionlist
        /// </summary>
        public FoodSchedule(List<string> foodList)
        {
            foodDescriptionList = foodList;
        }

        /// <summary>
        /// Adds an item to foodScheduleList
        /// </summary>
        /// <param name="itemToAdd">item added to the list</param>
        public void Add(string itemToAdd)
        {
            foodDescriptionList.Add(itemToAdd);
        }

        /// <summary>
        /// Removes an item from foodScheduleList
        /// </summary>
        /// <param name="indexOfItemToRemove">index of the item to remove from the list</param>
        public void RemoveFoodScheduleAt(int indexOfItemToRemove)
        {
            if (CheckIfIndexValid(indexOfItemToRemove))
                foodDescriptionList.RemoveAt(indexOfItemToRemove);
        }

        /// <summary>
        /// Check that the index is valid
        /// </summary>
        /// <param name="index">index to check</param>
        /// <returns></returns>
        private bool CheckIfIndexValid(int index)
        {
            return (index >= 0) && (index < foodDescriptionList.Count);
        }

        /// <summary>
        /// Change the foodSchedule at a specified index
        /// </summary>
        /// <param name="newStringValue">the new foodDescription</param>
        /// <param name="index">what index to change</param>
        /// <returns>if feedinformation was updated</returns>
        public bool ChangeFoodScheduleAt(string newStringValue, int index)
        {
            string oldValue = "";
            if (CheckIfIndexValid(index) && IsValueNotEmpty(newStringValue))
            {
                oldValue = foodDescriptionList[index];
                foodDescriptionList[index] = newStringValue;
            }

            return IsValueUpdated(oldValue, foodDescriptionList[index]);
        }

        /// <summary>
        /// Describe that no feeding information is required
        /// </summary>
        /// <returns></returns>
        public string NoFeedingInformationRequired()
        {
            return "No feedinginformation required";
        }

        /// <summary>
        /// Checks that a specified value to add is not empty
        /// </summary>
        /// <param name="newStringValue"></param>
        /// <returns></returns>
        private bool IsValueNotEmpty(string newStringValue)
        {
            return newStringValue != string.Empty;
        }

        /// <summary>
        /// Checks if the value of foodSchedule has been updated
        /// </summary>
        /// <param name="oldValue">old value, to compare new value to</param>
        /// <param name="newValue">the new value</param>
        /// <returns>if the values are valid, check if they differ, else false</returns>
        private bool IsValueUpdated(string oldValue, string newValue)
        {
            return (IsValueNotEmpty(oldValue)) ? oldValue != newValue : false;
        }

        /// <summary>
        /// Get a foodSchedule item on a specified index
        /// </summary>
        /// <param name="index">index at whitch to get food description item</param>
        /// <returns>food description item as string</returns>
        public string GetFoodScheduleAt(int index)
        {
            return foodDescriptionList[index];
        }

        /// <summary>
        /// Get all foodDescription items
        /// </summary>
        /// <returns>the full list of food description items</returns>
        public List<string> GetFoodScheduleAllItems()
        {
            return foodDescriptionList;
        }

        /// <summary>
        /// Print out information about the foodDescriptionList
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = string.Empty;
            output += "Food schedule for this animal is as follows: \n\n";
            for(int i = 0; i < foodDescriptionList.Count; i++)
                output += $"{i}: {foodDescriptionList[i]}";

            return output;
        }
    }
}
