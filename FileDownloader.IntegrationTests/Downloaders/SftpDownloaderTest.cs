﻿using System;
using System.Configuration;
using System.IO;
using FileDownloader.Downloaders;
using NUnit.Framework;

namespace FileDownloader.IntegrationTests.Downloaders
{
    [TestFixture]
    public class SftpDownloaderTest
    {
        private readonly Uri _url = new Uri(ConfigurationManager.AppSettings["sftpTestSource"]);
        private string _destinationPath = @"C:\TestDownloadedFiles";
        private string _fileName = "agoda.jpg";
        private SftpDownloader _sftpDownloader;
        private Stream _fileStream;
        private string FullFileName => $@"{_destinationPath}\{_fileName}";

        [SetUp]
        public void Setup()
        {
            _sftpDownloader = new SftpDownloader();
            Directory.CreateDirectory(_destinationPath);
            _fileStream = new FileStream(FullFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_destinationPath);
        }

        [Test]
        public void VerifyDwonload()
        {
            _sftpDownloader.StartDownload(_fileStream, _url);

            Assert.True(File.Exists(FullFileName), "File wasn't downloaded");

            File.Delete(FullFileName);
        }
    }
}