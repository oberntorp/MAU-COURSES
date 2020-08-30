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
    class Bullfinch : Bird
    {
        private FoodSchedule foodSchedule = null;
        public string WhatToSing { get; set; }

        /// <summary>
        /// BullFinch constructor, passed to it is the property values
        /// This constructor calls the bird constructor (as a BullFinch is a bird)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="typeOfBird">As bullfinch is a bird, type of bird needs to be passed</param>
        /// <param name="whatToSing">This is a property of bullfinch</param>
        public Bullfinch(string name, int age, Gender gender, BirdType typeOfBird, string whatToSing) : base(name, age, gender, typeOfBird)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("One insect for three times a day.");
            WhatToSing = whatToSing;
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
        /// Personal characteristics of a Bullfinch
        /// </summary>
        /// <returns>Personal characteristics of a Bullfinch as a string</returns>
        private string ILikeToSing()
        {
            return $"I like to sing {WhatToSing}";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a bird as well as something personal for a Bullfinch
        /// </summary>
        /// <returns>the common characteristics of a bird as well as something personal for a Bullfinch as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {ILikeToSing()}";
        }
    }
}
