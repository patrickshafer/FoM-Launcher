using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace FoM.Launcher.Models
{
    public class PreferenceModel
    {
        public string LauncherURL { get; set; }
        [XmlIgnore]
        public List<string> LauncherURLList { get; set; }
        public string FoMURL { get; set; }
        [XmlIgnore]
        public List<string> FoMURLList { get; set; }
        public bool AutoLaunch { get; set; }
        public bool WindowedMode { get; set; }

        public static PreferenceModel Load()
        {
            return PreferenceModel.Load("FoM.Launcher-beta.cfg");
        }
        public static PreferenceModel Load(string FileName)
        {
            PreferenceModel RetVal = null;
            if (File.Exists(FileName))
            {
                try
                {
                    using (StreamReader InputStream = new StreamReader(File.OpenRead(FileName)))
                    {
                        XmlSerializer Serializer = new XmlSerializer(typeof(PreferenceModel));
                        RetVal = (PreferenceModel)Serializer.Deserialize(InputStream);
                    }
                }
                catch (Exception)
                {
                    File.Delete(FileName);
                    RetVal = null;
                }
            }

            if(RetVal == null)  //set defaults
                RetVal = new PreferenceModel() { LauncherURL = @"http://patch.fomportal.com:8081/launcher-beta.xml", AutoLaunch = false, WindowedMode = true };
            return RetVal;
        }
        public void Save()
        {
            this.Save("FoM.Launcher-beta.cfg");
            //delete the legacy preference file
            //if (File.Exists("Launcher.prf"))
                //File.Delete("Launcher.prf");
        }
        public void Save(string FileName)
        {
            if (this.LauncherURLList != null)
                if (string.IsNullOrEmpty(this.LauncherURLList.Find(url => url == this.LauncherURL)))
                    this.LauncherURL = this.LauncherURLList[0];

            if (this.FoMURLList != null)
                if (string.IsNullOrEmpty(this.FoMURLList.Find(url => url == this.FoMURL)))
                    this.FoMURL = this.FoMURLList[0];

            using (StreamWriter OutputStream = new StreamWriter(FileName))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(PreferenceModel));
                Serializer.Serialize(OutputStream, this);
            }
        }
    }
}
