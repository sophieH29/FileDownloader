using System;
using System.IO;
using System.Net;

namespace FileDownloader.Downloaders
{
    /// <inheritdoc cref="BaseDownloader" />
    /// <summary>
    /// Responsible for downloading files based on HTTP protocol
    /// </summary>
    public class HttpDownloader : BaseDownloader, IDownloader
    {
        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        /// <param name="retry">true, if it is retry</param>
        public void Download(Stream fileStream, Uri url, bool retry = false)
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
        /// Prepares network stream
        /// </summary>
        /// <param name="url">Resource url</param>
        /// <param name="bytesRead">Bytes already read</param>
        /// <param name="retry">true, if it is retry</param>
        /// <returns>Network stream</returns>
        private Stream CreateNetworkStream(Uri url, int bytesRead, bool retry = false)
        {
            Console.WriteLine("Creating network stream...");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            if (bytesRead > 0) request.AddRange(BytesRead);

            WebResponse response = request.GetResponse();

            if (!retry)
            {
                Size = (int)response.ContentLength;
                SizeInKb = Size / 1024;
                Console.WriteLine($"Size in kb is {SizeInKb}");
            }

            //create network stream
            return response.GetResponseStream();
        }
    }
}
