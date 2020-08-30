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
    class MarchSandpiper : Bird
    {
        private FoodSchedule foodSchedule = null;
        public int WingSpan { get; set; }

        public Plumage Plumage { get; set; }

        /// <summary>
        /// MarchSandpiper constructor, passed to it is the property values
        /// This constructor calls the bird constructor (as a MarchSandpiper is a bird)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gender</param>
        /// <param name="typeOfBird">As marchsandpiper is a bird, type of bird needs to be passed</param>
        /// <param name="wingSpan">This is a marchsandpiper property</param>
        /// <param name="plumage">This is a marchsandpiper property</param>
        public MarchSandpiper(string name, int age, Gender gender, BirdType typeOfBird, int wingSpan, Plumage plumage) : base(name, age, gender, typeOfBird)
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.Add("One insect for once a week.");
            WingSpan = wingSpan;
            Plumage = plumage;
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
        /// ToString method to output the common characteristics of a bird as well as something personal for a MarchSandpiper
        /// </summary>
        /// <returns>the common characteristics of a bird as well as something personal for a MarchSandpiper as a string</returns>
        public override string ToString()
        {
            return $"{base.ToString()} - I have a plumage {Plumage} and a wingspan {WingSpan}";
        }
    }
}
