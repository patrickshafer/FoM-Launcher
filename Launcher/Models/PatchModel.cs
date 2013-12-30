﻿using System;
using System.IO;
using FoM.PatchLib;

namespace FoM.Launcher.Models
{
    class PatchModel
    {
        private RuntimeStateEnum _PatchState;
        private int _PatchProgress;
        private System.Threading.Mutex FoMMutex;

        public event EventHandler PatchProgressChanged;
        public event EventHandler PatchStateChanged;
        public event EventHandler PatchCompleted;

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
            this.AcquireFoMMutex();
            string LocalFolder = Directory.GetCurrentDirectory();
            this.PatchState = RuntimeStateEnum.UpdateCheck;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }
        public void LaunchFoM()
        {
            this.ReleaseFoMMutex();
            const string FoMClient = "fom_client.exe";

            if (File.Exists(FoMClient))
            {
                Preferences PrefData = Preferences.Load();
                string CmdLine = String.Format("-rez Resources -dpsmagic 1 +windowed {0}", PrefData.WindowedMode.GetHashCode());
                System.Diagnostics.Process.Start("fom_client.exe", CmdLine);
                LauncherApp.Instance.Exit();
            }
            else
            {
                Log.Error("Unable to launch fom_client.exe, it does not exist");
                System.Windows.MessageBox.Show("Unable to launch fom_client.exe, it does not exist", "Game launch", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        private void AcquireFoMMutex()
        {
            bool LockAcquired;
            this.FoMMutex = new System.Threading.Mutex(true, "fom_client.exe", out LockAcquired);
            if (!LockAcquired)
            {
                Log.Error("fom_client mutex already established, terminating");
                System.Windows.MessageBox.Show("Face of Mankind is running, please close it and try again", "fom_client running", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                LauncherApp.Instance.Exit();
            }
        }
        private void ReleaseFoMMutex()
        {
            if (this.FoMMutex != null)
            {
                this.FoMMutex.ReleaseMutex();
                this.FoMMutex = null;
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