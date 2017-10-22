using FileDownloader.FileSystems;
using FileDownloader.Downloaders;

namespace FileDownloader.Managers
{
    /// <summary>
    /// Responsible for file downloading and storing
    /// </summary>
    public class SimpleDownloadManager : IDownloadManager
    {

        private IFileSystem _fileSystem;
        private IDownloader _downloader;

        /// <summary>
        /// Creates instance of SimpleDownloadManager
        /// </summary>
        /// <param name="fileSystem">File system to use for file managing</param>
        /// <param name="downloader">Downloader to use for file downloading</param>
        public SimpleDownloadManager(IFileSystem fileSystem, IDownloader downloader) {
            _fileSystem = fileSystem;
            _downloader = downloader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void DownloadFile(string source)
        {
            throw new System.NotImplementedException();
        }
    }
}
