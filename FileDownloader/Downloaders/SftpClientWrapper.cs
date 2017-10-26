using System.IO;
using Renci.SshNet;

namespace FileDownloader.Downloaders
{
    public class SftpClientWrapper: SftpClient, ISftpClientWrapper
    {
        public SftpClientWrapper(ConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }

        public SftpClientWrapper(string host, int port, string username, string password) : base(host, port, username, password)
        {
        }

        public SftpClientWrapper(string host, string username, string password) : base(host, username, password)
        {
        }

        public SftpClientWrapper(string host, int port, string username, params PrivateKeyFile[] keyFiles) : base(host, port, username, keyFiles)
        {
        }

        public SftpClientWrapper(string host, string username, params PrivateKeyFile[] keyFiles) : base(host, username, keyFiles)
        {
        }

        public Stream CreateStream(string path, FileMode fileMode)
        {
            return Open(path, fileMode);
        }

        public void ConnectClient()
        {
            Connect();
        }

        public void DisconnectClient()
        {
            Disconnect();
        }
    }
}
