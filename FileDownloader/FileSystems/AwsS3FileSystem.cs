using System;

namespace FileDownloader.FileSystems
{
    /// <summary>
    /// 
    /// </summary>
    public class AwsS3FileSystem : IFileSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void CreateFile(string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool FileNameExists()
        {
            throw new NotImplementedException();
        }

        public string GenerateFileName()
        {
            throw new NotImplementedException();
        }

        public bool HasEnoughMemory()
        {
            throw new NotImplementedException();
        }
    }
}
