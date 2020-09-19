using MultiMediaClassesAndManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Utilities;

namespace MultiMediaBussinessLogic
{
    public class MediaHandler
    {
        public IMediaFile GetImageObject(string fullPath, Bitmap image, string fileName)
        {
            return new MultiMediaClassesAndManagers.MediaSubClasses.Image(fileName, fullPath, FileHandler.GetFileExtension(fileName), image.Width, image.Height);
        }
    }
}
