using System;

namespace FileDownloader.FileSystems
{
    /// <summary>
    /// Defines Amazon storage file system
    /// TODO: Implement all the methods.
    /// </summary>
    public class AwsS3FileSystem : IFileSystem
    {
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
        /// Checks if file already exists with that name
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>true, if file name exists</returns>
        public bool FileNameExists(string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates new file name based on one which already exists on the file system
        /// </summary>
        /// <returns>New file name</returns>
        public string GenerateFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks disc memory capacity
        /// </summary>
        /// <returns>true, if there is enough memory</returns>
        public bool HasEnoughMemory()
        {
            throw new NotImplementedException();
        }
    }
}
