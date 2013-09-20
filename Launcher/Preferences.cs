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
        public string LauncherURL { get; set; }
        public bool WindowedMode { get; set; }
        public bool AutoLaunch { get; set; }

        private Preferences(string LauncherURL, bool WindowedMode, bool AutoLaunch)
        {
            this.LauncherURL = LauncherURL;
            this.WindowedMode = WindowedMode;
            this.AutoLaunch = AutoLaunch;
        }
        public Preferences() { }
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
                RetVal = new Preferences(@"http://patch.patrickshafer.com/launcher-alpha.xml", true, false);
#if !DEBUG
            //if we're not a debug version, force the live version
            RetVal.LauncherURL = @"http://patch.patrickshafer.com/launcher-alpha.xml";
#endif
            return RetVal;
        }
        internal void Save(string FileName)
        {
            using (StreamWriter OutputStream = new StreamWriter(FileName))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(Preferences));
                Serializer.Serialize(OutputStream, this);
            }
        }
    }
}
