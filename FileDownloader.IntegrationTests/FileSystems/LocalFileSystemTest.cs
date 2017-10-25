using System.IO;
using FileDownloader.FileSystems;
using NUnit.Framework;

namespace FileDownloader.IntegrationTests.FileSystems
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
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText($@"{_destinationPath}\{_fileName}"))
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome");
            }

            var generatedFileName = _localFileSystem.GenerateFileName(_fileName);

            _localFileSystem.DeleteFile(_fileName);
            Assert.AreNotEqual(_fileName, generatedFileName, "Should be generated new file name");
        }
    }

}
