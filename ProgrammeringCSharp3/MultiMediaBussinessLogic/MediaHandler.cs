using MultiMediaClassesAndManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Utilities;

namespace MultiMediaBussinessLogic
{
    /// <summary>
    /// This class Handles the Access/Retreival operations of Media (Image/Video classes)
    /// </summary>
    public class MediaHandler
    {
        /// <summary>
        /// Creates a Image Object
        /// </summary>
        /// <param name="fullPath">path to set in object</param>
        /// <param name="previewUrl">Url of preview (differs for type)</param>
        /// <param name="image">the bitmapimage to add Width/Height</param>
        /// <param name="fileName">The name of the image</param>
        /// <returns>IMediaFile</returns>
        public IMediaFile CreateImageObject(string fullPath, string previewUrl, Bitmap image, string fileName)
        {
            return new MultiMediaClassesAndManagers.MediaSubClasses.Image(fileName, fullPath, previewUrl, FileHandler.GetFileExtension(fullPath), image.Width, image.Height);
        }
    }
}
