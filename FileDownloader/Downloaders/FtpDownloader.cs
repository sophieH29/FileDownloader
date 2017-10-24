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

        public bool IsUrlValid(string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        public void Download(Stream fileStream, Uri url)
        {
            Console.WriteLine("Preparing download..");

            var networkStream = CreateNetworkStream(url, BytesRead);
            fileStream = PrepareStream(fileStream);

            DoDownload(fileStream, networkStream);
        }

        /// <summary>
        /// Resume download
        /// </summary>
        /// <param name="fileStream">Resumed file stream</param>
        /// <param name="url">Url of resource to download</param>
        public void ResumeDownload(Stream fileStream, Uri url)
        {
            Console.WriteLine("Resuming download..");

            var networkStream = CreateNetworkStream(url, BytesRead, true);
            fileStream = ResumeStream(fileStream);

            DoDownload(fileStream, networkStream);
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
        /// <returns>Network stream</returns>
        private Stream CreateNetworkStream(Uri url, int bytesRead, bool resuming = false)
        {
            FtpWebRequest request = CreateFtpWebRequest(url, true);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if (bytesRead > 0)  request.ContentOffset = bytesRead;

            WebResponse response = request.GetResponse();

            if (!resuming)
            {
                Size = (int)response.ContentLength;
                SizeInKb = Size / 1024;
            }

            Console.WriteLine($"Size in kb is {SizeInKb}");

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
        /// Prepares file stream by setting needed size
        /// </summary>
        /// <param name="fileStream">File stream</param>
        /// <returns>File stream with pre-defined size</returns>
        private Stream PrepareStream(Stream fileStream)
        {
            fileStream.SetLength(Size);
            return fileStream;
        }

        /// <summary>
        /// Resumes file stream by setting position form where to continue writing
        /// </summary>
        /// <param name="fileStream">File stream</param>
        /// <returns>File stream with pre-defined position</returns>
        private Stream ResumeStream(Stream fileStream)
        {
            fileStream.Position = BytesRead;
            return fileStream;
        }
    }
}
