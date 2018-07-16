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
        //Create a list to store the result
        public List<string> EMAIL = new List<string>();
        public List<string> USERNAME = new List<string>();
        public List<string> HASH = new List<string>();
        public List<string> SALT = new List<string>();
        public List<string> ID_M = new List<string>();
        public List<string> MEMBERSHIP = new List<string>();

        public bool Signup = false;
        public bool SignupClicked = false;
        public bool CancelSingup = false;

        SolidColorBrush BrushRed = new SolidColorBrush(Colors.Red);
        Color CaretColor = (Color)ColorConverter.ConvertFromString("#FFFFC107");
        Color BorderColor = (Color)ColorConverter.ConvertFromString("#89000000");

        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void Insert()
        {
            string query = "INSERT INTO CryptoUsers (EMAIL, USERNAME, HASH, SALT) VALUES('" + EmailUp + "', '" + UserUp + "', '" + hashedpassword + "', '" + salt + "')";
            
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }

            MainWindow.CurrentID = Select(EMAIL).IndexOf(EmailUp) + 1;
        }

        public void InsertMember()
        {
            string query = "INSERT INTO CryptoMembership (ID, MEMBERSHIP) VALUES('" + Convert.ToString(MainWindow.CurrentID) + "', '" + FreeMembership + "')";
            
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            SelectMember(MEMBERSHIP);
            SettingsMembership.Text = "Membership: " + MEMBERSHIP.ElementAt(MainWindow.CurrentID - 1);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;

                    case 1130:
                        MessageBox.Show("Not allowed to connected to server.");
                        break;
                }
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
            string query = "SELECT * FROM CryptoMembership";

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

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            

            SignupClicked = true;

            if (IsValidEmail(txtSignupEmail.Text) == false || txtSignupUsername.Text == "" || txtSignupPassword.Password.Length <= 8 || txtSignupUsername.Text.Length > 20)
            {
                IncorrectInputTextBox(txtSignupEmail);
                IncorrectInputTextBox(txtSignupUsername);
                IncorrectInputPassword(txtSignupPassword);
            }
            else
            {
                ResetInputTextBox(txtSignupEmail);
                ResetInputTextBox(txtSignupUsername);
                ResetInputPassword(txtSignupPassword);
                salt = CreateSalt(10);
                hashedpassword = GenerateSHA256Hash(txtSignupPassword.Password, salt);
                EmailUp = txtSignupEmail.Text;
                UserUp = txtSignupUsername.Text;

                if (Select(EMAIL).Contains(EmailUp))
                {
                    MessageBox.Show("Email already registered.");
                }
                else if (Select(USERNAME).Contains(UserUp))
                {
                    MessageBox.Show("Username already taken.");
                }
                else
                {
                    Signup = true;
                    MainWindow.LoggedIn = true;

                    SettingsEmail.Text = "Email: " + txtSignupEmail.Text;
                    SettingsUsername.Text = txtSignupUsername.Text;
                    mw.HeaderUser.Text = txtSignupUsername.Text;

                    txtSignupEmail.Text = "";
                    txtSignupUsername.Text = "";
                    txtSignupPassword.Password = "";
                }
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
            if (SignupClicked == true && Signup == true)
            {
                if ((bool)eventArgs.Parameter == false) return;
                
                eventArgs.Cancel();
                
                eventArgs.Session.UpdateContent(new ProgressDialog());

                Insert();
                InsertMember();

                Signup = false;
                SignupClicked = false;
                
                Task.Delay(TimeSpan.FromSeconds(5))
                    .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                        TaskScheduler.FromCurrentSynchronizationContext());

                
            }
            else if (SignupClicked == true)
            {
                eventArgs.Cancel();
                SignupClicked = false;
            }
            else
            {

            }
        }

        private void AccountSettings_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsUsername.Text.Length != 0 && MainWindow.DefaultLogin == false)
            {
                EditUserUsername.Text = "Previous: " + MainWindow.PreviousUsername;
                LoginUsername.Text = MainWindow.PreviousUsername;
            }
            else if (SettingsUsername.Text.Length != 0)
            {
                EditUserUsername.Text = "Current: " + SettingsUsername.Text;
            }
            else
            {
                EditUserUsername.Text = "Current: Guest";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush CaretDefault = new SolidColorBrush(CaretColor);
            SolidColorBrush BorderDefault = new SolidColorBrush(BorderColor);

            if (LoginUsername.Text.Length > 20 && LoginPassword.Password.Length <= 8)
            {
                IncorrectInputTextBox(LoginUsername);
                IncorrectInputPassword(LoginPassword);
            }
            else if (LoginPassword.Password.Length <= 8)
            {
                ResetInputTextBox(LoginUsername);
                IncorrectInputPassword(LoginPassword);
            }
            else if (LoginUsername.Text.Length > 20)
            {
                IncorrectInputTextBox(LoginUsername);
                ResetInputPassword(LoginPassword);
            }
            else
            {
                try
                {
                    int Index = Select(USERNAME).IndexOf(LoginUsername.Text);
                    hashedpassword = Select(HASH)[Index];
                    salt = Select(SALT)[Index];

                    if (hashedpassword == GenerateSHA256Hash(LoginPassword.Password, salt))
                    {
                        MainWindow.LoggedIn = true;

                        SettingsEmail.Text = "Email: " + Select(EMAIL)[Index];
                        SettingsUsername.Text = LoginUsername.Text;
                        mw.HeaderUser.Text = LoginUsername.Text;

                        MainWindow.CurrentID = Select(EMAIL).IndexOf(Select(EMAIL)[Index]) + 1;

                        SelectMember(MEMBERSHIP);
                        SettingsMembership.Text = "Membership: " + MEMBERSHIP.ElementAt(MainWindow.CurrentID - 1);
                    }
                    else
                    {
                        IncorrectInputTextBox(LoginUsername);
                        IncorrectInputPassword(LoginPassword);
                    }
                }
                catch (Exception)
                {
                    IncorrectInputTextBox(LoginUsername);
                    IncorrectInputPassword(LoginPassword);
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LogoutUser.Text = "User: " + mw.HeaderUser.Text;
        }

        private void btnConfirmLogout_Click(object sender, RoutedEventArgs e)
        {
            mw.HeaderUser.Text = "Guest";
            SettingsUsername.Text = "Guest";
            SettingsEmail.Text = "Email: N/A";
            SettingsMembership.Text = "Membership: Free";
            SettingsDevices.Text = "Connected Devices: N/A";
            SettingsAccount.Text = "Account Balance: M/A";
            MainWindow.CurrentID = -1;
            MainWindow.LoggedIn = false;
        }
    }
}
