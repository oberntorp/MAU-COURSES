using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Dog;
using ApusAnimalMotel.FoodInformation;

namespace ApusAnimalMotel.Mammals
{
    /// <summary>
    /// Basic object of a dog
    /// </summary>
    public abstract class Dog : Mammal
    {

        public int NumberOfLegs { get; set; }
        public int TeilLength { get; set; }

        /// <summary>
        /// Dog constructor, passed to it is the property values
        /// This constructor calls the Mammal constructor (as a dog is a mammal)
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        /// <param name="numberOfTeeth">The number of teeth that the mammal has</param>
        /// <param name="numberOfLegs">The number of legs that a horse has</param>
        /// <param name="teilLength">The teil length of the horse</param>
        public Dog(string name, int age, Gender gender, int numberOfTeeth, int numberOfLegs, int teilLength) : base(name, age, gender, numberOfTeeth)
        {
            NumberOfLegs = numberOfLegs;
            TeilLength = teilLength;
        }

        /// <summary>
        /// ToString method to output the common characteristics of a dog
        /// </summary>
        /// <returns>the common characteristics of a dog as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {NumberOfLegs} - {TeilLength}";
        }
    }
}