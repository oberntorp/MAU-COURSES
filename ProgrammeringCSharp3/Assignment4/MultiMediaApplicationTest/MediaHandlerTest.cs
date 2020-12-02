using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiMediaClassesAndManagers.MediaSubClasses;
using MultiMediaBussinessLogic;
using WMPLib;
using MultiMediaClassesAndManagers.Interfaces;
using System.Drawing;

namespace MultiMediaApplicationTest
{
    [TestClass]
    public class MediaHandlerTest
    {
        [TestMethod]
        public void IsMediaVideoTest_WithVideoObject()
        {
            // Arrange
            Video testVideo = new Video("WIN_20200921_15_43_07_Pro.mp4", "C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\WIN_20200921_15_43_07_Pro.mp4", "../Images/video_icons8.png", "mp4", 5);
            MediaHandler mediaHandlerToTest = new MediaHandler();
            bool expected = true;

            // Act
            bool actualResult = mediaHandlerToTest.IsMediaVideo(testVideo);

            // Assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        public void IsMediaVideoTest_WithImageObject()
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
        public void CreateVideoObjectTest_WithCurrectVideoData()
        {
            // Arrange
            MediaHandler mediaHandlerToTest = new MediaHandler();
            WindowsMediaPlayer wmp = new WindowsMediaPlayer();

            Video expected = new Video("WIN_20200921_15_43_07_Pro.mp4", "C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\WIN_20200921_15_43_07_Pro.mp4", "../Images/video_icons8.png", "mp4", 5);

            // Act
            Video actualResult = (Video)mediaHandlerToTest.CreateVideoObject("C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\WIN_20200921_15_43_07_Pro.mp4", "Images/video_icons8.png", wmp.newMedia("C:\\Users\\obern\\OneDrive\\SkyDrive camera roll\\WIN_20200921_15_43_07_Pro.mp4"), "WIN_20200921_15_43_07_Pro.mp4");

            // Assert
            Assert.AreEqual(expected, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVideoObjectTest_WithIncurrectVideoData()
        {
            // Arrange
            MediaHandler mediaHandlerToTest = new MediaHandler();

            // Act
            IMediaFile actualResult = mediaHandlerToTest.CreateVideoObject(null, "Images/video_icons8.png", null, "WIN_20200921_15_43_07_Pro.mp4");
        }
    }
}
