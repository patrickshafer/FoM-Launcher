using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoM.Launcher.Views
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	WindowState = WindowState.Minimized;
        }

        private void DragBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const string EULA_FILE = @"Eula.rtf";
            EULAWindow EulaDialog = new EULAWindow();

            if (System.IO.File.Exists(EULA_FILE))
            {
                if (LauncherApp.Instance.PreferenceInfo.AcceptedEulaHash != getMD5Hash(EULA_FILE))
                {
                    EulaDialog.Owner = this;
                    EulaDialog.ShowDialog();
                    if (EulaDialog.DialogResult.HasValue && EulaDialog.DialogResult.Value)
                    {
                        LauncherApp.Instance.PreferenceInfo.AcceptedEulaHash = getMD5Hash(EULA_FILE);
                        LauncherApp.Instance.PreferenceInfo.Save();
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
        }
        private string getMD5Hash(string FileName)
        {
            System.Security.Cryptography.MD5 Hasher = System.Security.Cryptography.MD5.Create();
            System.Text.StringBuilder StringBuffer = new System.Text.StringBuilder();

            FileStream ReadStream = File.OpenRead(FileName);
            foreach (Byte b in Hasher.ComputeHash(ReadStream))
                StringBuffer.Append(b.ToString("x2"));
            ReadStream.Close();

            return StringBuffer.ToString().ToUpperInvariant();
        }

    }
}
