using System;
using System.Collections.Generic;
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
            LauncherApp.Instance.PatchInfo.StartUpdate(@"http://da73s87zjmd54.cloudfront.net/fom-preinstall.xml");
        }

    }
}
