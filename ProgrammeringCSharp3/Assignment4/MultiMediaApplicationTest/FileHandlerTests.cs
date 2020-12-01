using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Utilities;

namespace MultiMediaApplicationTest
{
    [TestClass]
    public class FileHandlerTests
    {
        [TestMethod]
        public void GetFileName_CurrectFileNameTest()
        {
            // Arrange
            string fileName = "C:\\Users\\obern\\OneDrive\\Bilder\\DSC08984.JPG";
            string expected = "DSC08984";

            // Act
            string actual = FileHandler.GetFileName(fileName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileName_WithOutFilePathTest()
        {
            // Arrange
            string fileName = string.Empty;
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileName_WithErrorNoDotInFileNameTest()
        {
            // Arrange
            string fileName = "file";
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileName_WithErrorIncompleteFileNameTest()
        {
            // Arrange
            string fileName = "file.";
            // Act
            FileHandler.GetFileName(fileName);
        }

        public void GetFileExtention_CurrectFileNameTest()
        {
            // Arrange
            string fileName = "C:\\Users\\obern\\OneDrive\\Bilder\\DSC08984.JPG";
            string expected = "JPG";

            // Act
            string actual = FileHandler.GetFileName(fileName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileExtention_WithOutFilePathTest()
        {
            // Arrange
            string fileName = string.Empty;
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileExtention_WithErrorNoDotInFileNameTest()
        {
            // Arrange
            string fileName = "file";
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileExtention_WithErrorIncompleteFileNameTest()
        {
            // Arrange
            string fileName = "file.";
            // Act
            FileHandler.GetFileName(fileName);
        }
    }
}
