using MultiMediaClassesAndManagers.MediaBaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.MediaSubClasses
{
    /// <summary>
    /// This class holds information important for an image
    /// </summary>
    public class Image : MediaFile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// The Image class constructor, initializes the object of type Image (as well as passes on the name and other common ground information to the base class)
        /// </summary>
        /// <param name="name">Name of the Image</param>
        /// <param name="sourceUrl">The Image source</param>
        /// <param name="previewUrl">Url of preview (differs for type)</param>
        /// <param name="fileExtention">The fileExtention of the Image</param>
        /// <param name="width">The width of the Image</param>
        /// <param name="height">The height of the Image</param>
        public Image(string name, string sourceUrl, string previewUrl, string fileExtention, int width, int height) : base(name, sourceUrl, previewUrl, fileExtention)
        {
            Width = width;
            Height = height;
        }
    }
}
