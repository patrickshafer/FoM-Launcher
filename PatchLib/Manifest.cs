using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace FoM.PatchLib
{
    public class Manifest
    {
        public List<FileNode> FileList;


        public void Save(string FileName)
        {
            using (StreamWriter OutputStream = new StreamWriter(FileName))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(Manifest));
                Serializer.Serialize(OutputStream, this);
            }
        }
        public static Manifest CreateFromXML(string ManifestURL)
        {
            using (WebClient HttpRequest = new WebClient())
            {
                using(StreamReader InputStream = new StreamReader(HttpRequest.OpenRead(ManifestURL)))
                {
                    XmlSerializer Serializer = new XmlSerializer(typeof(Manifest));
                    return (Manifest)Serializer.Deserialize(InputStream);
                }
            }
        }
    }
}
