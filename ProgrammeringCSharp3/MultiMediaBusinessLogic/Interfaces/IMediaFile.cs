using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.Interfaces
{
    public interface IMediaFile
    {
        int Id { get; set; }
        string Name { get; set; }
        string SourceUrl { get; set; }
        string FileExtention { get; set; }
    }
}
