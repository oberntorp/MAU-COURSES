using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.FoodInformation;
using ApusAnimalMotel.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.Subclasses.Horses
{
    /// <summary>
    /// This is a special species of horse
    /// </summary>
    [Serializable]
    class Pony : Horse
    {
        private FoodSchedule foodSchedule = null;
        public string HowLikeToRideBack { get; set; }

        /// <summary>
        /// Pony constructor, passed to it is the property values
        /// This constructor calls the Horse constructor (as a Tarpan is a horse)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="numberOfTeeth">A Pony is basicaly a mammal, thats why number of teeth is passed</param>
        /// <param name="teilLength">As Pony is a horse teilLength (of horse needs to be passed)</param>
        /// <param name="withers">As Pony is a horse withers (of horse needs to be passed)</param>
        /// <param name="numberOfLegs">As Pony is a horse number of legs (of horse needs to be passed)</param>
        /// <param name="howLikeToRideBack">This is a property of pony</param>
        public Pony(string name, int age, Gender gender, int numberOfTeeth, int teilLength, int withers, int numberOfLegs, string howLikeToRideBack): base(name, age, gender, numberOfTeeth, teilLength, withers, numberOfLegs)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Hay and water once a week.");
            HowLikeToRideBack = howLikeToRideBack;
        }

        /// <summary>
        /// Get the EaterType of the animal
        /// </summary>
        /// <returns></returns>
        public override EaterType GetEaterType()
        {
            return EaterType.Herbivore;
        }

        /// <summary>
        /// Get species for this object
        /// </summary>
        /// <returns>Species as a string</returns>
        public override string GetSpecies()
        {
            return AnimalSpecies.Horse.ToString();
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
        /// ToString method to output the common characteristics of a horse as well as something personal for a pony
        /// </summary>
        /// <returns>the common characteristics of a horse as well as something personal for a pony as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {HowLikeToRideBack}";
        }
    }
}
