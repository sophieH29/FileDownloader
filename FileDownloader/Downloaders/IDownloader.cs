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
    }
}
