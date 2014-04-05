using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.IO.Compression;

namespace FoM.PatchLib
{
    [XmlType("File")]
    public class FileNode
    {
        [XmlIgnore]
        public string LocalFilePath;
        private long _LocalSize;
        private string _LocalMD5Hash;
        private string _CachedMD5Hash;
        private DateTime _LocalCreatedDate;
        private DateTime _CachedCreatedDate;
        private DateTime _LocalModifiedDate;
        private DateTime _CachedModifiedDate;
        
        [XmlIgnore]
        public bool NeedsUpdate;
        [XmlElement("FileName")]
        public string RemoteFileName;
        [XmlElement("MD5Hash")]
        public string RemoteMD5Hash;
        [XmlElement("URL")]
        public string RemoteURL;
        [XmlElement("URL-Compressed")]
        public string RemoteURLCmp;
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
                    this.populateLocalFSInfo();
                return this._LocalSize;
            }
        }

        public void StageTo(string Folder)
        {
            string DestinationPath;
            string DestinationPathCmp;
            double SpaceSavings = 0;

            DestinationPath = Path.Combine(Folder, this.LocalMD5Hash.Substring(0, 2), this.LocalMD5Hash);
            DestinationPathCmp = DestinationPath + ".cmp";

            UriBuilder RemoteURLBuilder = new UriBuilder(this.RemoteURL);
            RemoteURLBuilder.Path = this.LocalMD5Hash.Substring(0, 2) + "/" + this.LocalMD5Hash;
            this.RemoteURL = RemoteURLBuilder.Uri.ToString();

            if (!Directory.Exists(Path.GetDirectoryName(DestinationPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(DestinationPath));
            
            if(!File.Exists(DestinationPath))
                File.Copy(this.LocalFilePath, DestinationPath);

            //only compress files larger than 4k
            if (this.LocalSize > 4096)
            {
                if (!File.Exists(DestinationPathCmp))
                {
                    using (FileStream originalFile = File.OpenRead(this.LocalFilePath))
                    {
                        using (FileStream compressedFile = File.Create(DestinationPathCmp))
                        {
                            using (DeflateStream compressionStream = new DeflateStream(compressedFile, CompressionMode.Compress))
                            {
                                originalFile.CopyTo(compressionStream);
                                SpaceSavings = (double)compressedFile.Length / (double)originalFile.Length;
                            }
                        }
                    }
                    //delete files that didn't compress very well
                    if (SpaceSavings > 0.8)
                        File.Delete(DestinationPathCmp);
                    else
                        this.RemoteURLCmp = this.RemoteURL + ".cmp";
                }
            }
        }
        public bool CheckUpdate()
        {
            bool RetVal = false;
            bool NeedsMD5 = false;

            if (!File.Exists(this.LocalFilePath))
                RetVal = true;
            if (!RetVal && (this.LocalSize != this.RemoteSize))
                RetVal = true;
            if (!RetVal)
                this.populateCacheData();
            if (!RetVal && (this.RemoteMD5Hash != this._CachedMD5Hash))
                NeedsMD5 = true;
            if(!RetVal && (this._LocalCreatedDate != this._CachedCreatedDate))
                NeedsMD5 = true;
            if (!RetVal && (this._LocalModifiedDate != this._CachedModifiedDate))
                NeedsMD5 = true;
            if (!RetVal && NeedsMD5)
            {
                if (this.LocalMD5Hash != this.RemoteMD5Hash)
                    RetVal = true;
                else
                    this.setCacheData();
            }

            this.NeedsUpdate = RetVal;
            return RetVal;
        }
        public void ApplyUpdate()
        {
            if (File.Exists(this.LocalFilePath))
                File.Delete(this.LocalFilePath);    //TODO: use a temporary roll-back mechanisim
            if(!Directory.Exists(Path.GetDirectoryName(this.LocalFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(this.LocalFilePath));

            DownloadManager.AddItem(this);
            DownloadManager.ProcessQueue();
            
            this._LocalMD5Hash = string.Empty;
            this._LocalSize = 0;
            this._LocalCreatedDate = DateTime.MinValue;
            this._LocalModifiedDate = DateTime.MinValue;
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
        private void populateLocalFSInfo()
        {
            FileInfo MyInfo = new FileInfo(this.LocalFilePath);
            this._LocalSize = MyInfo.Length;
            this._LocalCreatedDate = MyInfo.CreationTimeUtc;
            this._LocalModifiedDate = MyInfo.LastWriteTimeUtc;
        }
        private void setCacheData()
        {
            FileInfo MyInfo = new FileInfo(this.LocalFilePath);
            this._CachedCreatedDate = MyInfo.CreationTimeUtc;
            this._CachedModifiedDate = MyInfo.LastWriteTimeUtc;

            FSCache.GetInstance().AddUpdate(new FSNode(this.LocalFilePath, this.LocalMD5Hash, this._CachedCreatedDate, this._CachedModifiedDate));
        }
        private void populateCacheData()
        {
            FSNode Node = FSCache.GetInstance().GetNode(this.LocalFilePath);
            if (Node != null)
            {
                this._CachedCreatedDate = Node.CreatedDate;
                this._CachedModifiedDate = Node.ModifiedDate;
                this._CachedMD5Hash = Node.Hash;
            }
        }
    }
}
