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
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        public PreferencesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    public class ListToDictionaryURLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<string> SourceList;
            Dictionary<string, string> RetVal = new Dictionary<string, string>();
            string ChannelName = null;
            Uri SourceUri;

            if (value is List<string>)
            {
                SourceList = (List<string>)value;
                foreach (string url in SourceList)
                {
                    if (Uri.TryCreate(url, UriKind.Absolute, out SourceUri))
                    {
                        ChannelName = SourceUri.AbsolutePath;
                        ChannelName = System.IO.Path.GetFileNameWithoutExtension(ChannelName);
                        RetVal.Add(ChannelName, url);
                    }
                }
            }
            return RetVal.Count > 0 ? RetVal : DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
