using System;
using System.IO;
using FileDownloader.FileSystems;
using FileDownloader.Downloaders;

namespace FileDownloader.Managers
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for file downloading and storing
    /// </summary>
    public class SimpleDownloadManager : IDownloadManager
    {

        private string _fileName;
        private Stream _fileStream;

        private readonly IFileSystem _fileSystem;
        private readonly IDownloader _downloader;
        private readonly Uri _sourceUrl;

        /// <summary>
        /// Creates instance of SimpleDownloadManager
        /// </summary>
        /// <param name="fileSystem">File system to use for file managing</param>
        /// <param name="downloader">Downloader to use for file downloading</param>
        /// <param name="sourceUrl">Source url to download</param>
        public SimpleDownloadManager(IFileSystem fileSystem,
                                        IDownloader downloader,
                                        Uri sourceUrl) {
            _fileSystem = fileSystem;
            _downloader = downloader;
            _sourceUrl = sourceUrl;
            _fileName = Path.GetFileName(_sourceUrl.LocalPath);
        }

        /// <summary>
        /// StartDownload resource
        /// </summary>
        public string DownloadFile()
        {
           try
            {
                Console.WriteLine($"Preparing download for url {_sourceUrl.OriginalString}");

               
                _fileName = _fileSystem.GenerateFileName(_fileName);
                _fileSystem.PrepareDirectory(_fileName);

                _fileStream = _fileSystem.CreateStream(_fileName);
                _downloader.StartDownload(_fileStream, _sourceUrl);

                return _fileName;

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured during download: {e}");
                _fileSystem.DeleteFile(_fileName);
                return null;
            }
        }
    }
}
