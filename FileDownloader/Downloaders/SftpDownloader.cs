using System;
using System.Configuration;
using System.IO;
using Renci.SshNet;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on SFTP protocol
    /// </summary>
    class SftpDownloader : IDownloader
    {
        public int Size;
        public int SizeInKb;
        public int BytesRead;
        public int BytesToRead;

        private bool _retry;
        private readonly int _maxRetry;

        /// <summary>
        /// Initiates SFTP Downloader with defined maximum retries count
        /// </summary>
        public SftpDownloader()
        {
            _maxRetry = Int16.Parse(ConfigurationManager.AppSettings["sftpRetryCount"]);
        }

        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
        public bool IsUrlValid(string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri) && string.Equals(uri.Scheme, "sftp", StringComparison.OrdinalIgnoreCase);
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

            string sourceUrl = url.LocalPath;


            using (var client = new SftpClient(host, username, password))
            {
                client.Connect();

                if (!client.Exists(sourceUrl))
                {
                    Console.WriteLine($"File source {sourceUrl} does not exists...");
                    return;
                }

                var downloadAction = new Action(delegate
                {
                    Download(fileStream, sourceUrl, client);
                });

                WithRetry(downloadAction);

                client.Disconnect();
            }
        }

        /// <summary>
        /// StartDownload resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        /// <param name="client">SFTP Client</param>
        private void Download(Stream fileStream, string url, SftpClient client)
        {
            using (var sourceStream = client.Open(url, FileMode.Open))
            {
                if(BytesRead > 0) sourceStream.Seek(BytesRead, SeekOrigin.Begin);

                if (!_retry)
                {
                    Size = (int)sourceStream.Length;
                    SizeInKb = Size / 1024;
                    Console.WriteLine($"Size in kb is {SizeInKb}");
                }

                DoDownload(fileStream, sourceStream);
            }
        }

        /// <summary>
        /// Responsible for download process
        /// </summary>
        /// <param name="fileStream">File stream where to write bytes</param>
        /// <param name="networkStream">Network stream from where to get bytes</param>
        private void DoDownload(Stream fileStream, Stream networkStream)
        {
            byte[] buffer = new byte[10240];
            BytesToRead = Size;
            int byteSize;

            while ((byteSize = networkStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, byteSize);
                fileStream.Flush();
                BytesRead += byteSize;
                BytesToRead -= byteSize;
            }

            networkStream.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Retries of execute downloading of specified amount of times
        /// </summary>
        /// <param name="method">Method to execute</param>
        private void WithRetry(Action method)
        {
            int tryCount = 0;
            bool done = false;
            do
            {
                try
                {
                    method();
                    done = true;
                }
                catch (Exception)
                {
                    if (tryCount < _maxRetry)
                    {
                        tryCount ++;
                        _retry = true;
                        Console.WriteLine($"Retry #{tryCount} downloading...");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            while (!done);
        }
    }
}
