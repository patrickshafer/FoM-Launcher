﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //download manifest
            //iterate through manifest to produce a file list
            //iterate through the filelist to check:
            // - file.exists
            // - hash
            // - set NeedsUpdate flag
            //iterate through FileList that NeedUpdate and:
            // - download the file
            // - apply the file
        }

        /// <summary>
        /// Creates a patch (manifest) of a folder to a patch folder
        /// </summary>
        /// <param name="LocalFolder">Folder that serves as the image to patch</param>
        /// <param name="PatchFolder">Folder where patch files should be staged for uploading to a remote server</param>
        /// <param name="ChannelName">Name of the Channel/manifest</param>
        public static void CreatePatch(string LocalFolder, string PatchFolder, string ChannelName)
        {
            //Scan LocalFolder and enumerate files
            //Capture filesize and hash for all files
            //Copy & rename files to PathFolder
            //Render manifest XML to ChannelName\manifest.xml
        }
    }
}