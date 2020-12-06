using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaBussinessLogic;
using WMPLib;
using MultiMediaClassesAndManagers.Interfaces;
using System.Drawing;
using System.IO;

namespace MultiMediaApplicationTest
{
    [TestClass]
    public class MediaHandlerTest
    {
        [TestMethod]
        public void IsMediaVideoTest_WithVideoObject_True()
        {
            // Arrange
            Video testVideo = new Video("WIN_20200921_15_43_07_Pro.mp4", "./VideoForTest/WIN_20200921_15_43_07_Pro.mp4", "./Images/video_icons8.png", "mp4", 5);
            MediaHandler mediaHandlerToTest = new MediaHandler();
            bool expected = true;

            // Act
            bool actualResult = mediaHandlerToTest.IsMediaVideo(testVideo);

            // Assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void IsMediaVideoTest_WithImageObject_False()
        {
            // Arrange
            MultiMediaClassesAndManagers.MediaSubClasses.Image testVideo = new MultiMediaClassesAndManagers.MediaSubClasses.Image("TestImg", "C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\TestImg.jpg", "C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\TestImg.jpg", "jpg", 100, 400);
            MediaHandler mediaHandlerToTest = new MediaHandler();
            bool expected = false;

            // Act
            bool actualResult = mediaHandlerToTest.IsMediaVideo(testVideo);

            // Assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void CreateVideoObjectTest_WithCurrectVideoData_CurrectObjectReturned()
        {
            // Arrange
            MediaHandler mediaHandlerToTest = new MediaHandler();
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            string pathToVideo = Path.Combine(Environment.CurrentDirectory, "VideoForTest\\WIN_20200921_15_43_07_Pro.mp4");
            string pathToVideoPreview = Path.Combine(Environment.CurrentDirectory, "Images\\video_icons8.png");

            Video expected = new Video("WIN_20200921_15_43_07_Pro.mp4", pathToVideo, pathToVideoPreview, "mp4", 14);
            // Act
            Video actualResult = (Video)mediaHandlerToTest.CreateVideoObject(pathToVideo, pathToVideoPreview, wmp.newMedia(pathToVideo), "WIN_20200921_15_43_07_Pro.mp4");

            // Assert
            Assert.AreEqual(expected.Id, actualResult.Id);
            Assert.AreEqual(expected.Name, actualResult.Name);
            Assert.AreEqual(expected.LengthInSeconds, actualResult.LengthInSeconds);
            Assert.AreEqual(expected.PreviewUrl, actualResult.PreviewUrl);
            Assert.AreEqual(expected.SourceUrl, actualResult.SourceUrl);
            Assert.AreEqual(expected.SortInPlaylist, actualResult.SortInPlaylist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVideoObjectTest_WithIncurrectInput_ExceptionThrown()
        {
            // Arrange
            MediaHandler mediaHandlerToTest = new MediaHandler();
            string pathToVideoPreview = Path.Combine(Environment.CurrentDirectory, "Images\\video_icons8.png");

            // Act
            IMediaFile actualResult = mediaHandlerToTest.CreateVideoObject(null, pathToVideoPreview, null, "WIN_20200921_15_43_07_Pro.mp4");
        }
    }
}
