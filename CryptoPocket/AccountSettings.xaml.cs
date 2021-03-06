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
using Microsoft.Win32;
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Text.RegularExpressions;

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
            string DatabaseConnectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(DatabaseConnectionString);
        }

        public static string BaseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string FileLocation = System.IO.Path.Combine(BaseDir, "CryptoPocket.exe");

        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public bool Theme = false;
        public bool OnTop = false;
        public bool BootUp = false;

        public static MySqlConnection connection;
        public static string salt;
        public static string hashedpassword;
        public static string NameUp;
        public static string UserUp;
        public static string EmailUp;
        public static string FreeMembership = "Free";
        public static string ElectricitiyRate;
        //Create a list to store the result

        public List<string> ID_ELEC = new List<string>();
        public List<string> ELECTRICITYRATE = new List<string>();

        public bool Signup = false;
        public bool SignupClicked = false;
        public bool CancelSingup = false;
        public bool SaveWallet = false;

        SolidColorBrush BrushRed = new SolidColorBrush(Colors.Red);
        Color CaretColor = (Color)ColorConverter.ConvertFromString("#FFFFC107");
        Color BorderColor = (Color)ColorConverter.ConvertFromString("#89000000");

        private static readonly Regex NumericalInput = new Regex("[^0-9.]+");


        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        //Update statement
        public void UpdateElectricity()
        {
            string query = "UPDATE CryptoElectricity SET ELECTRICITY='" + ElectricitiyRate + "' WHERE ID='" + Convert.ToString(MainWindow.CurrentID) + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                mw.ServerError.IsOpen = true;

                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }



        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        private void ThemeChecked(object sender, RoutedEventArgs e)
        {
            if (Theme == false)
            {
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["txtBlack"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["bgColor"] = new SolidColorBrush(Colors.Gray);
                Application.Current.Resources["background"] = new SolidColorBrush(Colors.DimGray);
                Theme = true;
            }
            else if (Theme == true)
            {
                Application.Current.Resources["txtColor"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["txtBlack"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["bgColor"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["background"] = new SolidColorBrush(Colors.WhiteSmoke);
                Theme = false;
            }
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
                    key.SetValue("CryptoPocket", FileLocation);
                }
                BootUp = true;
            }
            else if (BootUp == true)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue("CryptoPocket", false);
                }
                BootUp = false;
            }
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

        }

        private void AccountSettings_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            mw.SignUp.IsOpen = true;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LoggedIn == false)
            {
                if (SettingsUsername.Text.Length != 0 && MainWindow.DefaultLogin == false)
                {
                    mw.EditUserUsername.Text = "Previous: " + MainWindow.PreviousUsername;
                    mw.LoginUsername.Text = MainWindow.PreviousUsername;
                }
                else if (SettingsUsername.Text.Length != 0)
                {
                    mw.EditUserUsername.Text = "Current: " + SettingsUsername.Text;
                }
                else
                {
                    mw.EditUserUsername.Text = "Current: Guest";
                }

                mw.Login.IsOpen = true;
            }
            else if (MainWindow.LoggedIn == true)
            {
                mw.LogoutUser.Text = "User: " + mw.HeaderUser.Text;

                mw.Logout.IsOpen = true;
            }
        }

        private void btnElectricityRate_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtElectricityRate.Text) >= 1)
                {
                    if(MainWindow.LoggedIn == true)
                    {
                        txtElectricityRate.Text = ElectricitiyRate;
                    }
                    else
                    {
                        txtElectricityRate.Text = "0.10";
                        ElectricitiyRate = "0.10";
                    }
                }
                else if (Convert.ToDecimal(txtElectricityRate.Text) < 1 && MainWindow.LoggedIn == true)
                {
                    UpdateElectricity();
                }
            }
            catch (Exception)
            {
                if (MainWindow.LoggedIn == true)
                {
                    txtElectricityRate.Text = ElectricitiyRate;
                }
                else
                {
                    txtElectricityRate.Text = "0.10";
                    ElectricitiyRate = "0.10";
                }
            }
        }

        private void btnCloseSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnManageWallet_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;
            if (Button.Name == "btnSaveWalletID")
            {
                mw.SaveWalletID.IsOpen = true;
            }
            else if (Button.Name == "btnRemoveWalletID")
            {
                mw.RemoveWalletID.IsOpen = true;
            }
        }

        private void Electricity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !NumericalInput.IsMatch(text);
        }
    }
}
