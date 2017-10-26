using System;
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
        private Uri Url => new Uri("http://ahdzbook.com/data/out/240/hdwp694087183.jpg");

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
            _httpDownloader.Protected().Setup("WithRetry", It.IsAny<Action>())
                .Callback<Action>(action => action());
            _httpDownloader.Protected().Setup("Download", It.IsAny<Stream>(), It.IsAny<Uri>()).Verifiable();

            
            //_fileStream.Setup(stream => stream.SetLength())
            _httpDownloader.Object.Download(_fileStream.Object, Url);
        }}
}
