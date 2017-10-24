using System;
using System.Configuration;

using FileDownloader.Downloaders;
using FileDownloader.FileSystems;
using FileDownloader.Managers;
using FileDownloader.Enums;

namespace FileDownloader.Factories
{
    /// <summary>
    /// Responsible for creating instance of specific Download manager based on parameters
    /// </summary>
    public class DownloadManagerFactory
    {
        /// <summary>
        /// Creates instance of specific Download manager based on parameters
        /// </summary>
        /// <param name="url">File source to download</param>
        /// <returns>instance of IDownloadManager</returns>
        public IDownloadManager GetDownloadManager(string url)
        {

            var uri = new Uri(url);
            var protocol = uri.Scheme;
            IDownloader downloader = GetDownloader(protocol);

            return downloader != null ? new SimpleDownloadManager(GetFileSystem(), downloader, uri) : null;
        }

        /// <summary>
        /// Get instance of downloader based on protocol
        /// </summary>
        /// <param name="protocol">Uri scheme</param>
        /// <returns>Instance of IDownloader</returns>
        private IDownloader GetDownloader(string protocol)
        {
            ProtocolTypes protocolType;
            if (Enum.TryParse(protocol, out protocolType))
            {
                switch (protocolType)
                {
                    case ProtocolTypes.http:
                    case ProtocolTypes.https:
                    {
                        return new HttpDownloader();
                    }
                    case ProtocolTypes.ftp:
                    {
                        return new FtpDownloader();
                    }
                    case ProtocolTypes.sftp:
                    {
                        return new SftpDownloader();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get instance of storage based on configuration
        /// </summary>
        /// <returns>Instance of IFileSystem</returns>
        private IFileSystem GetFileSystem()
        {
            var destinationPath = ConfigurationManager.AppSettings["destinationPath"];
            var storage = ConfigurationManager.AppSettings["storageType"];

            StorageTypes storageType;

            if (Enum.TryParse(storage, out storageType))
            {
                switch (storageType)
                {
                    case StorageTypes.local:
                    {
                        return new LocalFileSystem(destinationPath);
                    }
                    case StorageTypes.aws:
                    {
                        return new AwsS3FileSystem(destinationPath);
                    }
                    case StorageTypes.azure:
                    {
                        return new AzureBlobFileSystem(destinationPath);
                    }
                }
            }

            return new LocalFileSystem(null);
        }
    }
}
