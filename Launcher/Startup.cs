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
            log4net.Config.XmlConfigurator.Configure(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("FoM.Launcher.log4netConfig.xml"));

            Application.ThreadException += Application_ThreadException;

            Log.Info(String.Format("Launcher starting {0}", Application.ProductVersion));

            FoM.PatchLib.PatchManager.ApplicationStart("http://patch.patrickshafer.com/launcher.xml");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DevUI());
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.Fatal("Application_ThreadException", e.Exception);
        }
    }
}
