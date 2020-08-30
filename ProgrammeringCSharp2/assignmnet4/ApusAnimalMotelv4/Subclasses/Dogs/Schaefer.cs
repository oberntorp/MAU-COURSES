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
    /// Species of dog
    /// </summary>
    [Serializable]
    class Schaefer : Dog
    {
        private FoodSchedule foodSchedule = null;
        public AnimalUseCases UseCase { get; set; }

        /// <summary>
        /// Schaefer constructor, passed to it is the property values
        /// This constructor calls the dog constructor (as a Schaefer is a dog)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="numberOfTeeth">As a schaefer is a dog number of teeth needs to be passed</param>
        /// <param name="numberOfLegs">As a schaefer is a dog number of legs needs to be passed</param>
        /// <param name="teilLength">As a schaefer is a dog teil length needs to be passed</param>
        /// <param name="useCase">This is a property of Schaefer</param>
        public Schaefer(string name, int age, Gender gender, int numberOfTeeth, int numberOfLegs, int teilLength, AnimalUseCases useCase) : base(name, age, gender, numberOfTeeth, numberOfLegs, teilLength)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("Cheese and saucige once a week.");
            UseCase = useCase;
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
        /// ToString method to output the common characteristics of a dog as well as something personal for a Schaefer
        /// </summary>
        /// <returns>the common characteristics of a dog as well as something personal for a Schaefer as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - { UseCase}";
        }
    }
}
