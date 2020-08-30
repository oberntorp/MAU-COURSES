using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Bird;
using ApusAnimalMotel.FoodInformation;
using ApusAnimalMotel.Mammals;

namespace ApusAnimalMotel.Subclasses.Birds
{
    /// <summary>
    /// Species of a bird
    /// </summary>
    [Serializable]
    class Crow : Bird
    {
        private FoodSchedule foodSchedule = null;
        public string WhatSilverDoCrowLike { get; set; }

        /// <summary>
        /// Crow constructor, passed to it is the property values
        /// This constructor calls the bird constructor (as a Crow is a bird)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="typeOfBird">As bullfinch is a bird, type of bird needs to be passed</param>
        /// <param name="whatSilverDoCrowLike">This is a crow property</param>
        public Crow(string name, int age, Gender gender, BirdType typeOfBird, string whatSilverDoCrowLike) : base(name, age, gender, typeOfBird)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Two insect for twice a day.");
            WhatSilverDoCrowLike = whatSilverDoCrowLike;
        }

        /// <summary>
        /// Get the EaterType of the animal
        /// </summary>
        /// <returns></returns>
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
            return AnimalSpecies.Bird.ToString();
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
        /// Personal characteristics of a Crow
        /// </summary>
        /// <returns>Personal characteristics of a Crow as a string</returns>
        private string ILikeSilver()
        {
            return $"I like silver, {WhatSilverDoCrowLike}";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a bird as well as something personal for a Crow
        /// </summary>
        /// <returns>the common characteristics of a bird as well as something personal for a Crow as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {ILikeSilver()}";
        }
    }
}
