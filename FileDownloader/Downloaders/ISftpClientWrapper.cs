using System;
using System.IO;

namespace FileDownloader.Downloaders
{
    public interface ISftpClientWrapper : IDisposable
    {
        Stream CreateStream(string path, FileMode fileMode);
        void ConnectClient();
        void DisconnectClient();
    }
}
