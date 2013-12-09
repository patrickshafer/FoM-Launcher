using System;
using System.IO;
using FoM.PatchLib;

namespace FoM.Launcher.Models
{
    class PatchModel
    {
        private RuntimeStateEnum _PatchState;
        private int _PatchProgress;

        public event EventHandler PatchProgressChanged;
        public event EventHandler PatchStateChanged;
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
        }

        void PatchManager_UpdateCheckCompleted(UpdateCheckCompletedEventArgs e)
        {
            if (e.Manifest.NeedsUpdate)
            {
                this.PatchState = RuntimeStateEnum.ApplyUpdate;
                PatchManager.ApplyPatchAsync(e.Manifest);
            }
            else
                this.PatchState = RuntimeStateEnum.Nothing;
        }

        public void StartUpdate(string ManifestURL)
        {
#if DEBUG
            ManifestURL = @"http://patch.patrickshafer.com/fom-alpha-debug.xml";
#endif
            string LocalFolder = Directory.GetCurrentDirectory();
            this.PatchState = RuntimeStateEnum.UpdateCheck;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
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
