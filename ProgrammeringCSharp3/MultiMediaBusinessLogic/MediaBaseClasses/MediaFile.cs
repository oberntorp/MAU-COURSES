using MutiMediaClassesAndManagers.Interfaces;
using System;
using System;

namespace MutiMediaClassesAndManagers.MediaBaseClass
{
    public class MediaFile : IMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string FileExtention { get; set; }

        public MediaFile(string name, string source, string fileExtention)
        {
            Name = name;
            Source = source;
            fileExtention = FileExtention;
        }
    }
}
