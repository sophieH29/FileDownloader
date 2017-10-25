using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.Downloaders
{
    /// <summary>
    /// Base implementaion of generic methods for downloading
    /// </summary>
    abstract class BaseDownloader
    {
        protected int Size;
        protected int SizeInKb;
        protected int BytesRead;
        protected int BytesToRead;

        protected bool _retry;
        protected int _maxRetry;

       

        /// <summary>
        /// Retries of execute downloading of specified amount of times
        /// </summary>
        /// <param name="method">Method to execute</param>
        protected void WithRetry(Action method)
        {
            int tryCount = 0;
            bool done = false;
            do
            {
                try
                {
                    method();
                    done = true;
                }
                catch (Exception)
                {
                    if (tryCount < _maxRetry)
                    {
                        tryCount++;
                        _retry = true;
                        Console.WriteLine($"Retry #{tryCount} downloading...");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            while (!done);
        }
    }
}
