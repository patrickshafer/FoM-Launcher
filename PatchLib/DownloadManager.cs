using System.Collections.Generic;
using System.Net;

namespace FoM.PatchLib
{
    internal static class DownloadManager
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Queue<FileNode> DownloadQueue;
        private static WebClient Downloader;

        internal static void AddItem(FileNode item)
        {
            if (DownloadQueue == null)
                DownloadQueue = new Queue<FileNode>();
            DownloadQueue.Enqueue(item);
        }

        internal static void ProcessQueue()
        {
            FileNode DownloadItem;
            string DownloadURL = string.Empty;
            bool Compressed = false;

            if (Downloader == null)
                Downloader = new WebClient();

            while (DownloadQueue.Count > 0)
            {
                DownloadItem = DownloadQueue.Dequeue();
                if (DownloadItem.RemoteURLCmp == null)
                {
                    Compressed = false;
                    DownloadURL = DownloadItem.RemoteURL;
                }
                else
                {
                    Compressed = true;
                    DownloadURL = DownloadItem.RemoteURLCmp;
                }

                Log.Debug(string.Format("Downloading {0} to {1}", DownloadURL, DownloadItem.LocalFilePath));

                using (System.IO.Stream DownloadStream = Downloader.OpenRead(DownloadURL))
                {
                    using (System.IO.FileStream OutputStream = new System.IO.FileStream(DownloadItem.LocalFilePath, System.IO.FileMode.Create))
                    {
                        if (Compressed)
                        {
                            using (System.IO.Compression.DeflateStream CompressionStream = new System.IO.Compression.DeflateStream(DownloadStream, System.IO.Compression.CompressionMode.Decompress))
                            {
                                CompressionStream.CopyTo(OutputStream);
                            }
                        }
                        else
                            DownloadStream.CopyTo(OutputStream);
                    }
                }
            }
            
        }

    }
}