using FoM.Launcher.Models;
using System;
using System.Windows;

namespace FoM.Launcher
{
    class LauncherApp
    {
        private static LauncherApp _Instance;
        private Application _WPFApp;

        public UserModel UserInfo;
        public PatchModel PatchInfo;
        public PreferenceModel PreferenceInfo;
        public Application ThisApp { get { return this._WPFApp; } }

        private LauncherApp()
        {
            this.UserInfo = new UserModel();
            this.PatchInfo = new PatchModel();
            this.PreferenceInfo = PreferenceModel.Load();
            this._WPFApp = new Application();
        }

        public void StartApplication()
        {
            this._WPFApp.ShutdownMode = ShutdownMode.OnMainWindowClose;
            this._WPFApp.MainWindow = new Views.LauncherWindow();
            this._WPFApp.Run(this._WPFApp.MainWindow);
        }
        public void Exit()
        {
            this._WPFApp.Dispatcher.Invoke(new Action(delegate() { this._WPFApp.Shutdown(); }));
        }

        public static LauncherApp Instance
        {
            get
            {
                if (LauncherApp._Instance == null)
                    LauncherApp._Instance = new LauncherApp();
                return LauncherApp._Instance;
            }
        }
    }
}
