using System;
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
        /// <summary>
        /// Applies a patch (manifest) to a given folder/directory
        /// </summary>
        /// <param name="LocalFolder">Fully-qualified path on the local file system</param>
        /// <param name="ManifestURL">Fully-qualified URL to retrieve the manifest</param>
        public static void ApplyPatch(string LocalFolder, string ManifestURL)
        {
            Manifest PatchManifest = Manifest.CreateFromXML(ManifestURL);

            foreach (FileNode PatchFile in PatchManifest.FileList)
            {
                PatchFile.LocalFilePath = Path.Combine(LocalFolder, PatchFile.RemoteFileName);
                PatchFile.CheckUpdate();
            }

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
