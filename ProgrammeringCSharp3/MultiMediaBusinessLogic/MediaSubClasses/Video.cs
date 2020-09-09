using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic
{
    public class Video : MediaFile
    {
        public int Length { get; set; }

        public Video(string name, string source, string fileExtention, int length) : base(name, source, fileExtention)
        {
            Length = length;
        }
    }
}
