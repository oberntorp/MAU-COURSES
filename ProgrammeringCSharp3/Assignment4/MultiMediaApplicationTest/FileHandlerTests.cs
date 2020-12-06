using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Utilities;

namespace MultiMediaApplicationTest
{
    [TestClass]
    public class FileHandlerTests
    {
        [TestMethod]
        public void GetFileNameTest_CurrectFileName_FilenamesMatch()
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
        public void GetFileNameTest_WithOutFilePath_ExceptionThrown()
        {
            // Arrange
            string fileName = string.Empty;
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileNameTest_WithErrorNoDotInFileName_ExceptionThrown()
        {
            // Arrange
            string fileName = "file";
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileNameTest_WithErrorIncompleteFileName_ExceptionThrown()
        {
            // Arrange
            string fileName = "file.";
            // Act
            FileHandler.GetFileName(fileName);
        }

        public void GetFileExtentionTest_CurrectFileName_FileExtentionMatch()
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
        public void GetFileExtentionTest_WithOutFilePath_ExceptionThrown()
        {
            // Arrange
            string fileName = string.Empty;
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileExtention_WithErrorNoDotInFileNameTest_ExceptionThrown()
        {
            // Arrange
            string fileName = "file";
            // Act
            FileHandler.GetFileName(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFileExtention_WithErrorIncompleteFileNameTest_ExceptionThrown()
        {
            // Arrange
            string fileName = "file.";
            // Act
            FileHandler.GetFileName(fileName);
        }
    }
}
