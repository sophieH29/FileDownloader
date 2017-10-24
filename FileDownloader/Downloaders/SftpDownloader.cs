using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on SFTP protocol
    /// </summary>
    class SftpDownloader : IDownloader
    {
        public bool IsUrlValid(string url)
        {
            throw new NotImplementedException();
        }

        public void Download(Stream fileStream, Uri url)
        {
            throw new NotImplementedException();
        }

        public void ResumeDownload(Stream fileStream, Uri url)
        {
            throw new NotImplementedException();
        }
    }
}
