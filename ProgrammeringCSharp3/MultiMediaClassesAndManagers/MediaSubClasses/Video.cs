using MultiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.MediaSubClasses
{
    /// <summary>
    /// This class holds the information important for an video
    /// </summary>
    public class Video : MediaFile
    {
        public double LengthInMinutes { get; set; }

        /// <summary>
        /// The Video class constructor, initializes the object of type Video (as well as passes on the name and other common ground information to the base class)
        /// </summary>
        /// <param name="name">The name of the video</param>
        /// <param name="source">The source of the video</param>
        /// <param name="previewUrl">Url of preview (differs for type)</param>
        /// <param name="fileExtention">The fileExtention of the video</param>
        /// <param name="lengthInMinutes">The vides length</param>
        public Video(string name, string source, string previewUrl, string fileExtention, double lengthInMinutes) : base(name, source, previewUrl, fileExtention)
        {
            LengthInMinutes = lengthInMinutes;
        }
    }
}
