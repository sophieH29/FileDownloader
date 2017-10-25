using System;
using System.IO;
using FileDownloader.Downloaders;
using FileDownloader.FileSystems;
using FileDownloader.Managers;
using Moq;
using NUnit.Framework;

namespace FileDownloader.Tests
{
    [TestFixture]
    public class SimpleDownloadManagerTest
    {
        private Mock<IFileSystem> _fileSystemMock;
        private Mock<IDownloader> _downloaderMock;
        private Uri Url => new Uri("http://ahdzbook.com/data/out/240/hdwp694087183.jpg");
        private const string FileName = "hdwp694087183.jpg";
        private Stream _fileStream;

        [SetUp]
        public void Setup()
        {
            _fileSystemMock = new Mock<IFileSystem>();
            _downloaderMock = new Mock<IDownloader>();

            _fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        }

        [TearDown]
        public void TearDown()
        {
            _fileSystemMock.Verify();
            _downloaderMock.Verify();

            _fileStream.Close();
        }

        [Test]
        public void VerifyDownloaderManager()
        {
            _fileSystemMock.Setup(fileSystem => fileSystem.GenerateFileName(FileName)).Returns(FileName);
            _fileSystemMock.Setup(fileSystem => fileSystem.PrepareDirectory(FileName)).Verifiable();
            _fileSystemMock.Setup(fileSystem => fileSystem.CreateStream(FileName)).Returns(_fileStream);
            _downloaderMock.Setup(downloader => downloader.StartDownload(_fileStream, Url)).Verifiable();
            
            var simpleDownloadManager = new SimpleDownloadManager(_fileSystemMock.Object, _downloaderMock.Object, Url);
            var fileName = simpleDownloadManager.DownloadFile();

            Assert.AreEqual(FileName, fileName, $"Downloaded file should be equal {FileName}");
        }
    }
}
