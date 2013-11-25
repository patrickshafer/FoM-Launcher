using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using FoM.PatchLib;

namespace FoM.Launcher
{
    public partial class InternalUI : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private System.Windows.Forms.Timer LogTimer = new System.Windows.Forms.Timer();
        private log4net.Appender.MemoryAppender LogAppender;
        private PatchRunState _RunMode;
        private string UpdateURL = string.Empty;
        private Mutex FoMMutex;
        private System.Windows.Forms.Timer AutoLaunchTimer = new System.Windows.Forms.Timer();
        private int AutoLaunchTicker = 6;

        public InternalUI()
        {
            InitializeComponent();
        }

        private void InternalUI_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("{0} - {1}", Application.ProductName, Application.ProductVersion);
#if DEBUG
            this.Text += " DEBUG";
#endif
            this.InitializeLogOutput();
            this.AcquireFoMMutex();
            this.AutoLaunchTimer.Interval = 1000;
            this.AutoLaunchTimer.Tick += AutoLaunchTimer_Tick;
        }

        void AutoLaunchTimer_Tick(object sender, EventArgs e)
        {
            if (this.AutoLaunchTicker > 0)
            {
                this.AutoLaunchTicker--;
                this.StartButton.Text = String.Format("Launch ({0})", this.AutoLaunchTicker);
            }
            else
            {
                if (System.IO.File.Exists("fom_client.exe"))
                {
                    Preferences PrefData = Preferences.Load();
                    string CmdLine = String.Format("-rez Resources -dpsmagic 1 +windowed {0}", PrefData.WindowedMode.GetHashCode());
                    System.Diagnostics.Process.Start("fom_client.exe", CmdLine);
                    Application.Exit();
                }
                else
                {
                    Log.Error("Unable to launch fom_client.exe, it does not exist");
                    MessageBox.Show("Unable to launch fom_client.exe, it does not exist", "Game launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void AcquireFoMMutex()
        {
            bool LockAcquired;
            this.FoMMutex = new Mutex(true, "fom_client.exe", out LockAcquired);
            if (!LockAcquired)
            {
                Log.Error("fom_client mutex already established, terminating");
                MessageBox.Show("Face of Mankind is running, please close it and try again", "fom_client running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
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
            {
#if DEBUG
                this.UpdateURL = @"http://patch.patrickshafer.com/fom-alpha-debug.xml";
#endif
                StartUpdateCheckAsync(this.UpdateURL);
            }
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

            this.RunMode = PatchRunState.UpdateCheck;
            PatchManager.UpdateCheckAsync(LocalFolder, ManifestURL);
        }

        void PatchManager_ApplyPatchProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PatchProgress.Value = e.ProgressPercentage;
        }

        void PatchManager_ApplyPatchCompleted(object sender, EventArgs e)
        {
            if (RunMode == PatchRunState.ApplyUpdate)
                RunMode = PatchRunState.Ready;
        }

        void PatchManager_UpdateCheckCompleted(UpdateCheckCompletedEventArgs e)
        {
            if (e.Manifest.NeedsUpdate)
            {
                RunMode = PatchRunState.ApplyUpdate;
                PatchManager.ApplyPatchAsync(e.Manifest);
            }
            else
                RunMode = PatchRunState.Ready;
        }

        private PatchRunState RunMode
        {
            get { return this._RunMode; }
            set
            {
                if (this._RunMode != value)
                {
                    this._RunMode = value;
                    switch (this._RunMode)
                    {
                        case PatchRunState.Waiting:
                            PatchProgress.Value = 0;
                            StartButton.Text = "Start";
                            PatchProgress.Style = ProgressBarStyle.Continuous;
                            break;
                        case PatchRunState.UpdateCheck:
                            StartButton.Text = "Cancel";
                            PatchProgress.Style = ProgressBarStyle.Marquee;
                            break;
                        case PatchRunState.ApplyUpdate:
                            StartButton.Text = "Cancel";
                            PatchProgress.Style = ProgressBarStyle.Continuous;
                            break;
                        case PatchRunState.Ready:
                            PatchProgress.Value = 0;
                            PatchProgress.Style = ProgressBarStyle.Continuous;
                            Preferences PrefData = Preferences.Load();
                            if (PrefData.AutoLaunch)
                                this.AutoLaunchTimer.Start();
                            else
                                StartButton.Text = "Launch";
                            break;
                    }
                }
            }
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            switch (RunMode)
            {
                case PatchRunState.Waiting:
                    this.StartUpdate();
                    break;
                case PatchRunState.UpdateCheck:
                    PatchManager.UpdateCheckCancel();
                    RunMode = PatchRunState.Waiting;
                    break;
                case PatchRunState.ApplyUpdate:
                    PatchManager.ApplyPatchCancel();
                    RunMode = PatchRunState.Waiting;
                    break;
                case PatchRunState.Ready:
                    if (System.IO.File.Exists("fom_client.exe"))
                    {
                        Preferences PrefData = Preferences.Load();
                        string CmdLine = String.Format("-rez Resources -dpsmagic 1 +windowed {0}", PrefData.WindowedMode.GetHashCode());
                        System.Diagnostics.Process.Start("fom_client.exe", CmdLine);
                        Application.Exit();
                    }
                    else
                    {
                        Log.Error("Unable to launch fom_client.exe, it does not exist");
                        MessageBox.Show("Unable to launch fom_client.exe, it does not exist", "Game launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    break;
            }
        }
        enum PatchRunState
        {
            Waiting,
            UpdateCheck,
            ApplyUpdate,
            Ready
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            using (PreferencesUI PrefDialog = new PreferencesUI())
            {
                Preferences PrefData = Preferences.Load();

                PrefDialog.LauncherEdition = PrefData.LauncherEdition;
                PrefDialog.WindowedMode = PrefData.WindowedMode;
                PrefDialog.AutoLaunch = PrefData.AutoLaunch;

                if (PrefDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    PrefData.LauncherEdition = PrefDialog.LauncherEdition;
                    PrefData.WindowedMode = PrefDialog.WindowedMode;
                    PrefData.AutoLaunch = PrefDialog.AutoLaunch;
                    PrefData.Save();
                }
            }
        }
    }
}
