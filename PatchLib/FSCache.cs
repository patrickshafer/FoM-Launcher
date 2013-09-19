using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FoM.PatchLib
{
    public class FSCache
    {
        public List<FSNode> SaveList;
        
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
            {
                this.NodeList = new Dictionary<string, FSNode>();
                if (this.SaveList != null)
                    foreach (FSNode Item in this.SaveList)
                        if(!this.NodeList.ContainsKey(Item.FileName))
                            this.NodeList.Add(Item.FileName, Item);
            }

            if(this.NodeList.ContainsKey(FileName))
                if(this.NodeList[FileName].Expires > DateTime.UtcNow)
                    RetVal = this.NodeList[FileName];
            return RetVal;
        }
        private void FileListGC()
        {
            foreach (KeyValuePair<string, FSNode> Target in this.NodeList)
            {
                if (Target.Value.Expires < DateTime.UtcNow)
                    this.NodeList.Remove(Target.Key);
            }
        }
        internal void Save()
        {
            this.SaveList = new List<FSNode>(this.NodeList.Count);
            foreach (KeyValuePair<string, FSNode> Item in this.NodeList)
                SaveList.Add(Item.Value);

            using (StreamWriter OutputStream = new StreamWriter("FSCache.xml"))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(FSCache));
                Serializer.Serialize(OutputStream, this);
            }
        }
        internal static void Load()
        {
            if (File.Exists("FSCache.xml"))
            {
                using (StreamReader InputStream = new StreamReader(File.OpenRead("FSCache.xml")))
                {
                    XmlSerializer Serializer = new XmlSerializer(typeof(FSCache));
                    FSCache.ThisInstance = (FSCache)Serializer.Deserialize(InputStream);
                }
            }
        }
    }
    public class FSNode
    {
        public string FileName;
        public DateTime Expires;
        public DateTime CreatedDate;
        public DateTime ModifiedDate;

        public FSNode()
        {
            this.Expires = DateTime.UtcNow.AddDays(1);          //1 day for testing, will increase it as stability is proven
        }
        public FSNode(string FileName, DateTime CreatedDate, DateTime ModifiedDate) : this()
        {
            this.FileName = FileName;
            this.CreatedDate = CreatedDate;
            this.ModifiedDate = ModifiedDate;
        }

    }
}
