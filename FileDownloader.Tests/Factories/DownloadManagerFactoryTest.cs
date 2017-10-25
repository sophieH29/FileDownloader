using System.Collections.Generic;
using FileDownloader.Downloaders;
using FileDownloader.Factories;
using NUnit.Framework;

namespace FileDownloader.Tests.Factories
{
	[TestFixture]
	public class DownloadManagerFactoryTest
	{
		private DownloadManagerFactory _factory;
		private List<string> _successfulSources;
		private string _unsucessfulSource;

		[SetUp]
		public void SetUp()
		{
			_factory = DownloadManagerFactory.GetInstance();
			_successfulSources = new List<string>
			{
				"http://test/string",
				"https://test/string",
				"ftp://test/string",
				"sftp://test/string"
			};
			_unsucessfulSource = "some://test/string";
		}

		[Test]
		public void GetDownloadManager_NullExpected()
		{
			var type = _factory.GetDownloadManager(_unsucessfulSource);
			Assert.IsNull(type);
		}

		[Test]
		public void GetDownloadManager_ManagerExpected()
		{
			foreach (var source in _successfulSources)
			{
				var type = _factory.GetDownloadManager(source);
				Assert.IsNotNull(type);
			}
		}
	}
}
