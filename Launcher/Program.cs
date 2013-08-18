using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace FoM.Launcher
{
    static class Program
    {
        static Mutex AppRunningMutex = new Mutex(true, "{1A5BEC90-1B4F-4BF3-B214-49E0E4BF763C}");
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool OkToLaunch = false;
            try
            {
                OkToLaunch = AppRunningMutex.WaitOne(TimeSpan.FromSeconds(3), true);
            }
            catch (System.Threading.AbandonedMutexException)
            {
                OkToLaunch = true;
            }
            
            if (OkToLaunch)
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                log4net.Config.XmlConfigurator.Configure(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("FoM.Launcher.log4netConfig.xml"));
                Application.ThreadException += Application_ThreadException;
                Application.ApplicationExit += Application_ApplicationExit;
                Log.Info("FoM.Launcher starting...");

                Application.Run(new DevUI());
            }
            else
            {
                MessageBox.Show("Error starting launcher: Another instance is already running", "FoM Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            AppRunningMutex.ReleaseMutex();
            Log.Info("FoM.Launcher normal termination");
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Fatal("Application_ThreadException", e.Exception);
        }

        /// <summary>
        /// Event handler for when an assembly can't be loaded- checks for the assembly to be attached as a resource of the current executable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(args.Name);

            string path = assemblyName.Name + ".dll";
            if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
                path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);

            using (System.IO.Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null)
                    return null;
                byte[] assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }
    }
}
