using FileDownloader.Downloaders;
using FileDownloader.FileSystems;
using FileDownloader.Managers;

namespace FileDownloader.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class DownloadManagerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="downloader"></param>
        /// <returns></returns>
        IDownloadManager GetDownloaderManager(IFileSystem fileSystem, IDownloader downloader) {
            return null;
        }
    }
}
