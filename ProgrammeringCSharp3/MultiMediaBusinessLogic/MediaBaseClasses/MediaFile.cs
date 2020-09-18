using MutiMediaClassesAndManagers.Interfaces;
using System;
using System;

namespace MutiMediaClassesAndManagers.MediaBaseClass
{
    public class MediaFile : IMediaFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceUrl { get; set; }
        public string FileExtention { get; set; }

        public MediaFile(string name, string source, string fileExtention)
        {
            Name = name;
            SourceUrl = source;
            fileExtention = FileExtention;
        }
    }
}
