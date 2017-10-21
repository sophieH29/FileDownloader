namespace FileDownloader.Downloaders
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsUrlValid();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetFilePart();
    }
}
