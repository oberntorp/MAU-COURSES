using MultiMediaClassesAndManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Utilities;
using WMPLib;
using MultiMediaClassesAndManagers.MediaBaseClass;
using MultiMediaClassesAndManagers.MediaSubClasses;

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
        /// <param name="fullPath">The fullPath of the media</param>
        /// <param name="previewUrl">A preview url needed when displaying a thumbnail of the media</param>
        /// <param name="image">the bitmapimage to add Width/Height</param>
        /// <param name="fileName">The name of the image</param>
        /// <returns>IMediaFile</returns>
        public IMediaFile CreateImageObject(string fullPath, string previewUrl, Bitmap image, string fileName)
        {
            return new MultiMediaClassesAndManagers.MediaSubClasses.Image(fileName, fullPath, previewUrl, FileHandler.GetFileExtension(fullPath), image.Width, image.Height);
        }

        /// <summary>
        /// Creates a Video object
        /// </summary>
        /// <param name="fullPath">The fullPath of the media</param>
        /// <param name="previewUrl">A preview url needed when displaying a thumbnail of the media</param>
        /// <param name="vidoInfo">An object needed to obtain the length of the video in question</param>
        /// <param name="fileName">The name of the video</param>
        /// <returns></returns>
        public IMediaFile CreateVideoObject(string fullPath, string previewUrl, IWMPMedia vidoInfo, string fileName)
        {
            CheckVideoDataForErrors(fullPath, previewUrl, vidoInfo, fileName);
            return new MultiMediaClassesAndManagers.MediaSubClasses.Video(fileName, fullPath, previewUrl, FileHandler.GetFileExtension(fullPath), vidoInfo.duration);
        }

        private void CheckVideoDataForErrors(string fullPath, string previewUrl, IWMPMedia vidoInfo, string fileName)
        {
            if(string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(previewUrl) || vidoInfo == null || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("The Video data contains errors");
            }
        }

        /// <summary>
        /// This method checks to see if a MediFile is a video or an image
        /// </summary>
        /// <param name="mediaToCheck">The media to check</param>
        /// <returns>true/false</returns>
        public bool IsMediaVideo(MediaFile mediaToCheck)
        {
            return mediaToCheck is Video;
        }
    }
}
