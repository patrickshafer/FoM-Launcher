using System;

namespace FoM.PatchLib
{
    public delegate void UpdateCheckCompletedEventHandler(UpdateCheckCompletedEventArgs e);
    public class UpdateCheckCompletedEventArgs : System.EventArgs
    {
        internal UpdateCheckCompletedEventArgs(Manifest Result)
        {
            if (Result == null)
                this.Canceled = true;
            else
                this.Manifest = Result;
        }
        internal UpdateCheckCompletedEventArgs(Exception Error)
        {
            this.Canceled = true;
            this.Error = Error;
        }
        public Manifest Manifest { get; private set; }
        public Exception Error { get; private set; }
        public bool Canceled;
    }
    internal struct UpdateCheckArgs
    {
        internal string LocalFolder;
        internal string ManifestURL;
    }
    
}