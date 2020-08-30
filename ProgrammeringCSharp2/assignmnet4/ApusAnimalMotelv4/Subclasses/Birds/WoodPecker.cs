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
    class WoodPecker : Bird
    {
        private FoodSchedule foodSchedule = null;
        public BeakType TypeOfBeak { get; set; }

        /// <summary>
        /// WoodPecker constructor, passed to it is the property values
        /// This constructor calls the bird constructor (as a WoodPecker is a bird)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="typeOfBird">As marchsandpiper is a bird, type of bird needs to be passed</param>
        /// <param name="typeOfBeak">This is a marchsandpiper property</param>
        public WoodPecker(string name, int age, Gender gender, BirdType typeOfBird, BeakType typeOfBeak) : base(name, age, gender, typeOfBird)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Tree insects once a day.");
            foodSchedule.Add("Tree liters of water once a week");
            TypeOfBeak = typeOfBeak;
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
        /// Personal characteristics of a Woodpecker
        /// </summary>
        /// <returns>Personal characteristics of a Bullfinch as a string</returns>
        public string PeckOnWood()
        {
            return "My favorite habit is to peck woods";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a bird as well as something personal for a WoodPecker
        /// </summary>
        /// <returns>the common characteristics of a bird as well as something personal for a WoodPecker as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - my beak is {TypeOfBeak}";
        }
    }
}
