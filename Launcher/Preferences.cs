using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace FoM.Launcher
{
    public class Preferences
    {
        [XmlIgnore]
        public string LauncherURL
        {
            get
            {
                switch (this.LauncherEdition)
                {
                    case LauncherEditionEnum.Development:
                        return @"http://patch.patrickshafer.com/launcher-beta-debug.xml";
                    case LauncherEditionEnum.Live:
                    default:
                        return @"http://patch.fomportal.com:8081/launcher-beta.xml";
                }
            }
        }

        public LauncherEditionEnum LauncherEdition { get; set; }
        public bool WindowedMode { get; set; }
        public bool AutoLaunch { get; set; }

        private Preferences(LauncherEditionEnum LauncherEdition, bool WindowedMode, bool AutoLaunch)
        {
            this.LauncherEdition = LauncherEdition;
            this.WindowedMode = WindowedMode;
            this.AutoLaunch = AutoLaunch;
        }
        public Preferences() { }
        internal static Preferences Load()
        {
            return Preferences.Load("Launcher.prf");
        }
        internal static Preferences Load(string FileName)
        {
            Preferences RetVal;
            if (File.Exists(FileName))
            {
                using (StreamReader InputStream = new StreamReader(File.OpenRead(FileName)))
                {
                    XmlSerializer Serializer = new XmlSerializer(typeof(Preferences));
                    RetVal = (Preferences)Serializer.Deserialize(InputStream);
                }
            }
            else
#if DEBUG
                RetVal = new Preferences(LauncherEditionEnum.Development, true, true);
#else
                RetVal = new Preferences(LauncherEditionEnum.Live, true, true);
#endif
            return RetVal;
        }
        internal void Save()
        {
            this.Save("Launcher.prf");
        }
        internal void Save(string FileName)
        {
            using (StreamWriter OutputStream = new StreamWriter(FileName))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(Preferences));
                Serializer.Serialize(OutputStream, this);
            }
        }
        public enum LauncherEditionEnum
        {
            Live,
            Development
        }
    }
}
