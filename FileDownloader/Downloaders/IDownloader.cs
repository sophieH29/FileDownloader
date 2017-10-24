using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on protocols
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
        bool IsUrlValid(string url);

        /// <summary>
        /// Download resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        void Download(Stream fileStream, Uri url);

        /// <summary>
        /// Resume download
        /// </summary>
        /// <param name="fileStream">Resumed file stream</param>
        /// <param name="url">Url of resource to download</param>
        void ResumeDownload(Stream fileStream, Uri url);
    }
}
