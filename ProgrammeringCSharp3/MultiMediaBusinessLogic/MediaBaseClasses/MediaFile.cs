using System;

namespace MultiMediaBusinessLogic
{
    public class MediaFile : IMedia
    {
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
