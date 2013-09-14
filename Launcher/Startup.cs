using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoM.Launcher
{
    internal class Startup
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal void Execute()
        {
            log4net.Config.XmlConfigurator.Configure(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("FoM.Launcher.Resources.log4netConfig.xml"));

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Log.Info(String.Format("Launcher starting {0}", Application.ProductVersion));

            FoM.PatchLib.PatchManager.ApplicationStart("http://patch.patrickshafer.com/launcher-alpha.xml");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InternalUI());
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal("UnhandledException in current AppDomain", (Exception)e.ExceptionObject);
        }

    }
}
