using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Net;

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
        private long _LocalSize;
        [XmlIgnore]
        public bool NeedsUpdate;
        [XmlElement("FileName")]
        public string RemoteFileName;
        [XmlElement("MD5Hash")]
        public string RemoteMD5Hash;
        [XmlElement("URL")]
        public string RemoteURL;
        [XmlElement("Size")]
        public long RemoteSize;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        
        public string LocalMD5Hash
        {
            get
            {
                if (this._LocalMD5Hash == null)
                    this.getMD5Hash();
                return this._LocalMD5Hash;
            }
        }
        public long LocalSize
        {
            get
            {
                if (this._LocalSize == 0)
                    this.getLocalSize();
                return this._LocalSize;
            }
        }

        public void StageTo(string Folder)
        {
            string DestinationPath = Path.Combine(Folder, this.LocalMD5Hash);
            if(!File.Exists(DestinationPath))
                File.Copy(this.LocalFilePath, DestinationPath);
        }
        public bool CheckUpdate()
        {
            bool RetVal = false;

            if (!File.Exists(this.LocalFilePath))
                RetVal = true;
            if (!RetVal && (this.LocalSize != this.RemoteSize))
                RetVal = true;
            if (!RetVal && (this.LocalMD5Hash != this.RemoteMD5Hash))
                RetVal = true;


            this.NeedsUpdate = RetVal;
            return RetVal;
        }
        public void ApplyUpdate()
        {
            WebClient Downloader = new WebClient();

            if (File.Exists(this.LocalFilePath))
                File.Delete(this.LocalFilePath);    //TODO: use a temporary roll-back mechanisim
            if(!Directory.Exists(Path.GetDirectoryName(this.LocalFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(this.LocalFilePath));

            Log.Debug(String.Format("Downloading {0} to {1}", this.RemoteURL, this.LocalFilePath));
            Downloader.DownloadFile(this.RemoteURL, this.LocalFilePath);
            
            this._LocalMD5Hash = string.Empty;
            this._LocalSize = 0;
            this.NeedsUpdate = false;
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
            Log.Debug(String.Format("{0}-MD5: {1}", this.LocalFilePath, this.LocalMD5Hash));
        }
        private void getLocalSize()
        {
            FileInfo MyInfo = new FileInfo(this.LocalFilePath);
            this._LocalSize = MyInfo.Length;
        }
    }
}
