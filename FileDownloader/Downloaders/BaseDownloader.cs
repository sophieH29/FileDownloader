using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Base implementaion of generic methods for downloading
    /// </summary>
    public abstract class BaseDownloader
    {
        /// <summary>
        /// Size of file in bytes
        /// </summary>
        protected int Size;

        /// <summary>
        /// Size of file in kb
        /// </summary>
        protected int SizeInKb;

        /// <summary>
        /// Bytes read from network stream
        /// </summary>
        protected int BytesRead;

        /// <summary>
        /// Bytes to read from stream
        /// </summary>
        protected int BytesToRead;

        /// <summary>
        /// true, if it is retry
        /// </summary>
        protected bool Retry;

        /// <summary>
        /// Max count of retries
        /// </summary>
        protected byte MaxRetry;

        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        protected abstract void Download(Stream fileStream, Uri url);

        /// <summary>
        /// Responsible for download process
        /// </summary>
        /// <param name="fileStream">File stream where to write bytes</param>
        /// <param name="networkStream">Network stream from where to get bytes</param>
        protected void DoDownload(Stream fileStream, Stream networkStream)
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
        protected void WithRetry(Action method)
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
                    if (tryCount < MaxRetry)
                    {
                        tryCount ++;
                        Retry = true;
                        Console.WriteLine($"Retry #{tryCount} downloading...");
                    }
                    else
                    {
                        Retry = false;
                        throw;
                    }
                }
            }
            while (!done);
        }
    }
}
