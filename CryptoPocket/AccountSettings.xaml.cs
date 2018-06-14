﻿using System;
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

        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public bool Theme = false;

        private void ThemeChecked(object sender, RoutedEventArgs e)
        {
            if (Theme == false)
            {
                this.Background = new SolidColorBrush(Colors.Gray);
                mw.Background = new SolidColorBrush(Colors.Gray);
                mw.ThemeChange01.Foreground = new SolidColorBrush(Colors.White);
                mw.ThemeChange02.Foreground = new SolidColorBrush(Colors.White);
                mw.ThemeChange03.Foreground = new SolidColorBrush(Colors.White);
                mw.ThemeChange04.Foreground = new SolidColorBrush(Colors.White);
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.White);
                Theme = true;
            }
            else if (Theme == true)
            {
                this.Background = new SolidColorBrush(Colors.WhiteSmoke);
                mw.Background = new SolidColorBrush(Colors.WhiteSmoke);
                mw.ThemeChange01.Foreground = new SolidColorBrush(Colors.Black);
                mw.ThemeChange02.Foreground = new SolidColorBrush(Colors.Black);
                mw.ThemeChange03.Foreground = new SolidColorBrush(Colors.White);
                mw.ThemeChange04.Foreground = new SolidColorBrush(Colors.White);
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.Black);
                Theme = false;
            }
        }
    }
}
