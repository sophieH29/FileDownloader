using System;
using System.Configuration;
using FileDownloader.Factories;
using FileDownloader.Managers;

namespace FileDownloader
{
    class Program
    {
        public static string DestinationPath { get; set; }

        static void Main(string[] args)
        {
            string[] filesSources;

            if (args?.Length > 0)
            {
                filesSources = args;
            }
            else
            {
                filesSources = ConfigurationManager.AppSettings["sources"]?.Split(new[] { "," },
                                                            StringSplitOptions.RemoveEmptyEntries);
            }

            if (filesSources == null)
            {
                Console.WriteLine("Nothing to download");
                return;
            }

            foreach (var sourceUrl in filesSources)
            {
                Console.WriteLine($"Starting download of {sourceUrl}");
                DownloadManagerFactory downloadManagerFactory = new DownloadManagerFactory();
                IDownloadManager downloadManager = downloadManagerFactory.GetDownloadManager(sourceUrl);

                string downloadedFileName = downloadManager.DownloadFile();

                Console.WriteLine(
                    downloadedFileName != null
                        ? $"File {downloadedFileName} was successfuly downloaded from resource {sourceUrl}!"
                        : $"An error occured during download from resource {sourceUrl}!");
            }

            Console.ReadKey();
        }
    }
}
