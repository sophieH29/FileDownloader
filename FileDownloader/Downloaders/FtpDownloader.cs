using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on FTP protocol
    /// </summary>
    public class FtpDownloader : IDownloader
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
