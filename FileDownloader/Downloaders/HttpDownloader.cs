namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Responsible for downloading files based on HTTP protocol
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        public string GetFilePart(string url)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks if url is valid
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>true, if valid</returns>
        public bool IsUrlValid(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
