using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace FoM.Launcher.Views
{
    /// <summary>
    /// Interaction logic for EULAWindow.xaml
    /// </summary>
    public partial class EULAWindow : Window
    {
        public EULAWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            const string EULA_FILE = @"Eula.rtf";

            if (File.Exists(EULA_FILE))
            {
                using (FileStream InputStream = new FileStream(EULA_FILE, FileMode.Open))
                {
                    this.EULARTF.Selection.Load(InputStream, DataFormats.Rtf);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
