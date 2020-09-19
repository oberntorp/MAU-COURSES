using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMediaClassesAndManagers.Interfaces
{
    // <summary>
    /// The generic interface of MediaFile, ensures structure and that all MediaFiles behave the same
    /// </summary>
    /// <typeparam name="T">as this interface is generic, it needs a type</typeparam>
    public interface IMediaFile
    {
        int Id { get; set; }
        string Name { get; set; }
        string SourceUrl { get; set; }
        string FileExtention { get; set; }
    }
}
