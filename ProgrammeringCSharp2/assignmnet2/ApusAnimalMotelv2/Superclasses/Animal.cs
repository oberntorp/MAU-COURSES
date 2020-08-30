using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.FoodInformation;
using ApusAnimalMotel.Interfaces;

namespace ApusAnimalMotel
{
    /// <summary>
    /// The base class of any Animal with basic characteristics
    /// </summary>
    public abstract class Animal: IAnimal
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public EaterType EaterType { get; set; }

        /// <summary>
        /// Animal constructor, passed to it is the property values
        /// </summary>
        /// <param name="name">Name of mammal</param>
        /// <param name="age">Age of mammal</param>
        /// <param name="gender">gender of mammal</param>
        public Animal(string name, int age, Gender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public abstract EaterType GetEaterType();
        public abstract FoodSchedule GetFoodSchedule();
        public abstract string GetSpecies();

        /// <summary>
        /// ToString method to output the common characteristics of an animal
        /// </summary>
        /// <returns>the common characteristics of an animal as a string</returns>
        public override string ToString()
        {
            return $"{Id} - {Name} - {Age} - {Gender}";
        }
    }
}