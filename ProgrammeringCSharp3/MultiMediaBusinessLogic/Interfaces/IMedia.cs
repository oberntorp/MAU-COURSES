using System;
using System.Collections.Generic;
using System.Text;

namespace MutiMediaClassesAndManagers.Interfaces
{
    public interface IMedia
    {
        int Id { get; set; }
        string Name { get; set; }
        string Source { get; set; }
        string FileExtention { get; set; }
    }
}
