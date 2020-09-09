using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic
{
    public class Image : MediaFile
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Image(string name, string source, string fileExtention, int width, int height) : base(name, source, fileExtention)
        {
            Width = width;
            Height = height;
        }
    }
}
