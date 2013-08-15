using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FoM.PatchLib
{
    public class FileNode
    {
        private string _FilePath;
        private string _MD5Hash;

        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        
        public string MD5Hash
        {
            get
            {
                if (this._MD5Hash == null)
                    this.getMD5Hash();
                return this._MD5Hash;
            }
            set { this._MD5Hash = value; }
        }

        public void StageTo(string Folder)
        {
            string DestinationPath = Path.Combine(Folder, this.MD5Hash);
            File.Copy(this.FilePath, DestinationPath, true);    //TODO: if file exists, don't overwrite
        }

        private void getMD5Hash()
        {
            System.Security.Cryptography.MD5 Hasher = System.Security.Cryptography.MD5.Create();
            System.Text.StringBuilder StringBuffer = new System.Text.StringBuilder();

            FileStream ReadStream = File.OpenRead(this.FilePath);
            foreach (Byte b in Hasher.ComputeHash(ReadStream))
                StringBuffer.Append(b.ToString("x2"));
            ReadStream.Close();

            this._MD5Hash = StringBuffer.ToString().ToUpperInvariant();
        }
    }
}
