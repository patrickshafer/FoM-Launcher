using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoM.PatchLib
{
    /// <summary>
    /// Entry-point for managing the patch process
    /// </summary>
    static class PatchManager
    {
        /// <summary>
        /// Applies a patch (manifest) to a given folder/directory
        /// </summary>
        /// <param name="LocalFolder">Fully-qualified path on the local file system</param>
        /// <param name="ManifestURL">Fully-qualified URL to retrieve the manifest</param>
        static void ApplyPatch(string LocalFolder, string ManifestURL)
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
    }
}
