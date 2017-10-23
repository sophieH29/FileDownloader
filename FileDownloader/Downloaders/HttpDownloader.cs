using System;
using System.IO;
using System.Net;
using FileDownloader.Enums;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on HTTP protocol
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        public DownloadStatusEnum Status = DownloadStatusEnum.Preparing;
        public int Size; 
        public int SizeInKb;
        public int BytesRead;
        public string FullFileName;
        public string FileName;
        public Uri Url;

        private Stream _fileStream;
        private Stream _networkStream;

        /// <summary>
        /// Download speed in kilobytes/sec
        /// </summary>
        public double Speed = 0;

        public void DoDownload(Uri url, string localfilename)
        {
            
        }

        public string GetFilePart(string url)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
        public bool IsUrlValid(string url)
        {
            throw new System.NotImplementedException();
        }

        public void PrepareDownload(Uri url, Stream fileStream)
        {
            try
            {
                _fileStream = fileStream;
                _fileStream.SetLength(Size);
                CreateNetworkStream(false, url, BytesRead);

                Status = DownloadStatusEnum.Prepared;
                Console.WriteLine("Prepared download.");
            }
            catch (Exception e)
            {
                Status = DownloadStatusEnum.Error;
                Console.WriteLine("Error occured: " + e.Message);
            }
        }

      

        public void CreateNetworkStream(bool resuming, Uri url, int bytesRead)
        {
            Console.WriteLine("Creating network stream.");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            if (bytesRead > 0) request.AddRange(BytesRead);

            WebResponse response = request.GetResponse();

            if (!resuming)
            {
                Size = (int)response.ContentLength;
                SizeInKb = (int)Size / 1024;
            }

            //create network stream
            _networkStream =  response.GetResponseStream();
        }

        private void StartDownload()
        {
            try
            {
                Status = DownloadStatusEnum.Running;
                byte[] buffer = new byte[4096];
                int bytesToRead = Size;
                int byteSize;

                while ((byteSize = _networkStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    _fileStream.Write(buffer, 0, byteSize);
                    _fileStream.Flush();
                    BytesRead += byteSize;
                    bytesToRead -= byteSize;
                }

                _networkStream.Close();
                _networkStream = null;
                _fileStream.Close();

                Status = DownloadStatusEnum.Completed;
            }
            catch (Exception e)
            {
                Status = DownloadStatusEnum.Error;
            }
        }

        private void ResumeDownload(Stream fileStream, Uri url)
        {
            _fileStream = fileStream;
            _fileStream.Position = BytesRead;

            CreateNetworkStream(true, url, BytesRead);
            StartDownload();
        }
    }
}
