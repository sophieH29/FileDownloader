using System;
using System.Text.RegularExpressions;
using FileDownloader.Downloaders;
using FileDownloader.Enums;
using FileDownloader.Factories;
using FileDownloader.FileSystems;
using FileDownloader.Managers;

namespace FileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hey! Please type a source url from where you would like to download the file");
            string sourceUrl = Console.ReadLine();
            Console.WriteLine($"OK. Starting downloading the file from '{sourceUrl}'");

            Regex protocolRegex = new Regex(@"^(?<proto>\w+)://",
                         RegexOptions.None, TimeSpan.FromMilliseconds(150));
            Match protocolMatch = protocolRegex.Match(sourceUrl);

            if (!protocolMatch.Success)
            {
                Console.WriteLine("Your url is invalid. Please, try again...");
                return;
            }

            Console.WriteLine(protocolRegex.Match(sourceUrl).Result("You have specified url of your file using '${proto}' protocol"));
            string protocol = protocolMatch.Groups["proto"].Value;

            DownloadManagerFactory downloadManagerFactory = new DownloadManagerFactory();
            IDownloadManager downloadManager = null;

            ProtocolTypes protocolType;

            if (Enum.TryParse(protocol, out protocolType))
            {
                switch (protocolType)
                {
                    case ProtocolTypes.http:
                    {
                        downloadManager = downloadManagerFactory.GetDownloadManager(new LocalFileSystem(), new HttpDownloader());
                        break;
                    }
                    case ProtocolTypes.ftp:
                    {
                        downloadManager = downloadManagerFactory.GetDownloadManager(new LocalFileSystem(), new FtpDownloader());
                        break;
                    }
                    case ProtocolTypes.sftp:
                    {
                        downloadManager = downloadManagerFactory.GetDownloadManager(new LocalFileSystem(), new SftpDownloader());
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Sorry, there is no possibiluty to download file under {protocol} yet...");
            }

            if (downloadManager != null)
            {
                //TODO: do download and file saving
            }


            Console.ReadKey();
        }
    }
}
