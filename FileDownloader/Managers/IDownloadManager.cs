namespace FileDownloader.Managers
{
    /// <summary>
    /// Responsible for file downloading and storing
    /// </summary>
    public interface IDownloadManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        void DownloadFile(string source);
    }
}
