using System.IO;

namespace FileDownloader.FileSystems
{
    /// <summary>
    /// File System interface
    /// </summary>
    public interface IFileSystem
    {
       
        /// <summary>
        /// Saves file on the file system
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="filePath">File path</param>
        /// <returns>true, if file name successfuly saved</returns>
        bool SaveFile(string fileName, string filePath);

        /// <summary>
        /// Deletes file from file system
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="filePath">File path</param>
        void DeleteFile(string fileName, string filePath);

        /// <summary>
        /// Generates new file name based on one which already exists on the file system
        /// </summary>
        /// <returns>New file name</returns>
        string GenerateFileName(string fileName);

        void PrepareDirectory(string fileName);

        Stream CreateStream(string fileName);

        Stream ResumeStream(string fileName);
    }
}
