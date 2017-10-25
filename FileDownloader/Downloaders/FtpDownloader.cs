using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on FTP protocol
    /// </summary>
    public class FtpDownloader : IDownloader
    {
        public int Size;
        public int SizeInKb;
        public int BytesRead;
        public int BytesToRead;

        private bool _retry;
        private readonly int _maxRetry;

        /// <summary>
        /// Initiates FTP Downloader with defined maximum retries count
        /// </summary>
        public FtpDownloader()
        {
            _maxRetry = Int16.Parse(ConfigurationManager.AppSettings["ftpRetryCount"]);
        }

        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
        public bool IsUrlValid(string url)
        {
            Uri uri;
            return Uri.TryCreate(url, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeFtp);
        }

        /// <summary>
        /// StartDownload resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        public void StartDownload(Stream fileStream, Uri url)
        {
            var downloadAction = new Action(delegate
            {
                Download(fileStream, url);
            });

            WithRetry(downloadAction);
        }

        /// <summary>
        /// StartDownload resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        public void Download(Stream fileStream, Uri url)
        {
            Console.WriteLine("Preparing download..");
            var networkStream = CreateNetworkStream(url, BytesRead);

            try
            {
                fileStream.SetLength(Size);
                DoDownload(fileStream, networkStream);
            }
            catch (Exception)
            {
                networkStream.Close();
                throw;
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
        /// Prepares network stream
        /// </summary>
        /// <param name="url">Resource url</param>
        /// <param name="bytesRead">Bytes already read</param>
        /// <returns>Network stream</returns>
        private Stream CreateNetworkStream(Uri url, int bytesRead)
        {
            FtpWebRequest request = CreateFtpWebRequest(url, true);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if (bytesRead > 0)  request.ContentOffset = bytesRead;

            WebResponse response = request.GetResponse();

            if (!_retry)
            {
                Size = (int)response.ContentLength;
                SizeInKb = Size / 1024;
                Console.WriteLine($"Size in kb is {SizeInKb}");
            }

            //create network stream
            return response.GetResponseStream();
        }

        private FtpWebRequest CreateFtpWebRequest(Uri ftpDirectoryPath, bool keepAlive = false)
        {
            var userName = ConfigurationManager.AppSettings["ftpUserName"];
            var password = ConfigurationManager.AppSettings["ftpPassword"];

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpDirectoryPath);

            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(userName, password);

            return request;
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
                        tryCount++;
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
