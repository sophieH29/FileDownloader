using System;
using System.IO;
using System.Net;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on HTTP protocol
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        public int Size;
        public int SizeInKb;
        public int BytesRead;
        public int BytesToRead;

        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
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

            var networkStream = CreateNetworkStream(false, url, BytesRead);
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

            var networkStream = CreateNetworkStream(true, url, BytesRead);
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
        /// <param name="resuming">true, if needs to resume download</param>
        /// <param name="url">Resource url</param>
        /// <param name="bytesRead">Bytes already read</param>
        /// <returns>Network stream</returns>
        private Stream CreateNetworkStream(bool resuming, Uri url, int bytesRead)
        {
            Console.WriteLine("Creating network stream...");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            if (bytesRead > 0) request.AddRange(BytesRead);

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
