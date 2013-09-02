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
            if (Downloader == null)
                Downloader = new WebClient();


            while (DownloadQueue.Count > 0)
            {
                DownloadItem = DownloadQueue.Dequeue();
                Log.Debug(string.Format("Downloading {0} to {1}", DownloadItem.RemoteURL, DownloadItem.LocalFilePath));
                Downloader.DownloadFile(DownloadItem.RemoteURL, DownloadItem.LocalFilePath);
            }
            
        }

    }
}