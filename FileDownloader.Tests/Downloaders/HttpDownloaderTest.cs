using System.IO;
using FileDownloader.Downloaders;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace FileDownloader.Tests.Downloaders
{
    [TestFixture]
    public class HttpDownloaderTest
    {
        private Mock<HttpDownloader> _httpDownloader;
        private Mock<Stream> _fileStream;

        [SetUp]
        public void Setup()
        {
            _httpDownloader = new Mock<HttpDownloader> {CallBase = true};
            _fileStream = new Mock<Stream>();
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void VerifyStartDownloader()
        {
            _httpDownloader.Protected().Setup("WithRetry").Verifiable();
            //_fileStream.Setup(stream => stream.SetLength())
            //_httpDownloader.StartDownload();
        }
    }
}
