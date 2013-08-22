
namespace FoM.PatchLib
{
    public delegate void UpdateCheckCompletedEventHandler(UpdateCheckCompletedEventArgs e);
    public class UpdateCheckCompletedEventArgs : System.EventArgs
    {
        internal UpdateCheckCompletedEventArgs(Manifest Result)
        {
            this.Manifest = Result;
        }
        public Manifest Manifest { get; private set; }
    }
    internal struct UpdateCheckArgs
    {
        internal string LocalFolder;
        internal string ManifestURL;
    }
}