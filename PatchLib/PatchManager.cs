﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
namespace FoM.PatchLib
{
    /// <summary>
    /// Entry-point for managing the patch process
    /// </summary>
    public static class PatchManager
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public static Manifest UpdateCheck(string LocalFolder, string ManifestURL)
        {
            Log.Debug(String.Format("Entering UpdateCheck(), LocalFolder: {0}, ManifestURL: {1}", LocalFolder, ManifestURL));
            bool NeedsUpdate = false;
            Manifest PatchManifest = Manifest.CreateFromXML(ManifestURL);

            foreach (FileNode PatchFile in PatchManifest.FileList)
            {
                Log.Debug(String.Format("Setting LocalFilePath to {0}", Path.Combine(LocalFolder, PatchFile.RemoteFileName)));
                PatchFile.LocalFilePath = Path.Combine(LocalFolder, PatchFile.RemoteFileName);
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
