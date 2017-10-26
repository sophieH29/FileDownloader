﻿using System;
using System.Configuration;
using System.IO;
using FileDownloader.Downloaders;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Renci.SshNet.Sftp;

namespace FileDownloader.Tests.Downloaders
{
    [TestFixture]
    public class SftpDownloaderTest
    {
        private Mock<SftpDownloader> _sftpDownloader;
        private Mock<Stream> _fileStream;
        private Mock<ISftpClientWrapper> _sftpClientMock;
        private Mock<SftpClientWrapper> _sftpClient;
        private Mock<SftpFileStream> _sftpFileStream;
        private readonly Uri _url = new Uri("http://aaaa/bb.jpg");

        [SetUp]
        public void Setup()
        {
            ConfigurationManager.AppSettings["sftpHost"] = "sftpHost";
            ConfigurationManager.AppSettings["sftpUserName"] = "sftpUserName";
            ConfigurationManager.AppSettings["sftpPassword"] = "sftpPassword";

            _sftpDownloader = new Mock<SftpDownloader> { CallBase = true };
            _fileStream = new Mock<Stream>();
            _sftpFileStream = new Mock<SftpFileStream>();
            _sftpClientMock = new Mock<ISftpClientWrapper>();
            _sftpClient = new Mock<SftpClientWrapper>("sftpHost", "sftpUserName", "sftpPassword") { CallBase = true };
        }

        [Test]
        public void SftpDownloader_DownloadMethod()
        {
           
            _sftpClientMock.Setup(client => client.ConnectClient()).Verifiable();
            _sftpClientMock.Setup(client => client.CreateStream(_url.LocalPath, FileMode.Open)).Returns(_sftpFileStream.Object);
            _fileStream.Object.Position = 0;

            _fileStream.Setup(stream => stream.SetLength(It.IsAny<long>())).Verifiable();

            _sftpDownloader.Protected().Setup("DoDownload", _fileStream.Object, _sftpFileStream.Object).Verifiable();
            _sftpClientMock.Setup(client => client.DisconnectClient()).Verifiable();


            _sftpDownloader.Object.Download(_fileStream.Object, _url);
        }
    }
}
