using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.ComponentModel;
using System.Reflection;


namespace FoM.PatchLib
{
    /// <summary>
    /// Entry-point for managing the patch process
    /// </summary>
    public static class PatchManager
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Mutex PatchMutex;
        private static BackgroundWorker UpdateCheckBW;

        public static bool BootstrapMode = false;


        /// <summary>
        /// Applies a patch (manifest) to a given folder/directory
        /// </summary>
        /// <param name="PatchManifest">Manifest object to apply the patch from</param>
        public static void ApplyPatch(Manifest PatchManifest)
        {
            foreach (FileNode PatchFile in PatchManifest.FileList)
                if (PatchFile.CheckUpdate())
                    PatchFile.ApplyUpdate();
        }
        public static void ApplicationStart(string SelfUpdateURL)
        {
            if (AcquireLock())
            {
                Manifest SelfUpdateManifest = SelfUpdateNeeded(SelfUpdateURL);
                if (SelfUpdateManifest.NeedsUpdate)
                {
                    ApplySelfUpdate(SelfUpdateManifest);
                }
            }
        }
        private static bool AcquireLock()
        {
            PatchManager.PatchMutex = new Mutex(true, "{D1EF437C-5F7E-4B78-A71A-10489A280E61}");
            bool LaunchOK = false;

            try
            {
                LaunchOK = PatchManager.PatchMutex.WaitOne(TimeSpan.FromSeconds(2), true);
            }
            catch (AbandonedMutexException amex)
            {
                LaunchOK = true;
                Log.Debug("Received an AbandonedMutexException while WaitOne()", amex);
            }

            if (!LaunchOK)
            {
                Log.Warn("Unable to secure the Mutex, abnormal termination");
                Environment.Exit(1);
            }
            return LaunchOK;
        }
        private static Manifest SelfUpdateNeeded(string SelfUpdateURL)
        {
            string ExePath = Assembly.GetEntryAssembly().Location;
            string BootstrapPath = Path.Combine(Path.GetDirectoryName(ExePath), String.Format("_{0}", Path.GetFileName(ExePath)));
            if (File.Exists(BootstrapPath))
                File.Delete(BootstrapPath);
            Manifest SelfUpdateManifest = PatchManager.UpdateCheck(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), SelfUpdateURL);
            Log.Debug(String.Format("SelfUpdateNeeded: {0:True;0;False}", SelfUpdateManifest.NeedsUpdate));
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && SelfUpdateManifest.NeedsUpdate)
            {
                Log.Warn("Self-update needed, but wont execute as a debugger is attached to this DEBUG version");
                SelfUpdateManifest.NeedsUpdate = false;
            }
