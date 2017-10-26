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
        /// <summary>
        /// Host of SFTP connection
        /// </summary>
        private readonly string _host;

        /// <summary>
        /// Username of SFTP connection
        /// </summary>
        private readonly string _username;

        /// <summary>
        /// Password of SFTP connection
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// Initiates SFTP Downloader with defined maximum retries count
        /// </summary>
        public SftpDownloader()
        {
            _host = ConfigurationManager.AppSettings["sftpHost"];
            _username = ConfigurationManager.AppSettings["sftpUserName"];
            _password = ConfigurationManager.AppSettings["sftpPassword"];
        }

        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        /// <param name="retry">true, if it is retry</param>
        public void Download(Stream fileStream, Uri url, bool retry = false)
        {
            using (var client = new SftpClient(_host, _username, _password))
            {
                client.Connect();

                using (var sourceStream = client.Open(url.LocalPath, FileMode.Open))
                {
                    if (BytesRead > 0) sourceStream.Seek(BytesRead, SeekOrigin.Begin);

                    if (!retry)
                    {
                        Size = (int)sourceStream.Length;
                        SizeInKb = Size / 1024;
                        Console.WriteLine($"Size in kb is {SizeInKb}");
                    }

                    DoDownload(fileStream, sourceStream);
                }

                client.Disconnect();
            }
        }
    }
}
