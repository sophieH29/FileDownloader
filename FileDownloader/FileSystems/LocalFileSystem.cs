using System;

namespace FileDownloader.FileSystems
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalFileSystem : IFileSystem
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool FileNameExists()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GenerateFileName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasEnoughMemory()
        {
            throw new NotImplementedException();
        }
    }
}
