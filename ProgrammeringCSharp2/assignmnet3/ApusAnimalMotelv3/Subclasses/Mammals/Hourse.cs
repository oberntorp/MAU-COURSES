using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Dog;
using ApusAnimalMotel.FoodInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApusAnimalMotel.Mammals
{
    /// <summary>
    /// Basic object of a horse
    /// </summary>
    public abstract class Horse : Mammal
    {
        public int TeilLength { get; set; }
        public int Withers { get; set; }
        public int NumberOfLegs { get; set; }

        /// <summary>
        /// Constructor of horse, passed to it is the property values
        /// This constructor calls the Mammal constructor (as a horse is mammal)
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        /// <param name="numberOfTeeth">as a horse also is a mammal, it is also passed numberofteeth</param>
        /// <param name="teilLength">Teil length of horse</param>
        /// <param name="withers">Withers of a horse</param>
        /// <param name="numberOfLegs">The number of legs of a horse</param>
        public Horse(string name, int age, Gender gender, int numberOfTeeth , int teilLength, int withers, int numberOfLegs) : base(name, age, gender, numberOfTeeth)
        {
            TeilLength = teilLength;
            Withers = withers;
            NumberOfLegs = numberOfLegs;
        }

        /// <summary>
        /// ToString method to output the common characteristics of a bird as well as something personal for a Bullfinch
        /// </summary>
        /// <returns>the common characteristics of a bird as well as something personal for a Bullfinch as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {NumberOfLegs} - {TeilLength} - {Withers}";
        }

    }
}