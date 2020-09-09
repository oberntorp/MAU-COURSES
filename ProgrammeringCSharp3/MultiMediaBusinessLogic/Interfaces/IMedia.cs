using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaBusinessLogic
{
    public interface IMedia
    {
        string Name { get; set; }
        string Source { get; set; }
        string FileExtention { get; set; }
    }
}
