using System;
using System.IO;
using FoM.PatchLib;

namespace FoM.Launcher.Models
{
    class PatchModel
    {
        private RuntimeStateEnum _PatchState;
        private int _PatchProgress;
        private volatile System.Threading.Mutex _FoMMutex;
        private System.Windows.Threading.DispatcherTimer _AutoLaunchTimer;
        private int _AutoLaunchTicker = 0;

        public event EventHandler PatchProgressChanged;
        public event EventHandler PatchStateChanged;
        public event EventHandler PatchCompleted;
        public event AutoLaunchProgressEventHandler AutoLaunchProgress;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public RuntimeStateEnum PatchState
        {
            get { return this._PatchState; }
            set
            {
                this._PatchState = value;
                if (PatchStateChanged != null)
                    PatchStateChanged(this, EventArgs.Empty);
            }
        }
        public int PatchProgress
        {
            get { return this._PatchProgress; }
            set
            {
                this._PatchProgress = value;
                if (this.PatchProgressChanged != null)
                    PatchProgressChanged(this, EventArgs.Empty);
            }
        }
        public PatchModel()
        {
            PatchManager.UpdateCheckCompleted += PatchManager_UpdateCheckCompleted;
            PatchManager.ApplyPatchCompleted += PatchManager_ApplyPatchCompleted;
            PatchManager.ApplyPatchProgressChanged += PatchManager_ApplyPatchProgressChanged;
            this.PatchState = RuntimeStateEnum.NeedsLogin;
        }

        void PatchManager_ApplyPatchProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.PatchProgress = e.ProgressPercentage;
        }

        void PatchManager_ApplyPatchCompleted(object sender, EventArgs e)
        {
            this.PatchState = RuntimeStateEnum.Ready;
            if (this.PatchCompleted != null)
                this.PatchCompleted(this, EventArgs.Empty);
        }

        void PatchManager_UpdateCheckCompleted(UpdateCheckCompletedEventArgs e)
        {
            if (e.Manifest.NeedsUpdate)
            {
                this.PatchState = RuntimeStateEnum.ApplyUpdate;
                PatchManager.ApplyPatchAsync(e.Manifest);
            }
            else
            {
                this.PatchState = RuntimeStateEnum.Ready;
                if (this.PatchCompleted != null)
                    this.PatchCompleted(this, EventArgs.Empty);
            }
        }

        public void StartUpdate(string ManifestURL)
        {
            string LocalFolder = Directory.GetCurrentDirectory();
            this.PatchState = RuntimeStateEnum.UpdateCheck;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }

        public void StartUpdate()
        {
            string LocalFolder = Directory.GetCurrentDirectory();
            string ManifestURL = LauncherApp.Instance.PreferenceInfo.FoMURL;
            this.PatchState = RuntimeStateEnum.UpdateCheck;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }

        public void LaunchFoM()
        {
            const string FoMClient = "fom_client.exe";

            if (File.Exists(FoMClient))
            {
                //string CmdLine = String.Format("-rez Resources -dpsmagic 1 +windowed {0}", LauncherApp.Instance.PreferenceInfo.WindowedMode.GetHashCode());
                string CmdLine ="-rez Resources -dpsmagic 1";
                System.Diagnostics.Process.Start("fom_client.exe", CmdLine);
                Log.Debug(String.Format("Opening fom_client.exe with \"{0}\"", CmdLine));
                LauncherApp.Instance.Exit();
            }
            else
            {
                Log.Error("Unable to launch fom_client.exe, it does not exist");
                System.Windows.MessageBox.Show("Unable to launch fom_client.exe, it does not exist", "Game launch", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        public void StartAutoLaunch()
        {
            if (LauncherApp.Instance.PreferenceInfo.AutoLaunch)
            {
                this._AutoLaunchTicker = 4;
                this._AutoLaunchTimer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Normal, LauncherApp.Instance.ThisApp.Dispatcher);
                this._AutoLaunchTimer.Interval = TimeSpan.FromSeconds(1);
                this._AutoLaunchTimer.Tick += _AutoLaunchTimer_Tick;
                this._AutoLaunchTimer.Start();
            }
        }

        void _AutoLaunchTimer_Tick(object sender, EventArgs e)
        {
            if (this._AutoLaunchTicker <= 0)
            {
                this._AutoLaunchTimer.Stop();
                this.LaunchFoM();
            }
            else
            {
                if (this.AutoLaunchProgress != null)
                    this.AutoLaunchProgress(this, new AutoLaunchProgressEventArgs() { SecondsRemaining = this._AutoLaunchTicker });
                this._AutoLaunchTicker--;
            }
        }
        public delegate void AutoLaunchProgressEventHandler(object sender, AutoLaunchProgressEventArgs e);
        public class AutoLaunchProgressEventArgs : EventArgs
        {
            public int SecondsRemaining;
        }
        public void AcquireFoMMutex()
        {
            bool LockAcquired;

            this._FoMMutex = new System.Threading.Mutex(true, "fom_client.exe", out LockAcquired);
            
            if (!LockAcquired)
            {
                Log.Error("fom_client mutex already established, terminating");
                System.Windows.MessageBox.Show("Face of Mankind is running, please close it and try again", "fom_client running", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                LauncherApp.Instance.Exit();
            }
            else
                Log.Debug("fom_client mutex acquired");
        }
        public void ReleaseFoMMutex()
        {
            if (this._FoMMutex != null)
            {
                Log.Debug("Releasing fom_client mutex...");
                this._FoMMutex.ReleaseMutex();
                this._FoMMutex.Dispose();
                this._FoMMutex = null;
            }
        }
        public enum RuntimeStateEnum
        {
            Nothing,
            NeedsLogin,
            UpdateCheck,
            ApplyUpdate,
            Ready
        }
    }
}
