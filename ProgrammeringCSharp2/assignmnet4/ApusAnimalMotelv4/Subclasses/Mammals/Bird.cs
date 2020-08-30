using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Bird;
using ApusAnimalMotel.FoodInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApusAnimalMotel.Mammals
{
    /// <summary>
    /// Basic object of a Bird
    /// </summary>
    [Serializable]
    public abstract class Bird : Animal
    {
        public BirdType TypeOfBird { get; set; }

        /// <summary>
        /// Dog constructor, passed to it is the property values
        /// This constructor calls the Animal constructor (as a bird is an animal)
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        /// <param name="typeOfBird">What type of bird this is</param>
        public Bird(string name, int age, Gender gender, BirdType typeOfBird) : base(name, age, gender)
        {
            TypeOfBird = typeOfBird;
        }

        /// <summary>
        /// ToString method to output the common characteristics of a bird 
        /// </summary>
        /// <returns>the common characteristics of a bird as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {TypeOfBird}";
        }
    }
}