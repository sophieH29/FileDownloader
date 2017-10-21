namespace FileDownloader.FileSystems
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileSystem
    {
        bool FileNameExists();

        void CreateFile(string fileName);

        void DeleteFile(string fileName);

        string GenerateFileName();

        bool HasEnoughMemory();
    }
}
