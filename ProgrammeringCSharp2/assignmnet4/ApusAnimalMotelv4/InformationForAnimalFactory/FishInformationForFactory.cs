using ApusAnimalMotel.Enums.Bird;
using ApusAnimalMotel.Enums.Fish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApusAnimalMotel.AnimalFactoryInformation
{
    /// <summary>
    /// Depending on type of animal, different things is needed by the AnimalFactory, to limit the amuont of arguments to  AnimalFactory these classes were created, and three constructors of AnimalFactory were created 
    /// </summary>
    class FishInformationForFactory
    {
        public bool IDOForget { get; set; }
        public int NumberOfFins { get; set; }
        public string WhyIDangerous { get; set; }
        public FishFamily FishFamily { get; set; }
        public BeakType BeakType { get; set; }
    }
}
