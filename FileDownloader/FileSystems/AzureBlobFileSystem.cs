using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.FileSystems
{
    public class AzureBlobFileSystem : IFileSystem
    {
        public void CreateFile(string fileName)
        {
            throw new NotImplementedException();
        }

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
