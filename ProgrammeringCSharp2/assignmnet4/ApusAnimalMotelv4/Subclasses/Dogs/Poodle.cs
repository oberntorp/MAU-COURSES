using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Dog;
using ApusAnimalMotel.FoodInformation;
using ApusAnimalMotel.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.Subclasses.Dogs
{
    /// <summary>
    /// A species of dog
    /// </summary>
    [Serializable]
    class Poodle : Dog
    {
        public FoodSchedule foodSchedule = null;
        public bool CosyDog { get; set; }

        /// <summary>
        /// Poodle constructor, passed to it is the property values
        /// This constructor calls the dog constructor (as a Poodle is a dog)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="numberOfTeeth">As a poodle is a dog number of teeth needs to be passed</param>
        /// <param name="numberOfLegs">As a poodle is a dog number of legs needs to be passed</param>
        /// <param name="teilLength">As a poodle is a dog teil length needs to be passed</param>
        /// <param name="cosyDog">This is a property of poodle</param>
        public Poodle(string name, int age, Gender gender, int numberOfTeeth, int numberOfLegs, int teilLength, bool cosyDog) : base(name, age, gender, numberOfTeeth, numberOfLegs, teilLength)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Saucige once a day.");
            CosyDog = cosyDog;
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
            return AnimalSpecies.Dog.ToString();
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
        /// Personal characteristics of a Poodle
        /// </summary>
        /// <returns>Personal characteristics of a Poodle as a string</returns>
        public string Companion()
        {
            return (CosyDog) ? "I am good at being a companion dog" : "I am not good at being a companion dog";
        }

        /// <summary>
        /// ToString method to output the common characteristics of a dog as well as something personal for a poodle
        /// </summary>
        /// <returns>the common characteristics of a dog as well as something personal for a poodle as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {Companion()}";
        }
    }
}
