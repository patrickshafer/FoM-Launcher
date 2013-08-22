using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoM.PatchLib;

namespace FoM.Launcher
{
    public partial class InternalUI : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Timer LogTimer = new Timer();
        private log4net.Appender.MemoryAppender LogAppender;

        public InternalUI()
        {
            InitializeComponent();
        }

        private void InternalUI_Load(object sender, EventArgs e)
        {
            this.InitializeLogOutput();
        }

        private void InitializeLogOutput()
        {
            //setup l4n
            this.LogAppender = new log4net.Appender.MemoryAppender();
            this.LogAppender.Name = "InternalMemoryAppender";
            this.LogAppender.ActivateOptions();
            log4net.Repository.Hierarchy.Hierarchy LogRepository = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            LogRepository.Root.AddAppender(this.LogAppender);
            LogRepository.Configured = true;
            LogRepository.RaiseConfigurationChanged(EventArgs.Empty);

            //wire-up events
            LogTimer.Tick += LogTimer_Tick;
            LogTimer.Interval = 350;
            LogTimer.Start();
        }

        void LogTimer_Tick(object sender, EventArgs e)
        {
            log4net.Core.LoggingEvent[] LogEvents = this.LogAppender.GetEvents();
            this.LogAppender.Clear();
            StringBuilder messageBuilder = new StringBuilder(LogEvents.Length);

            if (LogEvents.Length > 0)
            {
                foreach (log4net.Core.LoggingEvent LogEvent in LogEvents)
                    messageBuilder.AppendLine(LogEvent.RenderedMessage);

                OutputMessage(messageBuilder.ToString());
            }

        }

        private void OutputMessage(string Message)
        {
            if (!Message.EndsWith("\r\n"))
                Message += "\r\n";
            OutputBox.AppendText(Message);

            if (OutputBox.Lines.Length > 1050)
                OutputBox.Lines = OutputBox.Lines.Skip(50).ToArray();


            OutputBox.SelectionStart = OutputBox.TextLength;
            OutputBox.ScrollToCaret();
        }

        private void DevUIButton_Click(object sender, EventArgs e)
        {
            DevUI newForm = new DevUI();
            newForm.ShowDialog();
        }

        private void InternalUI_Shown(object sender, EventArgs e)
        {
            string ManifestURL = "http://patch.patrickshafer.com/fom.xml";
            string LocalFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            PatchManager.UpdateCheckCompleted += PatchManager_UpdateCheckCompleted;
            PatchManager.ApplyPatchCompleted += PatchManager_ApplyPatchCompleted;

            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }

        void PatchManager_ApplyPatchCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("Patch Complete", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void PatchManager_UpdateCheckCompleted(UpdateCheckCompletedEventArgs e)
        {
            PatchManager.ApplyPatchAsync(e.Manifest);
        }
    }
}
