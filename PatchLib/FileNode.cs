using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace FoM.PatchLib
{
    [XmlType("File")]
    public class FileNode
    {
        [XmlIgnore]
        public string LocalFilePath;
        [XmlIgnore]
        private string _LocalMD5Hash;
        [XmlIgnore]
        public int LocalSize;     //TODO: Pick a better numeric data-type
        [XmlIgnore]
        public bool NeedsUpdate;
        [XmlElement("FileName")]
        public string RemoteFileName;
        [XmlElement("MD5Hash")]
        public string RemoteMD5Hash;
        [XmlElement("URL")]
        public string RemoteURL;
        [XmlElement("Size")]
        public int RemoteSize;

        
        public string LocalMD5Hash
        {
            get
            {
                if (this._LocalMD5Hash == null)
                    this.getMD5Hash();
                return this._LocalMD5Hash;
            }
        }

        public void StageTo(string Folder)
        {
            string DestinationPath = Path.Combine(Folder, this.LocalMD5Hash);
            if(!File.Exists(DestinationPath))
                File.Copy(this.LocalFilePath, DestinationPath);
        }

        private void getMD5Hash()
        {
            System.Security.Cryptography.MD5 Hasher = System.Security.Cryptography.MD5.Create();
            System.Text.StringBuilder StringBuffer = new System.Text.StringBuilder();

            FileStream ReadStream = File.OpenRead(this.LocalFilePath);
            foreach (Byte b in Hasher.ComputeHash(ReadStream))
                StringBuffer.Append(b.ToString("x2"));
            ReadStream.Close();

            this._LocalMD5Hash = StringBuffer.ToString().ToUpperInvariant();
        }
    }
}
