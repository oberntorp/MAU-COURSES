using ApusAnimalMotel.AnimalFactoryInformation;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Bird;
using ApusAnimalMotel.Enums.Dog;
using ApusAnimalMotel.Enums.Fish;
using ApusAnimalMotel.Enums.Horse;
using ApusAnimalMotel.Mammals;
using ApusAnimalMotel.Subclasses.Birds;
using ApusAnimalMotel.Subclasses.Dogs;
using ApusAnimalMotel.Subclasses.Horses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel
{
    /// <summary>
    /// This class handles the creation of animals
    /// </summary>
    class AnimalFactory
    {
        private string animalName = string.Empty;
        private int animalAge = 0;
        private Gender animalGender;
        private AnimalSpecies animalSpecies;
        private int animalTypeWithinSpecies = 0;
        private BirdInformationForFactory birdInformation;
        private DogInformationForFactory dogInformation;
        private FishInformationForFactory fishInformation;
        private HorseInformationForFactory horseInformation;
        private int mammalNumberOfTeeth = 0;



        /// <summary>
        /// Default constructor is needed if importing from dat where it is not instanciated via Add animal info
        /// </summary>
        public AnimalFactory()
        {

        }
        /// <summary>
        /// Animal factory constructor, sets the values needed in the creation of the animal
        /// </summary>
        /// <param name="name">Name of animal</param>
        /// <param name="age">Age of animal</param>
        /// <param name="gender">Gender of animal</param>
        /// <param name="animalSpecies">Used to know what animalSpecies was selected</param>
        /// <param name="typeWithinSpecies">Used to know what type within a species selected</param>
        /// <param name="birdInformation">Information needed to create a bird</param>
        public AnimalFactory(string name, int age, Gender gender, AnimalSpecies animalSpecies, int typeWithinSpecies, BirdInformationForFactory birdInformation)
        {
            animalName = name;
            animalAge = age;
            animalGender = gender;
            this.animalSpecies = animalSpecies;
            this.animalTypeWithinSpecies = typeWithinSpecies;
            this.birdInformation = birdInformation;
        }

        /// <summary>
        /// Animal factory constructor, sets the values needed in the creation of the animal
        /// </summary>
        /// <param name="name">Name of animal</param>
        /// <param name="age">Age of animal</param>
        /// <param name="gender">Gender of animal</param>
        /// <param name="animalSpecies">Used to know what animalSpecies was selected</param>
        /// <param name="typeWithinSpecies">Used to know what type within a species selected</param>
        /// <param name="dogInformation">Information needed to create a dog</param>
        /// <param name="numberOfTeeth">As a dog is a mammal, we also need number of teeth</param>
        public AnimalFactory(string name, int age, Gender gender, AnimalSpecies animalSpecies, int typeWithinSpecies, DogInformationForFactory dogInformation, int numberOfTeeth)
        {
            animalName = name;
            animalAge = age;
            animalGender = gender;
            this.animalSpecies = animalSpecies;
            this.animalTypeWithinSpecies = typeWithinSpecies;
            this.dogInformation = dogInformation;
            mammalNumberOfTeeth = numberOfTeeth;
        }

        /// <summary>
        /// Animal factory constructor, sets the values needed in the creation of the animal
        /// </summary>
        /// <param name="name">Name of animal</param>
        /// <param name="age">Age of animal</param>
        /// <param name="gender">Gender of animal</param>
        /// <param name="animalSpecies">Used to know what animalSpecies was selected</param>
        /// <param name="typeWithinSpecies">Used to know what type within a species selected</param>
        /// <param name="fishInformation">Information needed to create a fish</param>
        public AnimalFactory(string name, int age, Gender gender, AnimalSpecies animalSpecies, int typeWithinSpecies, FishInformationForFactory fishInformation)
        {
            animalName = name;
            animalAge = age;
            animalGender = gender;
            this.animalSpecies = animalSpecies;
            this.animalTypeWithinSpecies = typeWithinSpecies;
            this.fishInformation = fishInformation;
        }

        /// <summary>
        /// Animal factory constructor, sets the values needed in the creation of the animal
        /// </summary>
        /// <param name="name">Name of animal</param>
        /// <param name="age">Age of animal</param>
        /// <param name="gender">Gender of animal</param>
        /// <param name="animalSpecies">Used to know what animalSpecies was selected</param>
        /// <param name="typeWithinSpecies">Used to know what type within a species selected</param>
        /// <param name="horseInformation">Information needed to create a horse</param>
        /// <param name="numberOfTeeth">As a horse is a mammnal, we also need number of teeth</param>
        public AnimalFactory(string name, int age, Gender gender, AnimalSpecies animalSpecies, int typeWithinSpecies, HorseInformationForFactory horseInformation, int numberOfTeeth)
        {
            animalName = name;
            animalAge = age;
            animalGender = gender;
            this.animalSpecies = animalSpecies;
            this.animalTypeWithinSpecies = typeWithinSpecies;
            this.horseInformation = horseInformation;
            mammalNumberOfTeeth = numberOfTeeth;
        }

        /// <summary>
        /// This class starting method, first called upon creating an animal
        /// </summary>
        /// <returns></returns>
        public Animal CreateAnimal()
        {
            Animal animalAfterSpecies = CreateTypeWthinSpecies();
            return animalAfterSpecies;
        }

        /// <summary>
        /// Create typeWithin species
        /// </summary>
        /// <returns></returns>
        private Animal CreateTypeWthinSpecies()
        {
            if (animalSpecies == AnimalSpecies.Bird)
            {
                return CreateTypeOfBirdFromBirdType();
            }
            else if (animalSpecies == AnimalSpecies.Dog)
            {
                return CreateTypeOfDogFromDogType();
            }
            else if (animalSpecies == AnimalSpecies.Horse)
            {
                return CreateTypeOfHorseFromHorseType();
            }
            else
            {
                return CreateTypeOfFishFromFishType();
            }
        }

        /// <summary>
        /// Creates a Bird type depending on type selected
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        private Bird CreateTypeOfBirdFromBirdType()
        {
            if ((BirdType)animalTypeWithinSpecies == BirdType.Bullfinch)
            {
                Bullfinch bullfinch = new Bullfinch(animalName, animalAge, animalGender, birdInformation.BirdType, birdInformation.WhatToSing);
   
                return bullfinch;
            }
            else if ((BirdType)animalTypeWithinSpecies == BirdType.Crow)
            {
                Crow crow = new Crow(animalName, animalAge, animalGender, birdInformation.BirdType, birdInformation.WhatSilverDoCrowsLike);

                return crow;
            }
            else if ((BirdType)animalTypeWithinSpecies == BirdType.MarchSandpiper)
            {
                MarchSandpiper marchSandpiper = new MarchSandpiper(animalName, animalAge, animalGender, birdInformation.BirdType, birdInformation.WingSpan, birdInformation.Plumage);

                return marchSandpiper;
            }
            else
            {
                WoodPecker woodpecker = new WoodPecker(animalName, animalAge, animalGender, birdInformation.BirdType, birdInformation.TypeOfBeak);

                return woodpecker;
            }
        }

        /// <summary>
        /// Creates a Dog type depending on type selected
        /// </summary>
        /// <param name="other">Dog to copy details from</param>
        /// <returns></returns>
        private Dog CreateTypeOfDogFromDogType()
        {
            if ((DogType)animalTypeWithinSpecies == DogType.GoldenRetriever)
            {
                GoldenRetriever goldenRetriever = new GoldenRetriever(animalName, animalAge, animalGender, mammalNumberOfTeeth, dogInformation.NumberOfLegs, dogInformation.TeilLength, dogInformation.FurType);

                return goldenRetriever;
            }
            else if ((DogType)animalTypeWithinSpecies == DogType.Poodle)
            {
                Poodle poodle = new Poodle(animalName, animalAge, animalGender, mammalNumberOfTeeth, dogInformation.NumberOfLegs, dogInformation.TeilLength, dogInformation.IsCosy);

                return poodle;
            }
            else
            {
                Schaefer schaefer = new Schaefer(animalName, animalAge, animalGender, mammalNumberOfTeeth, dogInformation.NumberOfLegs, dogInformation.TeilLength, dogInformation.UseCases);

                return schaefer;
            }
        }

        /// <summary>
        /// Creates a Fish type depending on type selected
        /// </summary>
        /// <param name="other">Fish to copy details from</param>
        /// <returns></returns>
        private Fish CreateTypeOfFishFromFishType()
        {
            if ((FishType)animalTypeWithinSpecies == FishType.GoldFish)
            {
                GoldFish goldFish = new GoldFish(animalName, animalAge, animalGender, fishInformation.FishFamily, fishInformation.NumberOfFins, fishInformation.IDOForget);

                return goldFish;
            }
            else
            {
                Piraya piraya = new Piraya(animalName, animalAge, animalGender, fishInformation.FishFamily, fishInformation.NumberOfFins, fishInformation.WhyIDangerous);

                return piraya;
            }

        }

        /// <summary>
        /// Creates a horse type depending on type selected
        /// </summary>
        /// <param name="other">horse to copy details from</param>
        /// <returns></returns>
        private Horse CreateTypeOfHorseFromHorseType()
        {
            if ((HorseType)animalTypeWithinSpecies == HorseType.Pony)
            {
                Pony pony = new Pony(animalName, animalAge, animalGender, mammalNumberOfTeeth, horseInformation.TeilLength, horseInformation.Withers, horseInformation.NumberOfLegs, horseInformation.WhoLikesToRideBack);

                return pony;
            }
            else
            {
                Tarpan tarpan = new Tarpan(animalName, animalAge, animalGender, mammalNumberOfTeeth, horseInformation.TeilLength, horseInformation.Withers, horseInformation.NumberOfLegs, horseInformation.Sturdy);

                return tarpan;
            }
        }

        /// <summary>
        /// To be shown, the fetched animal needs to be converted to its right type
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public Animal ConvertAnimalToRightType(Animal animal)
        {
            if (animal is Bird)
            {
                return ConvertToBirds(animal);
            }
            else if (animal is Dog)
            {
                return ConvertToDogs(animal);
            }
            else if (animal is Fish)
            {
                return ConvertToFishes(animal);
            }
            else
            {
                return ConvertToHorses(animal);
            }
        }

        /// <summary>
        /// Method for generating the right objects for birds
        /// </summary>
        /// <param name="animal">the animal to get a bird from</param>
        /// <returns></returns>
        private Animal ConvertToBirds(Animal animal)
        {
            if (animal is Crow)
                return (Crow)animal;
            if (animal is MarchSandpiper)
                return (MarchSandpiper)animal;
            if (animal is WoodPecker)
                return (WoodPecker)animal;
            if (animal is Bullfinch)
                return (Bullfinch)animal;

            return null;
        }

        /// <summary>
        /// Method for generating the right objects for Dogs
        /// </summary>
        /// <param name="animal">the animal to get a dog from</param>
        /// <returns>Animal</returns>
        private Animal ConvertToDogs(Animal animal)
        {
            if (animal is GoldenRetriever)
                return (GoldenRetriever)animal;
            if (animal is Poodle)
                return (Poodle)animal;
            if (animal is Schaefer)
                return (Schaefer)animal;

            return null;
        }

        /// <summary>
        /// Method for generating right objects for Fishes
        /// </summary>
        /// <param name="animal">the animal to get a fish from</param>
        /// <returns>Animal</returns>
        private Animal ConvertToFishes(Animal animal)
        {
            if (animal is GoldFish)
                return (GoldFish)animal;
            if (animal is Piraya)
                return (Piraya)animal;

            return null;
        }

        /// <summary>
        /// Method for generating right objects for Horses
        /// </summary>
        /// <param name="animal">the animal to get a horse from</param>
        /// <returns></returns>
        private Animal ConvertToHorses(Animal animal)
        {
            if (animal is Pony)
                return (Pony)animal;
            else
                return (Tarpan)animal;
        }
    }
}
