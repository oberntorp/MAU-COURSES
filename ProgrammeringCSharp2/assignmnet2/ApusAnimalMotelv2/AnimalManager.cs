using ApusAnimalMotel.Mammals;
using ApusAnimalMotel.Subclasses.Birds;
using ApusAnimalMotel.Subclasses.Dogs;
using ApusAnimalMotel.Subclasses.Horses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApusAnimalMotel
{
    /// <summary>
    /// The class handling add/delete to the registry
    /// </summary>
    public class AnimalManager
    {
        List<Animal> animalList = null;

        public int ElementsOfAnimalList
        {
            get
            {
                return animalList.Count;
            }
        }

        /// <summary>
        /// Constructor of the AnimalManager, it only sets the List of animals
        /// </summary>
        public AnimalManager()
        {
            animalList = new List<Animal>();
        }

        /// <summary>
        /// Adds an animal, if any
        /// </summary>
        /// <param name="animalToAdd">the animal being added</param>
        public void Add(Animal animalToAdd)
        {
            if (animalToAdd != null)
            {
                animalToAdd.Id = GenerateIndexOfAnimal();
                animalList.Add(animalToAdd);
            }
        }

        /// <summary>
        /// Generates an index for the animal
        /// </summary>
        /// <returns>The index for the animal</returns>
        public int GenerateIndexOfAnimal()
        {
            return ElementsOfAnimalList + 1;
        }

        /// <summary>
        /// Get animals at a given index in the list, if the index given is valid
        /// Invocing different methods to get different species
        /// </summary>
        /// <param name="index">index to get the animal at</param>
        /// <returns>An animal</returns>
        public Animal GetAnimalAt(int index)
        {
            if (CheckIfIndexValid(index))
            {
                return animalList[index];
            }

            return null;
        }

        /// <summary>
        /// Check that the index is valid
        /// </summary>
        /// <param name="index">index to check</param>
        /// <returns></returns>
        private bool CheckIfIndexValid(int index)
        {
            return (index >= 0) && (index < animalList.Count);
        }

        /// <summary>
        /// Removes an animal at an index (if valid)
        /// </summary>
        /// <param name="indexOfItemToRemove">index at wich the object to remove is</param>
        public void RemoveAnimalAt(int indexOfItemToRemove)
        {
            if (CheckIfIndexValid(indexOfItemToRemove))
                animalList.RemoveAt(indexOfItemToRemove);
        }
    }
}