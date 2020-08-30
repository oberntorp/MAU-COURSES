using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.FoodInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.Interfaces
{
    /// <summary>
    /// The interface of Animal, ensures structure and that all Animals behave the same
    /// </summary>
    interface IAnimal
    {
        int Age { get; set; }
        Gender Gender { get; set; }
        string Name { get; set; }
        int Id { get; set; }
        EaterType GetEaterType();
        FoodSchedule GetFoodSchedule();
        string GetSpecies();
        string ToString();
    }
}
