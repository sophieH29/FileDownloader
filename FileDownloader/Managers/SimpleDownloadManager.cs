using System;
using System.IO;
using FileDownloader.FileSystems;
using FileDownloader.Downloaders;
using FileDownloader.Enums;

namespace FileDownloader.Managers
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for file downloading and storing
    /// </summary>
    public class SimpleDownloadManager : IDownloadManager
    {
        public DownloadStatusEnum Status = DownloadStatusEnum.Preparing;
        public int Size;
        public int SizeInKb;
        public int BytesRead;
        public string FullFileName;
        public string FileName;
        public Uri Url;

        private Stream _fileStream;
        private Stream _networkStream;


        private IFileSystem _fileSystem;
        private IDownloader _downloader;
        private readonly Uri _sourceUrl;
        private const string DefaultDestinationPath = @"D:\Projects\DownloadedFiles";

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
            FileName = sourceUrl.IsFile ? Path.GetFileName(_sourceUrl.LocalPath) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DownloadFile()
        {
            if (!_sourceUrl.IsFile)
            {
                Console.WriteLine($"Url {_sourceUrl} is invalid");
            }
//           long iFileSize = 0;
//            int iBufferSize = 1024;
//            iBufferSize *= 1000;
//            long iExistLen = 0;
//            var sDestinationPath = DefaultDestinationPath + @"\soap-bubble-1958650_960_720.jpg";
//            System.IO.FileStream saveFileStream;
//            if (System.IO.File.Exists(sDestinationPath))
//            {
//                System.IO.FileInfo fINfo = new System.IO.FileInfo(sDestinationPath);
//                iExistLen = fINfo.Length;
//            }
//
//            Console.WriteLine($"Downloading bytes of {sDestinationPath}");
//            if (iExistLen > 0)
//                saveFileStream = new System.IO.FileStream(sDestinationPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
//            else
//                saveFileStream = new System.IO.FileStream(sDestinationPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
//
//
//            System.Net.HttpWebRequest hwRq;
//            System.Net.HttpWebResponse hwRes;
//            hwRq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_sourceUrl);
//            hwRq.AddRange((int)iExistLen);
//            System.IO.Stream smRespStream;
//            hwRes = (System.Net.HttpWebResponse)hwRq.GetResponse();
//            smRespStream = hwRes.GetResponseStream();
//
//            iFileSize = hwRes.ContentLength;
//
//            Console.WriteLine($"File size: {iFileSize}");
//
//            int iByteSize;
//            byte[] downBuffer = new byte[iBufferSize];
//
//            while ((iByteSize = smRespStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
//            {
//                saveFileStream.Write(downBuffer, 0, iByteSize);
//            }
//
//            Console.WriteLine("Done");
        }

        private void DoDownload()
        {
           
        }

        private void PrepareDownload()
        {
            try
            {
                Console.WriteLine("Preparing download for url " + Url.OriginalString + ". localpath=" + FullFileName);

                FileName = _fileSystem.GenerateFileName(FileName);
                _fileSystem.PrepareDirectory(FileName);

              
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CreateStreams(bool resuming)
        {
            _fileStream = !resuming ? 
                _fileSystem.CreateStream(Size, FileName) :
                _fileSystem.ResumeStream(BytesRead, FileName);

            
        }
    }
}
