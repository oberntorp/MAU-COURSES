using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.FoodInformation;

namespace ApusAnimalMotel
{
    [Serializable]
    public abstract class Mammal : Animal
    {
        public int NumberOfTeeth { get; set; }

        /// <summary>
        /// Constructor of mammal, passed to it is the property values
        /// This constructor calls the animal constructor (as a mmammal is an animal)
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        /// <param name="numberOfTeeth">Mammal number of teeth</param>
        public Mammal(string name, int age, Gender gender, int numberOfTeeth) : base(name, age, gender)
        {
            NumberOfTeeth = numberOfTeeth;
        }

        /// <summary>
        /// ToString method to output the common characteristics of a mammal
        /// </summary>
        /// <returns>the common characteristics of a mammal as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - {NumberOfTeeth}";
        }
    }
}