#endif
            return SelfUpdateManifest;
        }
        private static void ApplySelfUpdate(Manifest SelfUpdateManifest)
        {
            string ExePath = string.Empty;
            string BootstrapPath = string.Empty;
            if (PatchManager.DetermineBootstrapMode())
            {
                BootstrapPath = Assembly.GetEntryAssembly().Location;
                ExePath = Path.Combine(Path.GetDirectoryName(BootstrapPath), Path.GetFileName(BootstrapPath).Substring(1));

                Log.Info("BOOTSTRAP: Applying update & launching new app...");
                PatchManager.ApplyPatch(SelfUpdateManifest);
                System.Diagnostics.Process.Start(ExePath);
                Environment.Exit(3);
            }
            else
            {
                ExePath = Assembly.GetEntryAssembly().Location;
                BootstrapPath = Path.Combine(Path.GetDirectoryName(ExePath), String.Format("_{0}", Path.GetFileName(ExePath)));
                Log.Info(String.Format("Copying self ({0}) to BootstrapPath: {1}", ExePath, BootstrapPath));
                File.Copy(ExePath, BootstrapPath);
                System.Diagnostics.Process.Start(BootstrapPath);
                Environment.Exit(2);
            }
        }
        private static bool DetermineBootstrapMode()
        {
            if (Path.GetFileName(Assembly.GetEntryAssembly().Location).StartsWith("_"))
                PatchManager.BootstrapMode = true;
            Log.Info(String.Format("BootstrapMode: {0:TRUE;0;FALSE}", PatchManager.BootstrapMode));
            return PatchManager.BootstrapMode;
        }

        public static Manifest UpdateCheck(string LocalFolder, string ManifestURL)
        {
            Log.Debug(String.Format("Entering UpdateCheck(), LocalFolder: {0}, ManifestURL: {1}", LocalFolder, ManifestURL));
            bool NeedsUpdate = false;
            Manifest PatchManifest = Manifest.CreateFromXML(ManifestURL);

            Log.Debug("Iterating through each FileNode in PatchManifest.FileList...");
            foreach (FileNode PatchFile in PatchManifest.FileList)
            {
                PatchFile.LocalFilePath = Path.Combine(LocalFolder, PatchFile.RemoteFileName);
                Log.Debug(String.Format("PatchFile.LocalFilePath: {0}", PatchFile.LocalFilePath));
            }

            for (int i = 0; (i < PatchManifest.FileList.Count) && !NeedsUpdate; i++)
            {
                if (PatchManifest.FileList[i].CheckUpdate())
                {
                    Log.Info(String.Format("Found a FileNode that needs updating at {0}", PatchManifest.FileList[i].LocalFilePath));
                    NeedsUpdate = true;
                }
            }

            PatchManifest.NeedsUpdate = NeedsUpdate;
            Log.Debug(String.Format("PatchManifest.NeedsUpdate: {0:true;0;False}", PatchManifest.NeedsUpdate));
            return PatchManifest;
        }

        public static void UpdateCheckAsync(string LocalFolder, string ManifestURL)
        {
            UpdateCheckArgs args = new UpdateCheckArgs();
            args.LocalFolder = LocalFolder;
            args.ManifestURL = ManifestURL;

            UpdateCheckBW = new BackgroundWorker();
            UpdateCheckBW.DoWork += UpdateCheckBW_DoWork;
            UpdateCheckBW.RunWorkerCompleted += UpdateCheckBW_RunWorkerCompleted;
            UpdateCheckBW.RunWorkerAsync(args);
        }

        static void UpdateCheckBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (UpdateCheckCompleted != null)
                UpdateCheckCompleted(new UpdateCheckCompletedEventArgs((Manifest)e.Result));
        }

        static void UpdateCheckBW_DoWork(object sender, DoWorkEventArgs e)
        {
            string LocalFolder = ((UpdateCheckArgs)e.Argument).LocalFolder;
            string ManifestURL = ((UpdateCheckArgs)e.Argument).ManifestURL;
            e.Result = UpdateCheck(LocalFolder, ManifestURL);
        }

        public static event UpdateCheckCompletedEventHandler UpdateCheckCompleted;


        /// <summary>
        /// Creates a patch (manifest) of a folder to a patch folder
        /// </summary>
        /// <param name="LocalFolder">Folder that serves as the image to patch</param>
        /// <param name="PatchFolder">Folder where patch files should be staged for uploading to a remote server</param>
        /// <param name="ChannelName">Name of the Channel/manifest</param>
        public static void CreatePatch(string LocalFolder, string PatchFolder, string ChannelName, string DistributionURL)
        {
            List<FileNode> LocalFiles = new List<FileNode>();
            FileNode NewFile;

            foreach(string FileName in Directory.EnumerateFiles(LocalFolder, "*", SearchOption.AllDirectories))
            {
                NewFile = new FileNode();
                NewFile.LocalFilePath = FileName;
                NewFile.RemoteFileName = FileName.Remove(0, LocalFolder.Length + 1);
                NewFile.RemoteMD5Hash = NewFile.LocalMD5Hash;
                NewFile.RemoteSize = NewFile.LocalSize;

                UriBuilder RemoteURIBuilder = new UriBuilder(DistributionURL);
                RemoteURIBuilder.Path = Path.Combine(RemoteURIBuilder.Path, NewFile.RemoteMD5Hash);
                NewFile.RemoteURL = RemoteURIBuilder.Uri.ToString();
                LocalFiles.Add(NewFile);
            }

            if (!Directory.Exists(PatchFolder))
                Directory.CreateDirectory(PatchFolder);

            foreach (string FileName in Directory.EnumerateFiles(PatchFolder, "*", SearchOption.AllDirectories))
                File.Delete(FileName);
            foreach (string DirectoryName in Directory.EnumerateDirectories(PatchFolder, "*", SearchOption.AllDirectories))
                Directory.Delete(DirectoryName);

            foreach (FileNode StageFile in LocalFiles)
                StageFile.StageTo(PatchFolder);

            string ManifestFile = Path.Combine(PatchFolder, ChannelName);
            ManifestFile += ".xml";


            Manifest PatchManifest = new Manifest();
            PatchManifest.FileList = LocalFiles;
            PatchManifest.Save(ManifestFile);
        }
    }
}
