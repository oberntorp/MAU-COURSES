using ApusAnimalMotel.Enums.Dog;
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
    class DogInformationForFactory
    {
        public int TeilLength { get; set; }
        public int NumberOfLegs { get; set; }
        public int NumberOfChildren { get; set; }
        public string FurType { get; set; }
        public AnimalUseCases UseCases { get; set; }
        public bool IsCosy { get; set; }
    }
}
