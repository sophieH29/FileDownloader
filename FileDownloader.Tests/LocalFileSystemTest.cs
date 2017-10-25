using System.IO;
using FileDownloader.FileSystems;
using FileDownloader.Managers;
using Moq;
using NUnit.Framework;

namespace FileDownloader.Tests
{
    [TestFixture]
    public class LocalFileSystemTest
    {
        private string _destinationPath = @"C:\TestDownloadedFiles";
        private string _fileName = "filename.txt";
        private LocalFileSystem _localFileSystem;

        [SetUp]
        public void Setup()
        {
            _localFileSystem = new LocalFileSystem(_destinationPath);
            _localFileSystem.PrepareDirectory(_fileName);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_destinationPath);
        }

        [Test]
        public void VerifyGenerateFileName()
        {
            var generatedFileName = _localFileSystem.GenerateFileName(_fileName);
            Assert.AreEqual(_fileName, generatedFileName, "Generated file should be correct");
        }

        [Test]
        public void VerifyGenerateFileNameWhenFileExists()
        {
            var generatedFileName = _localFileSystem.GenerateFileName(_fileName);
            Assert.AreEqual(_fileName, generatedFileName, "Generated file should be correct");
        }
    }
}
