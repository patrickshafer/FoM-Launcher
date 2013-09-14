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
        private PatchRunMode RunMode;
        private string UpdateURL = string.Empty;

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

        private void InternalUI_Shown(object sender, EventArgs e)
        {
            PatchManager.UpdateCheckCompleted += PatchManager_UpdateCheckCompleted;
            PatchManager.ApplyPatchCompleted += PatchManager_ApplyPatchCompleted;
            PatchManager.ApplyPatchProgressChanged += PatchManager_ApplyPatchProgressChanged;

            this.StartUpdate();
        }

        private void StartUpdate()
        {
            if(this.UpdateURL == string.Empty)
                this.UpdateURL = GetLogin();
            if (this.UpdateURL != string.Empty)
                StartUpdateCheckAsync(this.UpdateURL);
        }

        private string GetLogin()
        {
            AuthenticationRPC.AuthenticateResult LoginResult = new AuthenticationRPC.AuthenticateResult();
            DialogResult WindowResult = new DialogResult();
            string UpdateURL = string.Empty;
            using (Login LoginDialog = new Login())
            {
                do
                {
                    WindowResult = LoginDialog.ShowDialog(this);
                    if (WindowResult == System.Windows.Forms.DialogResult.OK)
                    {
                        LoginResult = AuthenticationRPC.Login(LoginDialog.Username, LoginDialog.Password);
                        if (LoginResult.Status == RPCEnvelope.StatusEnum.Error)
                            MessageBox.Show(LoginResult.ErrorMessage, "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                            UpdateURL = LoginResult.UpdateURL;
                    }

                } while (WindowResult == System.Windows.Forms.DialogResult.OK && LoginResult.Status == RPCEnvelope.StatusEnum.Error);
            }

            return UpdateURL;
        }

        private void StartUpdateCheckAsync(string ManifestURL)
        {
            string LocalFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            StartButton.Text = "Cancel";
            RunMode = PatchRunMode.UpdateCheck;
            PatchProgress.Style = ProgressBarStyle.Marquee;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }

        void PatchManager_ApplyPatchProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PatchProgress.Value = e.ProgressPercentage;
        }

        void PatchManager_ApplyPatchCompleted(object sender, EventArgs e)
        {
            PatchProgress.Value = 0;
            StartButton.Text = "Start";
            RunMode = PatchRunMode.Ready;
        }

        void PatchManager_UpdateCheckCompleted(UpdateCheckCompletedEventArgs e)
        {
            PatchProgress.Style = ProgressBarStyle.Continuous;
            if (e.Manifest.NeedsUpdate)
            {
                RunMode = PatchRunMode.ApplyUpdate;
                PatchManager.ApplyPatchAsync(e.Manifest);
            }
            else
            {
                RunMode = PatchRunMode.Ready;
                StartButton.Text = "Start";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            switch (RunMode)
            {
                case PatchRunMode.None:
                    this.StartUpdate();
                    break;
                case PatchRunMode.UpdateCheck:
                    PatchManager.UpdateCheckCancel();
                    StartButton.Text = "Start";
                    PatchProgress.Style = ProgressBarStyle.Continuous;
                    RunMode = PatchRunMode.None;
                    break;
                case PatchRunMode.ApplyUpdate:
                    PatchManager.ApplyPatchCancel();
                    PatchProgress.Value = 0;
                    StartButton.Text = "Start";
                    RunMode = PatchRunMode.None;
                    break;
                case PatchRunMode.Ready:
                    if (System.IO.File.Exists("fom_client.exe"))
                        System.Diagnostics.Process.Start("fom_client.exe", "-rez Resources -dpsmagic 1 +windowed 1");
                    else
                    {
                        Log.Error("Unable to launch fom_client.exe, it does not exist");
                        MessageBox.Show("Unable to launch fom_client.exe, it does not exist", "Game launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //-rez Resources -dpsmagic 1 +windowed 1
                    //may need a checkbox for windowed mode
                    break;
                default:
                    break;
            }
        }
        enum PatchRunMode
        {
            None,
            UpdateCheck,
            ApplyUpdate,
            Ready
        }
    }
}
