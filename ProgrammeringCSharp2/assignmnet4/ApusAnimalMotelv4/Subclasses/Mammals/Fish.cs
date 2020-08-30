using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Fish;
using ApusAnimalMotel.FoodInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApusAnimalMotel.Mammals
{
    /// <summary>
    /// Basic object of a fish
    /// </summary>
    [Serializable]
    public abstract class Fish : Animal
    {
        public FishFamily FishFamily { get; set; }

        public int NumberOfFins { get; set; }

        /// <summary>
        /// Animal constructor, passed to it is the property values
        /// This constructor calls the Animal constructor (as a fish is an animal)
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        /// <param name="fishFamily">What fish family the fish belongs to</param>
        public Fish(string name, int age, Gender gender, FishFamily fishFamily, int numberOfFins) : base(name, age, gender)
        {
            FishFamily = fishFamily;
            NumberOfFins = numberOfFins;
        }

        /// <summary>
        /// ToString method with the basic characteristics of a fish
        /// </summary>
        /// <returns>the basic characteristics of a fish</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {FishFamily} - {NumberOfFins}";
        }
    }
}