using MultiMediaClassesAndManagers.Interfaces;
using System;
using System;

namespace MultiMediaClassesAndManagers.MediaBaseClass
{
    /// <summary>
    /// The base class of a media file, defines generic properties or methods that all subclasses share
    /// </summary>
    public class MediaFile : IMediaFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceUrl { get; set; }
        public string PreviewUrl { get; set; }
        public string FileExtention { get; set; }

        /// <summary>
        /// Constructor of MediaFile, initializes object of type MediaFile
        /// </summary>
        /// <param name="name">the name of the media file</param>
        /// <param name="sourceUrl">the source of the MediaFile</param>
        /// <param name="previewUrl">Url of preview (differs for type)</param>
        /// <param name="fileExtention">the MediaFiles fileExtention</param>
        public MediaFile(string name, string sourceUrl, string previewUrl, string fileExtention)
        {
            Name = name;
            SourceUrl = sourceUrl;
            FileExtention = fileExtention;
        }
    }
}
