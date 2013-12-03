using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FoM.Launcher.Views;

namespace FoM.Launcher
{
    internal class StartupWPF : IStartup
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Application _Application;

        public void StartApplication()
        {
            log4net.Config.XmlConfigurator.Configure(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("FoM.Launcher.Resources.log4netConfig.xml"));

            this._Application = new Application();


            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Application.ThreadException += Application_ThreadException;

            //Log.Info(String.Format("Launcher starting {0}", Application.ProductVersion));

            Preferences PrefData = Preferences.Load();
            FoM.PatchLib.PatchManager.ApplicationStart(PrefData.LauncherURL);

            this._Application.Run(new LauncherWindow());

            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new InternalUI());

            
            
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.Fatal("ThreadException in current Application", e.Exception);
            //MessageBox.Show("An application error occured, please see the log file for details", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Application.ExitThread();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("UnhandledException in current AppDomain", (Exception)e.ExceptionObject);
            //MessageBox.Show("An application error occured, please see the log file for details", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Application.ExitThread();
        }
    }
}
