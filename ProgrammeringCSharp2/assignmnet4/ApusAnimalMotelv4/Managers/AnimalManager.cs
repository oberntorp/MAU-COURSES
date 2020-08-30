using ApusAnimalMotel.Mammals;
using ApusAnimalMotel.Subclasses.Birds;
using ApusAnimalMotel.Subclasses.Dogs;
using ApusAnimalMotel.Subclasses.Horses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApusAnimalMotel.Managers
{
    /// <summary>
    /// Manager of the Animals, inherits its methods from ListManager
    /// </summary>
    public class AnimalManager: ListManager<Animal>
    {
        private int IdOfAnimal = 1;
        /// <summary>
        /// AnimalManagers default constructor
        /// </summary>
        public AnimalManager()
        {
        }

        /// <summary>
        /// As an animal shall have an Id, this method first adds that it, then calls Add from ListManager
        /// </summary>
        /// <param name="animalToAdd"> the animal to add</param>
        /// <returns></returns>
        public bool AddAnimal(Animal animalToAdd)
        {
            AddIdToAnimal(ref animalToAdd);
            return Add(animalToAdd);
        }

        /// <summary>
        /// This method adds an id to the animal
        /// </summary>
        /// <param name="animalToAddAnId">animal to assign id, that is passed by reference</param>
        private void AddIdToAnimal(ref Animal animalToAddAnId)
        {
            if (Count == 0)
            {
                animalToAddAnId.Id = IdOfAnimal++;
            }
            else
            {
                // If importing animals from file, this is needed as new animals will get id starting at 1 otherwise
                animalToAddAnId.Id = Count + 1;
            }
        }
    }
}