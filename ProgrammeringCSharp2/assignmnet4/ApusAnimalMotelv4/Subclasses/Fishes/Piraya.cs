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
    /// A species of Fish
    /// </summary>
    [Serializable]
    public class Piraya : Fish
    {
        private FoodSchedule foodSchedule = null;
        public string WhyIDangerous { get; set; }

        /// <summary>
        /// Piraya constructor, passed to it is the property values
        /// This constructor calls the fish constructor (as a Piraya is a fish)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="fishFamily">As an piraya is a fish, we need to pass fishFamily</param>
        /// <param name="numberOfFins">As an piraya is a fish, we need to pass number of fins</param>
        /// <param name="whyIDangerous">This is a piraya property</param>
        public Piraya(string name, int age, Gender gender, FishFamily fishFamily, int numberOfFins, string whyIDangerous): base(name, age, gender, fishFamily, numberOfFins)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Frosen krill every day");
            WhyIDangerous = whyIDangerous;
        }

        /// <summary>
        /// Get the EaterType of the animal
        /// </summary>
        /// <returns></returns>
        public override EaterType GetEaterType()
        {
            return EaterType.Carnivore;
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
        /// Personal characteristics of a piraya
        /// </summary>
        /// <returns>Personal characteristics of a piraya as a string</returns>
        private string Dangerous()
        {
            return $"I am dangerous, because: {WhyIDangerous}";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a fish as well as something personal for a piraya
        /// </summary>
        /// <returns>the common characteristics of a fish as well as something personal for a piraya as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {Dangerous()}";
        }
    }
}