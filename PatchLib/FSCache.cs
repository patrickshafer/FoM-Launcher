using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FoM.PatchLib
{
    [Serializable]
    public class FSCache
    {
        private Dictionary<string, FSNode> NodeList;
        private static FSCache ThisInstance;

        internal static FSCache GetInstance()
        {
            if (FSCache.ThisInstance == null)
                FSCache.ThisInstance = new FSCache();
            return FSCache.ThisInstance;
        }

        internal void AddUpdate(FSNode NewNode)
        {
            if (this.NodeList == null)
                this.NodeList = new Dictionary<string, FSNode>();
            this.NodeList[NewNode.FileName] = NewNode;
        }


        internal FSNode GetNode(string FileName)
        {
            FSNode RetVal = null;
            if (this.NodeList == null)
                this.NodeList = new Dictionary<string, FSNode>();

            if(this.NodeList.ContainsKey(FileName))
                if(this.NodeList[FileName].Expires > DateTime.UtcNow)
                    RetVal = this.NodeList[FileName];
            return RetVal;
        }
        private void FileListGC()
        {
            if (this.NodeList != null)
                foreach (KeyValuePair<string, FSNode> Target in this.NodeList)
                    if (Target.Value.Expires < DateTime.UtcNow)
                        this.NodeList.Remove(Target.Key);
        }
        internal void Save()
        {
            if (new Random().Next(0, 100) <= 2)             //2% chance to garbage collect
                this.FileListGC();
            using (Stream OutputStream = new FileStream("FSCache.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(OutputStream, this);
            }
        }
        internal static void Load()
        {
            if (File.Exists("FSCache.bin"))
            {
                try
                {
                    using (Stream InputStream = new FileStream("FSCache.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        FSCache.ThisInstance = (FSCache)formatter.Deserialize(InputStream);
                    }
                }
                catch (Exception)
                {
                    File.Delete("FSCache.bin");
                }
                
            }
        }
    }
    [Serializable]
    public class FSNode
    {
        public string FileName;
        public string Hash;
        public DateTime Expires;
        public DateTime CreatedDate;
        public DateTime ModifiedDate;

        public FSNode()
        {
            this.Expires = DateTime.UtcNow.AddDays(new Random().Next(20, 30));          //20 - 30 days for forced recheck
        }

        public FSNode(string FileName, string Hash, DateTime CreatedDate, DateTime ModifiedDate) : this()
        {
            this.FileName = FileName;
            this.Hash = Hash;
            this.CreatedDate = CreatedDate;
            this.ModifiedDate = ModifiedDate;
        }

    }
}
