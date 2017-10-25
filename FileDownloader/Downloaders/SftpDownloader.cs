using System;
using System.Configuration;
using System.IO;
using Renci.SshNet;

namespace FileDownloader.Downloaders
{
    /// <inheritdoc cref="BaseDownloader" />
    /// <summary>
    /// Responsible for downloading files based on SFTP protocol
    /// </summary>
    public class SftpDownloader : BaseDownloader, IDownloader
    {
        private SftpClient _client;

        /// <summary>
        /// Initiates SFTP Downloader with defined maximum retries count
        /// </summary>
        public SftpDownloader()
        {
            if (!byte.TryParse(ConfigurationManager.AppSettings["sftpRetryCount"], out MaxRetry))
            {
                MaxRetry = 10;
            }
        }

        /// <summary>
        /// StartDownload resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        public void StartDownload(Stream fileStream, Uri url)
        {
            string host = ConfigurationManager.AppSettings["sftpHost"];
            string username = ConfigurationManager.AppSettings["sftpUserName"];
            string password = ConfigurationManager.AppSettings["sftpPassword"];

            using (_client = new SftpClient(host, username, password))
            {
                _client.Connect();

                WithRetry(() => Download(fileStream, url));

                _client.Disconnect();
            }
        }

        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        protected override void Download(Stream fileStream, Uri url)
        {
            using (var sourceStream = _client.Open(url.LocalPath, FileMode.Open))
            {
                if(BytesRead > 0) sourceStream.Seek(BytesRead, SeekOrigin.Begin);

                if (!Retry)
                {
                    Size = (int)sourceStream.Length;
                    SizeInKb = Size / 1024;
                    Console.WriteLine($"Size in kb is {SizeInKb}");
                }

                DoDownload(fileStream, sourceStream);
            }
        }}
}
