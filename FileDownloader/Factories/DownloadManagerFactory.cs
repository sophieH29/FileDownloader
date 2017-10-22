using FileDownloader.Downloaders;
using FileDownloader.FileSystems;
using FileDownloader.Managers;

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
        /// <param name="fileSystem">Specific file system</param>
        /// <param name="downloader">Specific downloader</param>
        /// <returns>instance of IDownloadManager</returns>
        IDownloadManager GetDownloadManager(IFileSystem fileSystem, IDownloader downloader) {
            return new SimpleDownloadManager(fileSystem, downloader);
        }
    }
}
