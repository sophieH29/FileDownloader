using System;
using System.IO;

namespace FileDownloader.FileSystems
{
    /// <summary>
    /// Defines Local File System
    /// </summary>
    public class LocalFileSystem : IFileSystem
    {
        private const string DefaultDestinationPath = @"D:\Projects\DownloadedFiles";
        private readonly string _destinationPath;

        public LocalFileSystem(string destinationPath)
        {
            _destinationPath = destinationPath ?? DefaultDestinationPath;
        }

        /// <summary>
        /// Saves file on the file system
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="filePath">File path</param>
        /// <returns>true, if file name successfuly saved</returns>
        public bool SaveFile(string fileName, string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes file from file system
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="filePath">File path</param>
        public void DeleteFile(string fileName, string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates new file name based on one which already exists on the file system
        /// </summary>
        /// <returns>New file name</returns>
        public string GenerateFileName(string fileName)
        {
            string fullFileName = GetFullFileName(fileName);

            if (!File.Exists(fullFileName)) return fullFileName;

            string fileExtension = Path.GetExtension(fullFileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFileName);
            fullFileName = fileNameWithoutExtension + Guid.NewGuid() + fileExtension;

            return fullFileName;
        }

        public void PrepareDirectory(string fileName)
        {
           var fileInfo = new FileInfo(GetFullFileName(fileName));
            if (fileInfo.DirectoryName != null && !Directory.Exists(fileInfo.DirectoryName))
                Directory.CreateDirectory(fileInfo.DirectoryName);
        }

        public Stream CreateStream(int size, string fileName)
        {
            Console.WriteLine("Creating local file stream...");

            var fullFileName = GetFullFileName(fileName);
            return new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        }

        public Stream ResumeStream(int bytesRead, string fileName)
        {
            Console.WriteLine("Resuming local file stream...");

            var fullFileName = GetFullFileName(fileName);
            return new FileStream(fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        private string GetFullFileName(string fileName)
        {
            return $@"{_destinationPath}\{fileName}";
        }
    }
}
