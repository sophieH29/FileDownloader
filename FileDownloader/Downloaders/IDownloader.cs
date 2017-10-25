﻿using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on protocols
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// StartDownload resource
        /// </summary>
        /// <param name="fileStream">File stream where downloaded bytes will be written</param>
        /// <param name="url">Url of resource to download</param>
        void StartDownload(Stream fileStream, Uri url);
    }
}
