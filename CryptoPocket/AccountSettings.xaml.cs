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
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Threading;

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
            WalletCustomIDs = new ObservableCollection<string>();
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
        public static string SaveCustomID;
        public static string SaveWalletAddress;
        public static string ElectricitiyRate;
        //Create a list to store the result
        public List<string> ID = new List<string>();
        public List<string> EMAIL = new List<string>();
        public List<string> USERNAME = new List<string>();
        public List<string> HASH = new List<string>();
        public List<string> SALT = new List<string>();
        public List<string> ID_M = new List<string>();
        public List<string> MEMBERSHIP = new List<string>();
        public List<string> ID_MS = new List<string>();
        public List<string> CUSTOMID = new List<string>();
        public List<string> WALLET = new List<string>();
        public List<string> ID_ELEC = new List<string>();
        public List<string> ELECTRICITYRATE = new List<string>();
        public List<string> ID_CC = new List<string>();
        public List<string> C_COIN = new List<string>();
        public List<string> C_CURRENCY = new List<string>();

        public bool Signup = false;
        public bool SignupClicked = false;
        public bool CancelSingup = false;
        public bool SaveWallet = false;

        SolidColorBrush BrushRed = new SolidColorBrush(Colors.Red);
        Color CaretColor = (Color)ColorConverter.ConvertFromString("#FFFFC107");
        Color BorderColor = (Color)ColorConverter.ConvertFromString("#89000000");

        public ObservableCollection<string> WalletCustomIDs { get; set; }

        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public void InsertMiningSettings()
        {
            string query = "INSERT INTO CryptoMiningSettings (ID, CUSTOMID, WALLET) VALUES('" + Convert.ToString(MainWindow.CurrentID) + "', '" + SaveCustomID + "', '" + SaveWalletAddress + "')";

            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    WalletCustomIDs.Add(SaveCustomID);
                    ComboBoxIDs.ItemsSource = WalletCustomIDs;
                }
                catch (Exception)
                {
                    MessageBox.Show("Duplicate Custom ID Entry");
                }
            }
        }

        public void InsertElectricity()
        {
            string query = "INSERT INTO CryptoElectricity (ID, ELECTRICITY) VALUES('" + Convert.ToString(MainWindow.CurrentID) + "', '" + ElectricitiyRate + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DeleteMiningSettings()
        {
            string query = "DELETE FROM CryptoMiningSettings WHERE ID='" + Convert.ToString(MainWindow.CurrentID) + "'AND CUSTOMID='" + RemoveComboBoxIDs.Text + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
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

        //Select statement
        public List<string> Select(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoUsers";

            ID.Clear();
            EMAIL.Clear();
            USERNAME.Clear();
            HASH.Clear();
            SALT.Clear();
            
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    ID.Add(dataReader["ID"] + "");
                    EMAIL.Add(dataReader["EMAIL"] + "");
                    USERNAME.Add(dataReader["USERNAME"] + "");
                    HASH.Add(dataReader["HASH"] + "");
                    SALT.Add(dataReader["SALT"] + "");
                }
                
                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
        }
        
        public List<string> SelectMember(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoMembership WHERE ID='" + MainWindow.CurrentID + "'";

            ID_M.Clear();
            MEMBERSHIP.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_M.Add(dataReader["ID"] + "");
                    MEMBERSHIP.Add(dataReader["MEMBERSHIP"] + "");
                }

                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
        }


        public List<string> SelectMiningSettings(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoMiningSettings";

            ID_MS.Clear();
            CUSTOMID.Clear();
            WALLET.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_MS.Add(dataReader["ID"] + "");
                    CUSTOMID.Add(dataReader["CUSTOMID"] + "");
                    WALLET.Add(dataReader["WALLET"] + "");
                }

                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
        }

        public ObservableCollection<string> SelectMiningSettingsForUser(ObservableCollection<string> Ref)
        {
            string query = "SELECT * FROM CryptoMiningSettings WHERE ID='" + MainWindow.CurrentID + "'";

            WalletCustomIDs.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    WalletCustomIDs.Add(dataReader["CUSTOMID"] + "");
                }

                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
        }

        public List<string> SelectElectricity(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoElectricity WHERE ID='" + MainWindow.CurrentID + "'";

            ID_ELEC.Clear();
            ELECTRICITYRATE.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_ELEC.Add(dataReader["ID"] + "");
                    ELECTRICITYRATE.Add(dataReader["ELECTRICITY"] + "");
                }

                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
        }

        public List<string> SelectCustomCoins(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoCustomCoins WHERE ID='" + Convert.ToString(MainWindow.CurrentID) + "'";

            ID_CC.Clear();
            C_COIN.Clear();
            C_CURRENCY.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ID_CC.Add(dataReader["ID"] + "");
                    C_COIN.Add(dataReader["COIN"] + "");
                    C_CURRENCY.Add(dataReader["CURRENCY"] + "");
                }

                dataReader.Close();
                this.CloseConnection();
                return Ref;
            }
            else
            {
                return Ref;
            }
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

        private void btnSaveWallet_Click(object sender, RoutedEventArgs e)
        {
            if (CustomID.Text.Length == 0 || WalletAddress.Text.Length == 0)
            {
                IncorrectInputTextBox(CustomID);
                IncorrectInputTextBox(WalletAddress);
            }
            else if (MainWindow.LoggedIn == false)
            {
                MessageBox.Show("Guest user cannot save data!");
            }
            else
            {
                ResetInputTextBox(CustomID);
                ResetInputTextBox(WalletAddress);
                if (WalletCustomIDs.Contains(CustomID.Text))
                {
                    
                }
                else
                {
                    SaveWallet = true;
                    SaveCustomID = CustomID.Text;
                    SaveWalletAddress = WalletAddress.Text;
                }
            }
                
        }

        private void btnRemoveWallet_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveComboBoxIDs.Text.Length != 0)
            {
                DeleteMiningSettings();
                WalletCustomIDs.Remove(RemoveComboBoxIDs.Text);
                ComboBoxIDs.ItemsSource = WalletCustomIDs;
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

        void IncorrectInputTextBox(TextBox x)
        {
            x.BorderBrush = BrushRed;
            x.CaretBrush = BrushRed;

        }

        void IncorrectInputPassword(PasswordBox x)
        {
            x.BorderBrush = BrushRed;
            x.CaretBrush = BrushRed;
        }

        void ResetInputTextBox(TextBox x)
        {
            SolidColorBrush CaretDefault = new SolidColorBrush(CaretColor);
            SolidColorBrush BorderDefault = new SolidColorBrush(BorderColor);

            x.BorderBrush = BorderDefault;
            x.CaretBrush = CaretDefault;
        }

        void ResetInputPassword(PasswordBox x)
        {
            SolidColorBrush CaretDefault = new SolidColorBrush(CaretColor);
            SolidColorBrush BorderDefault = new SolidColorBrush(BorderColor);

            x.BorderBrush = BorderDefault;
            x.CaretBrush = CaretDefault;
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (SaveWallet == true)
            {
                SaveWallet = false;
                InsertMiningSettings();
            }
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CustomID.Text = "";
            WalletAddress.Text = "";
            RemoveComboBoxIDs.Text = "";
        }

        private void btnElectricityRate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MainWindow.LoggedIn == true)
            {
                ElectricitiyRate = txtElectricityRate.Text;
                UpdateElectricity();
            }
        }

        private void btnCloseSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
