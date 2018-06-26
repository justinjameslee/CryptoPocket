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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CryptoPocket
{
    /// <summary>
    /// Interaction logic for AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : UserControl
    {
        public AccountSettings()
        {
            InitializeComponent();
        }
        
        public static string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public bool Theme = false;
        public bool OnTop = false;
        public bool BootUp = false;

        private void ThemeChecked(object sender, RoutedEventArgs e)
        {
            if (Theme == false)
            {
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["background"] = new SolidColorBrush(Colors.DimGray);
                Theme = true;
            }
            else if (Theme == true)
            {
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["background"] = new SolidColorBrush(Colors.WhiteSmoke);
                Theme = false;
            }
        }

        private void btnSaveWallet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveWallet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnTopChecked(object sender, RoutedEventArgs e)
        {
            if (OnTop == false)
            {
                mw.Topmost = true;
                OnTop = true;
            }
            else if (OnTop == true)
            {
                mw.Topmost = false;
                OnTop = false;
            }
        }

        private void BootChecked(object sender, RoutedEventArgs e)
        {
            if (BootUp == false)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("CryptoPocket", BaseDir);
                }
                BootUp = true;
            }
            else if (BootUp == true)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue("My ApplicationStartUpDemo", false);
                }
                BootUp = false;
            }
        }
    }
}
