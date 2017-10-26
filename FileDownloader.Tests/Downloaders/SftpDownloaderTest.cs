using System;
using System.Configuration;
using System.IO;
using FileDownloader.Downloaders;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace FileDownloader.Tests.Downloaders
{
    [TestFixture]
    public class SftpDownloaderTest
    {
        private Mock<SftpDownloader> _sftpDownloader;
        private Mock<Stream> _fileStream;
        private Mock<Stream> _networkStream;
        private Mock<SftpClient> _sftpClient;
        private Mock<SftpFileStream> _sftpFileStream;
        private readonly Uri _url = new Uri("http://aaaa/bb.jpg");

        [SetUp]
        public void Setup()
        {
            ConfigurationManager.AppSettings.Add("sftpHost", "sftpHost");
            ConfigurationManager.AppSettings.Add("sftpUserName", "sftpUserName");
            ConfigurationManager.AppSettings.Add("sftpPassword", "sftpPassword");

            _sftpDownloader = new Mock<SftpDownloader> { CallBase = true };
            _fileStream = new Mock<Stream>();
            _networkStream = new Mock<Stream>();
            _sftpFileStream = new Mock<SftpFileStream>();
            _sftpClient = new Mock<SftpClient>("sftpHost", "sftpUserName", "sftpPassword") { CallBase = true };
        }

        [Test]
        public void FtpDownloader_DownloadMethod()
        {
            _sftpClient.Setup(client => client.Connect()).Verifiable();
            _sftpClient.Setup(client => client.Open(_url.LocalPath, FileMode.Open)).Returns(_sftpFileStream.Object);
            _fileStream.Object.Position = 0;

            _fileStream.Setup(stream => stream.SetLength(It.IsAny<long>())).Verifiable();

            _sftpDownloader.Protected().Setup("DoDownload", _fileStream.Object, _sftpFileStream.Object).Verifiable();
            _sftpClient.Setup(client => client.Disconnect()).Verifiable();


            _sftpDownloader.Object.Download(_fileStream.Object, _url);
        }
    }
}
