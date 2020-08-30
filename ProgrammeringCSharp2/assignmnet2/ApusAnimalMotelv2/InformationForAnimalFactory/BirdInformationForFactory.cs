using ApusAnimalMotel.Enums.Bird;
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
    class BirdInformationForFactory
    {
        public BirdType BirdType { get; set; }
        public string WhatToSing { get; set; }
        public string WhatSilverDoCrowsLike { get; set; }
        public int WingSpan { get; set; }
        public Plumage Plumage { get; set; }
        public BeakType TypeOfBeak { get; internal set; }
    }
}
