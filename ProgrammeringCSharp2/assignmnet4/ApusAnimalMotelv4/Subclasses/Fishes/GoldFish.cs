using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Fish;
using ApusAnimalMotel.FoodInformation;
using ApusAnimalMotel.Mammals;

namespace ApusAnimalMotel
{
    /// <summary>
    /// An species of Fish
    /// </summary>
    [Serializable]
    public class GoldFish : Fish
    {
        private FoodSchedule foodSchedule = null;
        public bool IDoForget { get; set; }

        /// <summary>
        /// GoldFish constructor, passed to it is the property values
        /// This constructor calls the fish constructor (as a GoldFish is a fish)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="fishFamily">As a goldfish is a fish, we need to pass fishFamily</param>
        /// <param name="numberOfFins">As a goldfish is a fish, we need to pass number of fins</param>
        /// <param name="iDoForget">This is a propewrty of goldfish</param>
        public GoldFish(string name, int age, Gender gender, FishFamily fishFamily, int numberOfFins, bool iDoForget) : base(name, age, gender, fishFamily, numberOfFins)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Krill every day.");
            IDoForget = iDoForget;
        }

        /// <summary>
        /// Get the EaterType of the animal
        /// </summary>
        /// <returns></returns>
        public override EaterType GetEaterType()
        {
            return EaterType.Omnivore;
        }

        /// <summary>
        /// Get species for this object
        /// </summary>
        /// <returns>Species as a string</returns>
        public override string GetSpecies()
        {
            return AnimalSpecies.Fish.ToString();
        }

        /// <summary>
        /// Get the foodschedule for this animal
        /// </summary>
        /// <returns>the food schedule of an animal</returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }

        /// <summary>
        /// Personal characteristics of a GoldFish
        /// </summary>
        /// <returns>Personal characteristics of a GoldFish as a string</returns>
        private string SwimAndForget()
        {
            return $"I am swiming {StatementAboutMyMemory()}";
        }

        /// <summary>
        /// A statement on how this fishs memory operates
        /// </summary>
        /// <returns>string</returns>
        private string StatementAboutMyMemory()
        {
            return (IDoForget) ? ", alas I have bad memory, I do not remember if I have been a place before" : "My memmory is good";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a fish as well as something personal for a GoldFish
        /// </summary>
        /// <returns>the common characteristics of a fish as well as something personal for a GoldFish</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {SwimAndForget()}";
        }
    }
}