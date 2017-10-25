using System.Collections.Generic;
using FileDownloader.Downloaders;
using FileDownloader.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileDownloader.Tests.Factories
{
	[TestClass]
	public class DownloadManagerFactoryTest
	{
		private DownloadManagerFactory _factory;

		[TestInitialize]
		public void Init()
		{
			_factory = DownloadManagerFactory.GetInstance();
		}

		[TestMethod]
		public void GetDownloadManager_NullExpected()
		{
			var source = "some://test/string";
			var type = _factory.GetDownloadManager(source);
			Assert.IsNull(type);
		}

		[TestMethod]
		public void GetDownloadManager_HTTP()
		{
			var source = "http://test/string";
			var type = _factory.GetDownloadManager(source);
			var downloader = type as HttpDownloader;
			Assert.IsTrue(downloader != null);

			source = "https://test/string";
			type = _factory.GetDownloadManager(source);
			downloader = type as HttpDownloader;
			Assert.IsTrue(downloader != null);
		}
	}
}
