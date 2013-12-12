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

        private LauncherApp()
        {
            this.UserInfo = new UserModel();
            this.PatchInfo = new PatchModel();
            this._WPFApp = new Application();
        }

        public void StartApplication()
        {
            this._WPFApp.Run(new Views.LauncherWindow());
        }
        public void Exit()
        {
            this._WPFApp.Shutdown();
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
