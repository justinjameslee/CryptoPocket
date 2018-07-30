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
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;
using System.IO;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using System.Net;
using System.Text.RegularExpressions;
using QuickType;
using Newtonsoft.Json.Linq;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;

namespace CryptoPocket
{

    public partial class MainWindow : Window
    {

        public Func<ChartPoint, string> PointLabel { get; set; }

        public class CustomCoinBox
        {
            public string CustomCoinName { get; set; }
            public string CustomCoinFontSize { get; set; }
            public string ValueUSD { get; set; }
            public string ValueBTC { get; set; }
            public string PortfolioQuan { get; set; }
            public string PortfolioValue { get; set; }
            public string Change24Hour { get; set; }
            public string Change7Day { get; set; }
            public string OverallTrend { get; set; }
            public string Trend24 { get; set; }
            public string Trend7 { get; set; }
            public int Currency { get; set; }
        }

        public class CustomWorkerBox
        {
            public string CustomWorkerName { get; set; }
            public string MiningWorker { get; set; }
            public string ProfitabilityDay { get; set; }
            public string Hashrate { get; set; }
            public string Uptime { get; set; }
            public string Status { get; set; }
            public Brush StatusColour { get; set; }
            public string LastUpdated { get; set; }
        }

        MainWindow mw = (MainWindow)Application.Current.MainWindow;

        MySqlConnection connection;
        bool CoinToggled = false;
        bool WorkerToggled = false;
        public bool AlwaysOnTop = false;

        public static bool LoggedIn = false;
        public static bool DefaultLogin = true;
        public static string PreviousUsername;

        public static int CurrentID = 0;

        Color HoverColour = (Color)ColorConverter.ConvertFromString("#FFFFCC33");
        Color UnhoverColour = (Color)ColorConverter.ConvertFromString("#FFFEC007");

        public static string initialCoinList;
        public static string relevantCoinList;
        public static string[] CoinListA;
        public static List<string> SepCoinList = new List<string>();

        public static string initialCoinN;
        public static string relevantCoinN;
        public static string CoinN;
        public static List<string> SepCoinN = new List<string>();
        public static string relevantCoinID;
        public static List<string> SepCoinID = new List<string>();
        public static List<string> SessionCustomCoins = new List<string>();
        public static List<string> SessionCustomCoinsCurrency = new List<string>();

        public static MainData[] CoinData;

        public static string initialCustomCoin;
        public static string relevantCustomCoin;
        public static string[] CustomCoinA;
        public static string[] RegexSplitChange7;
        public static List<string> SepCustomCoinA = new List<string>();

        public static string initialCoinB;
        public static string CustomPrice;
        public static string CustomChange24;
        public static string CustomChange7;
        public static List<string> CustomCoinPrice = new List<string>();
        public static List<string> CustomCoin24 = new List<string>();
        public static List<string> CustomCoin7 = new List<string>();
        public static string Change24;
        public static string Change7;
        public static string ChangeCurrency;
        public static string CustomCoin;
        public static string CustomWorker;

        public static string initialWorkerInfo;
        public static string relevantWorkerInfo;
        public static string[] WorkerInfoA;
        public static List<string> SepWorkerInfoA = new List<string>();
        public static string initialWorker;
        public static string[] RegexSplitWorkerInfo;
        public static string sessWorkerName;

        public static bool WalletSelected = false;

        public static bool Incorrect;
        public static string Index;
        public static bool UpdatingCustomCoin;
        public static bool UpdatingCustomWorker;

        public static string salt;
        public static string hashedpassword;
        public static string NameUp;
        public static string UserUp;
        public static string EmailUp;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static bool SigningUp = false;
        public static bool LoggingIn = false;
        public static bool SavingWalletID = false;
        public static bool RemovingWalletID = false;
        public static bool AddWorkerBool = false;
        public static bool RemoveWorkerBool = false;

        public static string strLoginEmail;
        public static string strLoginUsername;
        public static string strLoginPassword;

        public static string SignupEmail;
        public static string SingupUsername;
        public static string SignUpPassword;

        public static string SaveCustomID;
        public static string SaveWalletAddress;
        public static string RemoveCustomID;

        public static string strAddWorkerNameID;
        public static string strAddWorkerPool;
        public static string strAddWorkerWalletID;
        public static string strAddWorkerWalletAddress;
        public static string strAddWorkerElectricity;
        public static string strAddWorkerName;
        public static string strRemoveWorkerName;

        public bool SettingsActive;

        public List<string> ID = new List<string>();
        public List<string> EMAIL = new List<string>();
        public List<string> USERNAME = new List<string>();
        public List<string> HASH = new List<string>();
        public List<string> SALT = new List<string>();
        public List<string> ID_MS = new List<string>();
        public List<string> CUSTOMID = new List<string>();
        public List<string> WALLET = new List<string>();
        public List<string> ID_M = new List<string>();
        public List<string> MEMBERSHIP = new List<string>();
        public List<string> ID_CC = new List<string>();
        public List<string> C_COIN = new List<string>();
        public List<string> C_CURRENCY = new List<string>();

        public List<string> WORKERNAME = new List<string>();
        public List<string> POWER = new List<string>();
        public List<string> POOL = new List<string>();

        private static readonly Regex NumericalInput = new Regex("[^0-9]+");

        public ObservableCollection<string> WalletCustomIDs { get; set; }
        public ObservableCollection<string> WalletCustomAddresses { get; set; }
        public ObservableCollection<string> WorkerList { get; set; }
        public ObservableCollection<string> WorkerCustomList { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
            HeaderTabs.SelectedIndex = 0;

            //string connectionstring2 = "SERVER=27.121.66.21;DATABASE=goldli00_CryptoPocket;UID=goldli00_admin;PWD=passCrypto123;";

            string DatabaseConnectionString = Properties.Settings.Default.ConnectionStringLocal;
            connection = new MySqlConnection(DatabaseConnectionString);

            WalletCustomIDs = new ObservableCollection<string>();
            WalletCustomAddresses = new ObservableCollection<string>();
            WorkerList = new ObservableCollection<string>();
            WorkerCustomList = new ObservableCollection<string>();

        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void DeleteCustomCoins()
        {
            string query = "DELETE FROM CryptoCustomCoins WHERE ID='" + CurrentID + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DeleteCustomWorkers()
        {
            string query = "DELETE FROM CryptoWorkers WHERE ID='" + CurrentID + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void InsertCustomCoins(string Coin, string Currency)
        {
            string query = "INSERT INTO CryptoCustomCoins (ID, COIN, CURRENCY) VALUES('" + CurrentID + "', '" + Coin + "', '" + Currency + "')";

            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Saving Data, Data Lost.");
                }
            }
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }



        public void CustomCoinCalculation()
        {
            try
            {
                if (UpdatingCustomCoin == true)
                {
                    Index = Convert.ToString(SepCoinID[ComboEditCoinName.SelectedIndex]);
                    SessionCustomCoins.Remove(ComboEditCoinName.Text);
                    SessionCustomCoinsCurrency.Remove(Convert.ToString(((CustomCoinBox)CoinBox.SelectedItem).Currency));
                    ChangeCurrency = ComboEditCoinPercentage.Text;
                    CustomCoin = ComboEditCoinName.Text;
                }
                else
                {
                    Index = Convert.ToString(SepCoinID[CoinComboBox.SelectedIndex]);
                    ChangeCurrency = ComboCoinPercentage.Text;
                    CustomCoin = CoinComboBox.Text;
                }


                CoinToggled = false;

                CustomCoinPrice.Clear();
                CustomCoin24.Clear();
                CustomCoin7.Clear();

                string url = @"https://api.coinmarketcap.com/v2/ticker/" + Index + "/?convert=BTC";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    initialCustomCoin = reader.ReadToEnd();
                    relevantCustomCoin = References.EaseMethods.getBetween(initialCustomCoin, "\"USD\": {", "\"last_updated\"");
                    CustomCoinA = Regex.Split(relevantCustomCoin, "},");
                    SepCustomCoinA = CustomCoinA.OfType<string>().ToList();
                    for (int x = 0; x < SepCustomCoinA.Count; x++)
                    {
                        initialCoinB = SepCustomCoinA[x];
                        CustomPrice = References.EaseMethods.getBetween(initialCoinB, "price\": ", ",");
                        CustomChange24 = References.EaseMethods.getBetween(initialCoinB, "percent_change_24h\": ", ",");
                        CustomChange7 = EaseMethods.RemoveKeepingColumnAndDots(initialCoinB);
                        RegexSplitChange7 = Regex.Split(CustomChange7, ":");
                        CustomChange7 = RegexSplitChange7.Last();
                        CustomCoinPrice.Add(CustomPrice);
                        CustomCoin24.Add(CustomChange24);
                        CustomCoin7.Add(CustomChange7);
                    }
                }


                string FontSize;
                decimal rounding;

                if (CustomCoin.Length == 5) { FontSize = "14"; }
                else if (CustomCoin.Length == 6) { FontSize = "12"; }
                else if (CustomCoin.Length >= 7) { FontSize = "10"; }
                else { FontSize = "16"; }

                if (CustomCoinPrice[0].Contains("e")) { rounding = Decimal.Parse(CustomCoinPrice[0], System.Globalization.NumberStyles.Float); }
                else { rounding = Convert.ToDecimal(CustomCoinPrice[0]); }
                rounding = Math.Round(rounding, 2);
                CustomCoinPrice[0] = Convert.ToString(rounding);

                if (CustomCoinPrice[1].Contains("e")) { rounding = Decimal.Parse(CustomCoinPrice[1], System.Globalization.NumberStyles.Float); }
                else { rounding = Convert.ToDecimal(CustomCoinPrice[1]); }
                rounding = Math.Round(rounding, 8);
                CustomCoinPrice[1] = Convert.ToString(rounding);

                CustomCoinPrice[0] = CustomCoin + "/USD: $" + CustomCoinPrice[0];
                CustomCoinPrice[1] = CustomCoin + "/BTC: " + CustomCoinPrice[1];

                int Currency = 0;

                if (ChangeCurrency == "USD")
                {
                    Currency = 0;
                    ChangeCurrencyMethod(Currency);
                }
                else if (ChangeCurrency == "BTC")
                {
                    Currency = 1;
                    ChangeCurrencyMethod(Currency);
                }

                if (UpdatingCustomCoin == true)
                {
                    int index = CoinBox.SelectedIndex;
                    CoinBox.Items.RemoveAt(index);
                    CoinBox.Items.Insert(index, new CustomCoinBox() { CustomCoinName = CustomCoin, CustomCoinFontSize = FontSize, ValueUSD = CustomCoinPrice[0], ValueBTC = CustomCoinPrice[1], PortfolioQuan = "N/A", PortfolioValue = "N/A", Change24Hour = CustomCoin24[Currency], Change7Day = CustomCoin7[Currency], OverallTrend = Change7, Trend24 = Change24, Trend7 = Change7, Currency = Currency });
                    SessionCustomCoins.Insert(index, CustomCoin);
                    SessionCustomCoinsCurrency.Insert(index, Convert.ToString(Currency));
                }
                else
                {
                    CoinBox.Items.Add(new CustomCoinBox() { CustomCoinName = CustomCoin, CustomCoinFontSize = FontSize, ValueUSD = CustomCoinPrice[0], ValueBTC = CustomCoinPrice[1], PortfolioQuan = "N/A", PortfolioValue = "N/A", Change24Hour = CustomCoin24[Currency], Change7Day = CustomCoin7[Currency], OverallTrend = Change7, Trend24 = Change24, Trend7 = Change7, Currency = Currency });
                    SessionCustomCoins.Add(CustomCoin);
                    SessionCustomCoinsCurrency.Add(Convert.ToString(Currency));
                }

                CoinComboBox.Text = "";
                ComboCoinPercentage.Text = "";

            }
            catch (Exception)
            {
                CoinComboBox.Text = "";
                ComboCoinPercentage.Text = "";
                Incorrect = true;
            }
        }

        public void CustomWorkerCalculation()
        {
            try
            {
                if (UpdatingCustomWorker == true)
                {
                    SessionCustomCoins.Remove(ComboEditCoinName.Text);
                    SessionCustomCoinsCurrency.Remove(Convert.ToString(((CustomCoinBox)CoinBox.SelectedItem).Currency));
                    ChangeCurrency = ComboEditCoinPercentage.Text;
                    CustomCoin = ComboEditCoinName.Text;
                }
                else
                {
                    CustomWorker = WorkerComboBox.Text;
                }


                WorkerToggled = false;

                int Index = SelectWorker(WORKERNAME).IndexOf(CustomWorker);
                string CustomPool = SelectWorker(POOL).ElementAt(Index);
                Index = SelectMiningSettings(CUSTOMID).IndexOf()
                string CustomWalletAddress = SelectMiningSettings()

                if (CustomPool == "NiceHash")
                {
                    try
                    {
                        string url = @"https://api.nicehash.com/api?method=stats.provider.workers&addr=" + strAddWorkerWalletAddress;

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                        request.AutomaticDecompression = DecompressionMethods.GZip;

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            initialWorkerInfo = reader.ReadToEnd();
                            if (!initialWorkerInfo.Contains("Incorrect BTC address specified"))
                            {
                                relevantWorkerInfo = References.EaseMethods.getBetween(initialWorkerInfo, "workers\":[", "],\"algo\"");
                                WorkerInfoA = Regex.Split(relevantWorkerInfo, "],");
                                SepWorkerInfoA = WorkerInfoA.OfType<string>().ToList();
                                for (int x = 0; x < SepWorkerInfoA.Count; x++)
                                {
                                    initialWorker = SepWorkerInfoA[x];
                                    RegexSplitWorkerInfo = Regex.Split(initialWorker, ",");
                                    sessWorkerName = References.EaseMethods.getBetween(RegexSplitWorkerInfo[0], "[\"", "\"");
                                    if (sessWorkerName.Length == 0)
                                    {
                                        sessWorkerName = "unknown";
                                    }

                                    WorkerList.Add(sessWorkerName);
                                }
                            }
                            else
                            {
                                WorkerList.Add("no data found");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        WorkerList.Add("no data found");
                    }

                    if (UpdatingCustomCoin == true)
                    {
                        int index = CoinBox.SelectedIndex;
                        CoinBox.Items.RemoveAt(index);
                        CoinBox.Items.Insert(index, new CustomCoinBox() { CustomCoinName = CustomCoin, CustomCoinFontSize = FontSize, ValueUSD = CustomCoinPrice[0], ValueBTC = CustomCoinPrice[1], PortfolioQuan = "N/A", PortfolioValue = "N/A", Change24Hour = CustomCoin24[Currency], Change7Day = CustomCoin7[Currency], OverallTrend = Change7, Trend24 = Change24, Trend7 = Change7, Currency = Currency });
                        SessionCustomCoins.Insert(index, CustomCoin);
                        SessionCustomCoinsCurrency.Insert(index, Convert.ToString(Currency));
                    }
                    else
                    {
                        CoinBox.Items.Add(new CustomCoinBox() { CustomCoinName = CustomCoin, CustomCoinFontSize = FontSize, ValueUSD = CustomCoinPrice[0], ValueBTC = CustomCoinPrice[1], PortfolioQuan = "N/A", PortfolioValue = "N/A", Change24Hour = CustomCoin24[Currency], Change7Day = CustomCoin7[Currency], OverallTrend = Change7, Trend24 = Change24, Trend7 = Change7, Currency = Currency });
                        SessionCustomCoins.Add(CustomCoin);
                        SessionCustomCoinsCurrency.Add(Convert.ToString(Currency));
                    }

                    CoinComboBox.Text = "";
                    ComboCoinPercentage.Text = "";
                }
                else if (CustomPool == "SiaMining")
                {

                }

                

            }
            catch (Exception)
            {
                CoinComboBox.Text = "";
                ComboCoinPercentage.Text = "";
                Incorrect = true;
            }
        }

        private void btnCustomCoin_Click(object sender, RoutedEventArgs e)
        {
            if (CoinComboBox.Text.Length != 0 && SepCoinN.Contains(CoinComboBox.Text))
            {
                UpdatingCustomCoin = false;
                CustomCoinCalculation();
                Incorrect = false;
            }
            else
            {
                Incorrect = true;
            }
        }

        private void btnCustomWorker_Click(object sender, RoutedEventArgs e)
        {
            if (WorkerComboBox.Text.Length != 0 && WorkerCustomList.Contains(WorkerComboBox.Text))
            {
                UpdatingCustomWorker = false;
                CustomWorkerCalculation();
                Incorrect = false;
            }
            else
            {
                Incorrect = true;
            }
            WorkerBox.Items.Add(new CustomWorkerBox() { });
        }

        public void ChangeCurrencyMethod(int Y)
        {
            if (Convert.ToDouble(CustomCoin24[Y]) < 0) { Change24 = "TrendingDown"; }
            else if (Convert.ToDouble(CustomCoin24[Y]) > 0) { Change24 = "TrendingUp"; }
            else { Change24 = "TrendingNeutral"; }

            if (Convert.ToDouble(CustomCoin7[Y]) < 0) { Change7 = "TrendingDown"; }
            else if (Convert.ToDouble(CustomCoin7[Y]) > 0) { Change7 = "TrendingUp"; }
            else { Change7 = "TrendingNeutral"; }

            CustomCoin24[Y] = CustomCoin24[Y] + "%";
            CustomCoin7[Y] = CustomCoin7[Y] + "%";
        }

        private void CustomCoinDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditCustomCoin.IsOpen = true;
            try
            {
                ComboEditCoinName.ItemsSource = SepCoinN;
                ComboEditCoinName.Text = ((CustomCoinBox)CoinBox.SelectedItem).CustomCoinName;
                if (((CustomCoinBox)CoinBox.SelectedItem).Currency == 0)
                {
                    ComboEditCoinPercentage.Text = "USD";
                }
                else
                {
                    ComboEditCoinPercentage.Text = "BTC";
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnDeleteCustomCoin_Click(object sender, RoutedEventArgs e)
        {
            int index = CoinBox.SelectedIndex;
            CoinBox.Items.RemoveAt(index);
            SessionCustomCoins.RemoveAt(index);
            SessionCustomCoinsCurrency.RemoveAt(index);
            Incorrect = false;
        }

        private void btnUpdateCustomCoin_Click(object sender, RoutedEventArgs e)
        {
            if (ComboEditCoinName.Text.Length != 0)
            {
                UpdatingCustomCoin = true;
                CustomCoinCalculation();
                Incorrect = false;
            }
            else
            {
                Incorrect = true;
            }
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Incorrect == true)
            {
                eventArgs.Cancel();
                Incorrect = false;
            }
        }

        

        private void Header_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AccountSettings.Visibility = Visibility.Hidden;
        }



        private void CryptoPocket_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMustFiles();

            if (new FileInfo(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt").Length == 0)
            {
                string url = @"https://api.coinmarketcap.com/v2/listings/";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    initialCoinList = reader.ReadToEnd();
                    relevantCoinList = References.EaseMethods.getBetween(initialCoinList, "[", "],");
                    CoinListA = Regex.Split(relevantCoinList, "},");
                    SepCoinList = CoinListA.OfType<string>().ToList();
                    for (int x = 0; x < SepCoinList.Count; x++)
                    {
                        initialCoinN = SepCoinList[x];
                        relevantCoinN = References.EaseMethods.getBetween(initialCoinN, "\",", "\"website");
                        CoinN = References.EaseMethods.getBetween(relevantCoinN, ": \"", "\",");
                        SepCoinN.Add(CoinN);

                        relevantCoinID = References.EaseMethods.getBetween(initialCoinN, "id\": ", ",");
                        SepCoinID.Add(relevantCoinID);
                    }
                }

                File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoins.txt", SepCoinN);
                File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoinsID.txt", SepCoinID);
                CoinComboBox.ItemsSource = SepCoinN;
            }
            else
            {
                SepCoinID = File.ReadAllLines(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoinsID.txt").ToList();
                SepCoinN = File.ReadAllLines(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoins.txt").ToList();
                CoinComboBox.ItemsSource = SepCoinN;
            }

            if (new FileInfo(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt").Length == 0)
            {
                DefaultLogin = true;
            }
            else
            {
                PreviousUsername = File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt");
                DefaultLogin = false;


                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AccountSettings.LoginEditButton);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
            (sender as DispatcherTimer).Stop();
        }

        private void CustomCoinToggle_Click(object sender, RoutedEventArgs e)
        {

            object clicked = (e.OriginalSource as FrameworkElement).DataContext;
            var lbi = CoinBox.ItemContainerGenerator.ContainerFromItem(clicked) as ListBoxItem;
            lbi.IsSelected = true;

            try
            {
                // Getting the currently selected ListBoxItem
                // Note that the ListBox must have
                // IsSynchronizedWithCurrentItem set to True for this to work
                ListBoxItem CoinBoxItem =
                    (ListBoxItem)(CoinBox.ItemContainerGenerator.ContainerFromItem(CoinBox.Items.CurrentItem));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(CoinBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;

                TextBlock tempPortfolioCoin = (TextBlock)myDataTemplate.FindName("PortfolioCoin", myContentPresenter);
                TextBlock tempPortfolioCoinV = (TextBlock)myDataTemplate.FindName("PortfolioCoinV", myContentPresenter);
                TextBlock temptxt24Hours = (TextBlock)myDataTemplate.FindName("txt24Hours", myContentPresenter);
                TextBlock temptxt7Days = (TextBlock)myDataTemplate.FindName("txt7Days", myContentPresenter);
                TextBlock temptxt24HoursV = (TextBlock)myDataTemplate.FindName("txt24HoursV", myContentPresenter);
                TextBlock temptxt7DaysV = (TextBlock)myDataTemplate.FindName("txt7DaysV", myContentPresenter);
                MaterialDesignThemes.Wpf.PackIcon tempIcon24Hours = (MaterialDesignThemes.Wpf.PackIcon)myDataTemplate.FindName("Icon24Hours", myContentPresenter);
                MaterialDesignThemes.Wpf.PackIcon tempIcon7Days = (MaterialDesignThemes.Wpf.PackIcon)myDataTemplate.FindName("Icon7Days", myContentPresenter);
                TextBlock tempToggleCoinText = (TextBlock)myDataTemplate.FindName("ToggleCoinText", myContentPresenter);

                if (CoinToggled == false)
                {
                    tempPortfolioCoin.Opacity = 100;
                    tempPortfolioCoinV.Opacity = 100;
                    temptxt24Hours.Opacity = 0;
                    temptxt7Days.Opacity = 0;
                    temptxt24HoursV.Opacity = 0;
                    temptxt7DaysV.Opacity = 0;
                    tempIcon24Hours.Opacity = 0;
                    tempIcon7Days.Opacity = 0;
                    CoinToggled = true;
                    tempToggleCoinText.Text = "Portfolio Value";

                }
                else if (CoinToggled == true)
                {
                    tempPortfolioCoin.Opacity = 0;
                    tempPortfolioCoinV.Opacity = 0;
                    temptxt24Hours.Opacity = 100;
                    temptxt7Days.Opacity = 100;
                    temptxt24HoursV.Opacity = 100;
                    temptxt7DaysV.Opacity = 100;
                    tempIcon24Hours.Opacity = 100;
                    tempIcon7Days.Opacity = 100;
                    CoinToggled = false;
                    tempToggleCoinText.Text = "Recent Change";
                }
            }
            catch (Exception)
            {

            }

        }

        private void CustomWorkerToggle_Click(object sender, RoutedEventArgs e)
        {

            object clicked = (e.OriginalSource as FrameworkElement).DataContext;
            var lbi = WorkerBox.ItemContainerGenerator.ContainerFromItem(clicked) as ListBoxItem;
            lbi.IsSelected = true;

            try
            {
                // Getting the currently selected ListBoxItem
                // Note that the ListBox must have
                // IsSynchronizedWithCurrentItem set to True for this to work
                ListBoxItem WorkerBoxItem =
                    (ListBoxItem)(WorkerBox.ItemContainerGenerator.ContainerFromItem(WorkerBox.Items.CurrentItem));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(WorkerBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;

                TextBlock tempProfitabilityDay = (TextBlock)myDataTemplate.FindName("ProfitabilityDay", myContentPresenter);
                TextBlock tempProfitabilityDayAlt = (TextBlock)myDataTemplate.FindName("ProfitabilityDayAlt", myContentPresenter);

                if (WorkerToggled == true)
                {
                    tempProfitabilityDay.Opacity = 100;
                    tempProfitabilityDayAlt.Opacity = 0;
                    WorkerToggled = false;
                }
                else if (WorkerToggled == false)
                {
                    tempProfitabilityDay.Opacity = 0;
                    tempProfitabilityDayAlt.Opacity = 100;
                    WorkerToggled = true;
                }
            }
            catch (Exception)
            {

            }

        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void CoinBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void WorkerBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void WorkerBox_Hover(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            CustomWorkerButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        private void WorkerBox_Leave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            CustomWorkerButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        private void CoinBox_Hover(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            CustomCoinButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        private void CoinBox_Leave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            CustomCoinButton.BeginAnimation(Button.OpacityProperty, animation);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (SettingsActive == false)
            {
                AccountSettings.Visibility = Visibility.Visible;
                SettingsActive = true;
            }
            else if (SettingsActive == true)
            {
                AccountSettings.Visibility = Visibility.Hidden;
                SettingsActive = false;
            }

        }

        private void Header_Click(object sender, MouseButtonEventArgs e)
        {
            SettingsActive = false;
            AccountSettings.Visibility = Visibility.Hidden;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (LoggedIn == true)
            {
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", HeaderUser.Text);
                DeleteCustomCoins();
                for (int x = 0; x < SessionCustomCoins.Count; x++)
                {
                    string coin = SessionCustomCoins[x];
                    string Currency = SessionCustomCoinsCurrency[x];
                    InsertCustomCoins(coin, Currency);
                }
                CoinBox.Items.Clear();
                SessionCustomCoins.Clear();
                SessionCustomCoinsCurrency.Clear();
            }
            else
            {
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", "");
            }
            Environment.Exit(1);
        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {

        }

        //Checking and Creating Required Files
        public static void CreateMustFiles()
        {
            if (!File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt") || !File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoins.txt") || !File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoinsID.txt"))
            {
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session");
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins");
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", "");
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoins.txt", "");
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Coins\ActiveCoinsID.txt", "");
            }
        }

        private void HeaderMouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush BrushHover = new SolidColorBrush(HoverColour);
            Button X = (Button)sender;
            X.Background = BrushHover;
        }

        private void HeaderMouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush BrushUnhover = new SolidColorBrush(UnhoverColour);
            Button X = (Button)sender;
            X.Background = BrushUnhover;
        }

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
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }

            CurrentID = Convert.ToInt32(Select(ID).ElementAt((Select(EMAIL).IndexOf(EmailUp))));
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

        public void InsertMember()
        {
            string query = "INSERT INTO CryptoMembership (ID, MEMBERSHIP) VALUES('" + CurrentID + "', '" + AccountSettings.FreeMembership + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void InsertElectricity()
        {
            string query = "INSERT INTO CryptoElectricity (ID, ELECTRICITY) VALUES('" + CurrentID + "', '" + AccountSettings.ElectricitiyRate + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        private Task InsertMiningSettings()
        {
            return Task.Run(() =>
            {
                string query = "INSERT INTO CryptoMiningSettings (ID, CUSTOMID, WALLET) VALUES('" + CurrentID + "', '" + SaveCustomID + "', '" + SaveWalletAddress + "')";

                if (OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        this.CloseConnection();

                        this.Dispatcher.Invoke(() =>
                        {
                            if (WalletCustomIDs[0] == "no data found")
                            {
                                WalletCustomIDs.Clear();
                            }
                            WalletCustomIDs.Add(SaveCustomID);
                            AccountSettings.ComboBoxIDs.ItemsSource = WalletCustomIDs;
                        });
                    }
                    catch (Exception)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            SaveWalletIDInfo.IsOpen = true;
                        });

                    }
                }
            });
        }

        private Task InsertWorker()
        {
            return Task.Run(() =>
            {
                string query = "INSERT INTO CryptoWorkers (ID, CUSTOMID, WALLETID, WORKER, POWER) VALUES('" + CurrentID + "', '" + strAddWorkerNameID + "', '" + strAddWorkerWalletID + "', '" + strAddWorkerName + "', '" + strAddWorkerElectricity + "')";

                if (OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        this.CloseConnection();

                        this.Dispatcher.Invoke(() =>
                        {
                            WorkerCustomList.Add(strAddWorkerNameID);
                            WorkerComboBox.ItemsSource = WorkerCustomList;
                            MiningRemoveComboBox.ItemsSource = WorkerCustomList;
                        });
                    }
                    catch (Exception)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            AddWorkerInfo.IsOpen = true;
                        });

                    }
                }
            });
        }

        private Task DeleteWorker()
        {
            return Task.Run(() =>
            {
                string query = "DELETE FROM CryptoWorkers WHERE ID='" + CurrentID + "' AND CUSTOMID='" + strRemoveWorkerName + "'";

                if (OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        this.CloseConnection();

                        this.Dispatcher.Invoke(() =>
                        {
                            WorkerCustomList.Remove(strRemoveWorkerName);
                            WorkerComboBox.ItemsSource = WorkerCustomList;
                            MiningRemoveComboBox.ItemsSource = WorkerCustomList;

                            //DeleteCustomWorkers();
                            //for (int x = 0; x < SessionCustomCoins.Count; x++)
                            //{
                            //    string coin = SessionCustomCoins[x];
                            //    string Currency = SessionCustomCoinsCurrency[x];
                            //    InsertCustomCoins(coin, Currency);
                            //}
                            //CoinBox.Items.Clear();
                            //SessionCustomCoins.Clear();
                            //SessionCustomCoinsCurrency.Clear();
                        });
                    }
                    catch (Exception)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            RemoveWorkerInfo.IsOpen = true;
                        });

                    }
                }
            });

        }

        public void DeleteMiningSettings()
        {
            string query = "DELETE FROM CryptoMiningSettings WHERE ID='" + CurrentID + "'AND CUSTOMID='" + RemoveCustomID + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
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
            string query = "SELECT * FROM CryptoMiningSettings WHERE ID='" + CurrentID + "'";

            WalletCustomIDs.Clear();
            WalletCustomAddresses.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    WalletCustomIDs.Add(dataReader["CUSTOMID"] + "");
                    WalletCustomAddresses.Add(dataReader["WALLET"] + "");
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


        public ObservableCollection<string> SelectWorkerForUser(ObservableCollection<string> Ref)
        {
            string query = "SELECT * FROM CryptoWorkers WHERE ID='" + CurrentID + "'";

            WorkerCustomList.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    WorkerCustomList.Add(dataReader["CUSTOMID"] + "");
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

        public List<string> SelectWorker(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoWorkers WHERE ID='" + CurrentID + "'";

            WORKERNAME.Clear();
            POWER.Clear();
            POOL.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    WORKERNAME.Add(dataReader["WORKER"] + "");
                    POWER.Add(dataReader["POWER"] + "");
                    POOL.Add(dataReader["POOL"] + "");
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
            string query = "SELECT * FROM CryptoElectricity WHERE ID='" + CurrentID + "'";

            AccountSettings.ID_ELEC.Clear();
            AccountSettings.ELECTRICITYRATE.Clear();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    AccountSettings.ID_ELEC.Add(dataReader["ID"] + "");
                    AccountSettings.ELECTRICITYRATE.Add(dataReader["ELECTRICITY"] + "");
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

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            ProgressDialog.IsOpen = false;
            (sender as DispatcherTimer).Stop();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoggingIn = true;
            SigningUp = false;

            Login.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnConfirmLogout_Click(object sender, RoutedEventArgs e)
        {
            LoggingIn = false;
            SigningUp = false;

            Logout.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            SigningUp = true;
            LoggingIn = false;

            SignUp.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnSaveWallet_Click(object sender, RoutedEventArgs e)
        {
            SavingWalletID = true;
            RemovingWalletID = false;

            SaveWalletID.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnRemoveWallet_Click(object sender, RoutedEventArgs e)
        {
            SavingWalletID = false;
            RemovingWalletID = true;

            RemoveWalletID.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerBool = true;
            RemoveWorkerBool = false;

            AddWorker.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void btnRemoveWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerBool = false;
            RemoveWorkerBool = true;

            RemoveWorker.IsOpen = false;

            ProgressDialog.IsOpen = true;
        }

        private void ProgressDialog_Loaded(object sender, DialogOpenedEventArgs eventArgs)
        {
            //Signing Up Calculation Pt.1

            if (SigningUp == true)
            {
                SigningUp = false;

                SignupEmail = txtSignupEmail.Text;
                SingupUsername = txtSignupUsername.Text;
                SignUpPassword = txtSignupPassword.Password;

                if (LoggedIn == true)
                {
                    DeleteCustomCoins();
                    for (int x = 0; x < SessionCustomCoins.Count; x++)
                    {
                        string coin = SessionCustomCoins[x];
                        string Currency = SessionCustomCoinsCurrency[x];
                        InsertCustomCoins(coin, Currency);
                    }
                    CoinBox.Items.Clear();
                    SessionCustomCoins.Clear();
                    SessionCustomCoinsCurrency.Clear();
                }


                if (IsValidEmail(SignupEmail) == false || SingupUsername == "" || SignUpPassword.Length < 8 || SingupUsername.Length > 20)
                {
                    ProgressDialog.IsOpen = false;
                    SignupInfo.IsOpen = true;
                    SignupInfoTitle.Text = "Signup Info";
                    SignupInfo1.Text = "Must be a valid email address";
                    SignupInfo2.Opacity = 100;
                    SignupInfo3.Opacity = 100;
                }
                else
                {
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                    dispatcherTimer.Start();

                    salt = CreateSalt(10);
                    hashedpassword = GenerateSHA256Hash(SignUpPassword, salt);
                    EmailUp = SignupEmail;
                    UserUp = SingupUsername;

                    AccountSettings.ElectricitiyRate = "0.1";

                    WalletCustomIDs.Clear();
                    AccountSettings.ComboBoxIDs.ItemsSource = null;

                    InsertNewUser();
                }
            }

            // Logging In Calculation Pt.1

            else if (LoggingIn == true)
            {
                LoggingIn = false;

                strLoginUsername = LoginUsername.Text;
                strLoginPassword = LoginPassword.Password;

                if (LoggedIn == true)
                {
                    DeleteCustomCoins();
                    for (int x = 0; x < SessionCustomCoins.Count; x++)
                    {
                        string coin = SessionCustomCoins[x];
                        string Currency = SessionCustomCoinsCurrency[x];
                        InsertCustomCoins(coin, Currency);
                    }
                    CoinBox.Items.Clear();
                    SessionCustomCoins.Clear();
                    SessionCustomCoinsCurrency.Clear();
                }

                if (strLoginUsername.Length > 20 || strLoginPassword.Length < 8)
                {
                    ProgressDialog.IsOpen = false;
                    LoginInfo.IsOpen = true;
                    if (strLoginUsername == PreviousUsername)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            LoginInfo.IsOpen = true;
                            LoginInfoTitle.Text = "Invalid Input";
                            LoginInfo1.Text = "Password is incorrect.";
                            LoginInfo2.Opacity = 0;
                            LoginPassword.Password = "";
                        });
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            LoginInfo.IsOpen = true;
                            LoginInfoTitle.Text = "Invalid Input";
                            LoginInfo1.Text = "Username or Password is incorrect.";
                            LoginInfo2.Opacity = 0;
                            LoginPassword.Password = "";
                        });
                    }
                }
                else
                {
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                    dispatcherTimer.Start();

                    LoginUser();
                }
            }

            //Logging Out Calculation Pt.1

            else if (LoggedIn == true && SigningUp == false && LoggingIn == false && SavingWalletID == false 
                && RemovingWalletID == false && AddWorkerBool == false && RemoveWorkerBool == false)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                dispatcherTimer.Start();

                LogoutUserCalc();
            }

            //Saving Wallet ID | Pt.1

            else if (SavingWalletID == true)
            {
                SavingWalletID = false;

                if (CustomID.Text.Length == 0 || WalletAddress.Text.Length == 0 || LoggedIn == false)
                {
                    ProgressDialog.IsOpen = false;
                    SaveWalletIDInfo.IsOpen = true;
                    SaveWalletIDInfoTitle.Text = "Save Wallet ID Form Info";
                    SaveWalletIDInfo1.Text = "Wallet ID and Wallet Address fields cannot be empty.";
                    SaveWalletIDInfo2.Opacity = 100;
                    SaveWalletIDInfo3.Opacity = 100;
                }
                else
                {
                    if (WalletCustomIDs.Contains(CustomID.Text))
                    {
                        ProgressDialog.IsOpen = false;
                        SaveWalletIDInfo.IsOpen = true;
                        SaveWalletIDInfoTitle.Text = "Save Wallet ID Form Info";
                        SaveWalletIDInfo1.Text = "Wallet ID and Wallet Address fields cannot be empty.";
                        SaveWalletIDInfo2.Opacity = 100;
                        SaveWalletIDInfo3.Opacity = 100;
                    }
                    else
                    {
                        dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                        dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                        dispatcherTimer.Start();

                        SaveCustomID = CustomID.Text;
                        SaveWalletAddress = WalletAddress.Text;

                        InsertWalletID();
                    }
                }
            }

            //Remove Wallet ID | Pt.1

            else if (RemovingWalletID == true)
            {
                RemovingWalletID = false;

                if (RemoveComboBoxIDs.Text.Length != 0 && LoggedIn == true)
                {
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                    dispatcherTimer.Start();

                    RemoveCustomID = RemoveComboBoxIDs.Text;

                    DeleteWalletID();
                }
                else
                {
                    ProgressDialog.IsOpen = false;
                    RemoveWalletIDInfo.IsOpen = true;
                    RemoveWalletIDInfoTitle.Text = "Remove Wallet ID Form Info";
                    RemoveWalletIDInfo1.Text = "Wallet ID field cannot be empty.";
                    RemoveWalletIDInfo2.Opacity = 100;
                }
            }

            else if (AddWorkerBool == true)
            {
                AddWorkerBool = false;

                if (LoggedIn == true && AddWorkerNameCustomID.Text.Length != 0 && AddWorkerPool.Text.Length != 0 && AddWorkerWalletCustomID.Text.Length != 0 && AddWorkerName.Text.Length != 0 && AddWorkerElectricity.Text.Length != 0)
                {
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                    dispatcherTimer.Start();

                    strAddWorkerNameID = AddWorkerNameCustomID.Text;
                    strAddWorkerPool = AddWorkerPool.Text;
                    strAddWorkerWalletID = AddWorkerWalletCustomID.Text;
                    strAddWorkerName = AddWorkerName.Text;
                    strAddWorkerElectricity = AddWorkerElectricity.Text;
                    
                    strAddWorkerElectricity = EaseMethods.KeepOnlyNumbers(strAddWorkerElectricity);

                    AddWorkerTask();
                }
                else
                {
                    ProgressDialog.IsOpen = false;
                    AddWorkerInfo.IsOpen = true;
                    AddWorkerInfoTitle.Text = "Add Worker Form Info";
                    AddWorkerInfo1.Text = "Power consumption is an optional field.";
                    AddWorkerInfo2.Opacity = 100;
                    AddWorkerInfo3.Opacity = 100;
                    AddWorkerInfo4.Opacity = 100;
                }
            }

            else if (RemoveWorkerBool == true)
            {
                RemoveWorkerBool = false;

                if (LoggedIn == true && MiningRemoveComboBox.Text.Length != 0)
                {
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer2_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                    dispatcherTimer.Start();

                    strRemoveWorkerName = MiningRemoveComboBox.Text;


                }
                else
                {

                }
            }
        }

        //Async | Signing Up Calculation Pt.2

        async Task InsertNewUser()
        {
            await InsertNewUserCalculation();
        }

        private Task InsertNewUserCalculation()
        {
            return Task.Run(() =>
            {
                if (Select(EMAIL).Contains(EmailUp))
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressDialog.IsOpen = false;
                        SignupInfo.IsOpen = true;
                        SignupInfoTitle.Text = "Invalid Input";
                        SignupInfo1.Text = "Email address is already registered.";
                        SignupInfo2.Opacity = 0;
                        SignupInfo3.Opacity = 0;
                    });
                }
                else if (Select(USERNAME).Contains(UserUp))
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressDialog.IsOpen = false;
                        SignupInfo.IsOpen = true;
                        SignupInfoTitle.Text = "Invalid Input";
                        SignupInfo1.Text = "Username is already registered.";
                        SignupInfo2.Opacity = 0;
                        SignupInfo3.Opacity = 0;
                    });
                }
                else
                {
                    try
                    {
                        Insert();
                        InsertMember();
                        InsertElectricity();

                        this.Dispatcher.Invoke(() =>
                        {
                            SelectMember(MEMBERSHIP);
                            AccountSettings.SettingsMembership.Text = "Membership: " + MEMBERSHIP[0];

                            AccountSettings.SettingsEmail.Text = "Email: " + EmailUp;
                            AccountSettings.SettingsUsername.Text = UserUp;
                            HeaderUser.Text = UserUp;

                            txtSignupEmail.Text = "";
                            txtSignupUsername.Text = "";
                            txtSignupPassword.Password = "";

                            AccountSettings.txtElectricityRate.Text = AccountSettings.ElectricitiyRate;

                            AccountSettings.LoginIcon.Kind = PackIconKind.Logout;
                            AccountSettings.LoginEditButton.ToolTip = "Logout";
                            AccountSettings.LoginText.Text = "LOGOUT";

                            LoggedIn = true;

                            File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", HeaderUser.Text);
                        });
                    }
                    catch (Exception)
                    {
                        dispatcherTimer.Stop();
                        this.Dispatcher.Invoke(() =>
                        {
                            ProgressDialog.IsOpen = false;
                            SignupInfo.IsOpen = true;
                            SignupInfoTitle.Text = "Server-side Issue";
                            SignupInfo1.Text = "Please try again later.";
                            SignupInfo2.Opacity = 0;
                            SignupInfo3.Opacity = 0;
                        });
                    }


                }
            });
        }

        //Async | Logging In Calculation Pt.2

        async Task LoginUser()
        {
            await LoginUserCalculation();
        }

        private Task LoginUserCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    int Index = Select(USERNAME).IndexOf(strLoginUsername);
                    hashedpassword = Select(HASH)[Index];
                    salt = Select(SALT)[Index];

                    if (hashedpassword == GenerateSHA256Hash(strLoginPassword, salt))
                    {
                        CurrentID = Convert.ToInt32(Select(ID).ElementAt(Select(EMAIL).IndexOf(Select(EMAIL)[Index])));

                        SelectMember(MEMBERSHIP);

                        SelectElectricity(AccountSettings.ELECTRICITYRATE);
                        AccountSettings.ElectricitiyRate = AccountSettings.ELECTRICITYRATE[0];

                        SelectCustomCoins(C_COIN);
                        SelectCustomCoins(C_CURRENCY);
                        SessionCustomCoins = C_COIN;
                        SessionCustomCoinsCurrency = C_CURRENCY;

                        LoginCustomCoinCalculation();

                        this.Dispatcher.Invoke(() =>
                        {
                            SelectMiningSettingsForUser(WalletCustomIDs);
                            SelectWorkerForUser(WorkerCustomList);

                            AccountSettings.SettingsEmail.Text = "Email: " + Select(EMAIL)[Index];
                            AccountSettings.SettingsUsername.Text = strLoginUsername;
                            HeaderUser.Text = strLoginUsername;

                            AccountSettings.SettingsMembership.Text = "Membership: " + MEMBERSHIP[0];

                            AccountSettings.txtElectricityRate.Text = AccountSettings.ElectricitiyRate;

                            AccountSettings.ComboBoxIDs.ItemsSource = WalletCustomIDs;
                            WorkerComboBox.ItemsSource = WorkerCustomList;

                            LoginUsername.Text = "";
                            LoginPassword.Password = "";

                            AccountSettings.LoginIcon.Kind = PackIconKind.Logout;
                            AccountSettings.LoginEditButton.ToolTip = "Logout";
                            AccountSettings.LoginText.Text = "LOGOUT";

                            LoggedIn = true;

                            File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", HeaderUser.Text);
                        });
                    }
                    else
                    {
                        if (strLoginUsername == PreviousUsername)
                        {
                            dispatcherTimer.Stop();
                            this.Dispatcher.Invoke(() =>
                            {
                                ProgressDialog.IsOpen = false;
                                LoginInfo.IsOpen = true;
                                LoginInfoTitle.Text = "Invalid Input";
                                LoginInfo1.Text = "Password is incorrect.";
                                LoginInfo2.Opacity = 0;
                                LoginPassword.Password = "";
                            });

                        }
                        else
                        {
                            dispatcherTimer.Stop();
                            this.Dispatcher.Invoke(() =>
                            {
                                ProgressDialog.IsOpen = false;
                                LoginInfo.IsOpen = true;
                                LoginInfoTitle.Text = "Invalid Input";
                                LoginInfo1.Text = "Username or Password is incorrect.";
                                LoginInfo2.Opacity = 0;
                                LoginPassword.Password = "";
                            });

                        }
                    }
                }
                catch (Exception)
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressDialog.IsOpen = false;
                        LoginInfo.IsOpen = true;
                        LoginInfoTitle.Text = "Server-side Issue";
                        LoginInfo1.Text = "Please try again later.";
                        LoginInfo2.Opacity = 0;
                        LoginPassword.Password = "";
                    });
                }
            });
        }

        //Async | Logging In Calculation Pt.3

        async Task LoginCustomCoinCalculation()
        {
            await ReturningCustomCoinCalculation();
        }

        private Task ReturningCustomCoinCalculation()
        {
            return Task.Run(() =>
            {
                List<string> index = new List<string>();
                for (int x = 0; x < SessionCustomCoins.Count; x++)
                {
                    CustomCoin = SessionCustomCoins[x];
                    int Currency = Convert.ToInt32(SessionCustomCoinsCurrency[x]);

                    index.Add(Convert.ToString(SepCoinN.IndexOf(CustomCoin) + 1));

                    CoinToggled = false;

                    CustomCoinPrice.Clear();
                    CustomCoin24.Clear();
                    CustomCoin7.Clear();

                    string url = @"https://api.coinmarketcap.com/v2/ticker/" + index[x] + "/?convert=BTC";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        initialCustomCoin = reader.ReadToEnd();
                        relevantCustomCoin = References.EaseMethods.getBetween(initialCustomCoin, "\"USD\": {", "\"last_updated\"");
                        CustomCoinA = Regex.Split(relevantCustomCoin, "},");
                        SepCustomCoinA = CustomCoinA.OfType<string>().ToList();
                        for (int xx = 0; xx < SepCustomCoinA.Count; xx++)
                        {
                            initialCoinB = SepCustomCoinA[xx];
                            CustomPrice = References.EaseMethods.getBetween(initialCoinB, "price\": ", ",");
                            CustomChange24 = References.EaseMethods.getBetween(initialCoinB, "percent_change_24h\": ", ",");
                            CustomChange7 = EaseMethods.RemoveKeepingColumnAndDots(initialCoinB);
                            RegexSplitChange7 = Regex.Split(CustomChange7, ":");
                            CustomChange7 = RegexSplitChange7.Last();
                            CustomCoinPrice.Add(CustomPrice);
                            CustomCoin24.Add(CustomChange24);
                            CustomCoin7.Add(CustomChange7);
                        }
                    }

                    string FontSize;
                    decimal rounding;

                    if (CustomCoin.Length == 5) { FontSize = "14"; }
                    else if (CustomCoin.Length == 6) { FontSize = "12"; }
                    else if (CustomCoin.Length >= 7) { FontSize = "10"; }
                    else { FontSize = "16"; }

                    if (CustomCoinPrice[0].Contains("e")) { rounding = Decimal.Parse(CustomCoinPrice[0], System.Globalization.NumberStyles.Float); }
                    else { rounding = Convert.ToDecimal(CustomCoinPrice[0]); }
                    rounding = Math.Round(rounding, 2);
                    CustomCoinPrice[0] = Convert.ToString(rounding);

                    if (CustomCoinPrice[1].Contains("e")) { rounding = Decimal.Parse(CustomCoinPrice[1], System.Globalization.NumberStyles.Float); }
                    else { rounding = Convert.ToDecimal(CustomCoinPrice[1]); }
                    rounding = Math.Round(rounding, 8);
                    CustomCoinPrice[1] = Convert.ToString(rounding);

                    CustomCoinPrice[0] = CustomCoin + "/USD: $" + CustomCoinPrice[0];
                    CustomCoinPrice[1] = CustomCoin + "/BTC: " + CustomCoinPrice[1];

                    if (Currency == 0)
                    {
                        ChangeCurrencyMethod(Currency);
                    }
                    else if (SessionCustomCoinsCurrency[x] == "1")
                    {
                        ChangeCurrencyMethod(Currency);
                    }

                    this.Dispatcher.Invoke(() =>
                    {
                        CoinBox.Items.Add(new CustomCoinBox()
                        {
                            CustomCoinName = CustomCoin,
                            CustomCoinFontSize = FontSize,
                            ValueUSD = CustomCoinPrice[0],
                            ValueBTC = CustomCoinPrice[1],
                            PortfolioQuan = "N/A",
                            PortfolioValue = "N/A",
                            Change24Hour = CustomCoin24[Currency],
                            Change7Day = CustomCoin7[Currency],
                            OverallTrend = Change7,
                            Trend24 = Change24,
                            Trend7 = Change7,
                            Currency = Currency
                        });

                        CoinComboBox.Text = "";
                        ComboCoinPercentage.Text = "";
                    });
                }
            });
        }

        async Task LogoutUserCalc()
        {
            await LogoutUserCalculation();
        }

        private Task LogoutUserCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    DeleteCustomCoins();

                    this.Dispatcher.Invoke(() =>
                    {
                        for (int x = 0; x < SessionCustomCoins.Count; x++)
                        {
                            string coin = SessionCustomCoins[x];
                            string Currency = SessionCustomCoinsCurrency[x];
                            InsertCustomCoins(coin, Currency);
                        }
                        CoinBox.Items.Clear();
                        SessionCustomCoins.Clear();
                        SessionCustomCoinsCurrency.Clear();

                        AccountSettings.ComboBoxIDs.ItemsSource = null;
                        WorkerComboBox.ItemsSource = null;

                        AccountSettings.LoginIcon.Kind = PackIconKind.Login;
                        AccountSettings.LoginEditButton.ToolTip = "Login";
                        AccountSettings.LoginText.Text = "LOGIN";

                        HeaderUser.Text = "Guest";
                        AccountSettings.SettingsUsername.Text = "Guest";
                        AccountSettings.SettingsEmail.Text = "Email: N/A";
                        AccountSettings.SettingsMembership.Text = "Membership: Free";
                        AccountSettings.SettingsDevices.Text = "Connected Devices: N/A";
                        AccountSettings.txtElectricityRate.Text = "0.1";
                        AccountSettings.ElectricitiyRate = "0.1";

                        File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", HeaderUser.Text);

                    });

                    CurrentID = 0;
                    LoggedIn = false;
                }
                catch (Exception)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        dispatcherTimer.Stop();
                        Logout.IsOpen = false;
                        ServerError.IsOpen = true;
                    });
                }
            });
        }

        async Task InsertWalletID()
        {
            await InsertWalletIDCalculation();
        }

        private Task InsertWalletIDCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        InsertMiningSettings();
                        CustomID.Text = "";
                        WalletAddress.Text = "";
                    });
                }
                catch (Exception)
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressDialog.IsOpen = false;
                        SaveWalletIDInfo.IsOpen = true;
                        SaveWalletIDInfoTitle.Text = "Server-side Issue";
                        SaveWalletIDInfo1.Text = "Please try again later.";
                        SaveWalletIDInfo2.Opacity = 0;
                        SaveWalletIDInfo3.Opacity = 0;
                    });
                }
            });
        }

        async Task DeleteWalletID()
        {
            await DeleteWalletIDCalculation();
        }

        private Task DeleteWalletIDCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DeleteMiningSettings();
                        WalletCustomIDs.Remove(RemoveComboBoxIDs.Text);
                        AccountSettings.ComboBoxIDs.ItemsSource = WalletCustomIDs;
                    });
                }
                catch (Exception)
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ProgressDialog.IsOpen = false;
                        RemoveWalletIDInfo.IsOpen = true;
                        RemoveWalletIDInfoTitle.Text = "Server-side Issue";
                        RemoveWalletIDInfo1.Text = "Please try again later.";
                        RemoveWalletIDInfo2.Opacity = 0;
                    });
                }
            });
        }

        async Task AddWorkerTask()
        {
            await AddWorkerTaskCalculation();
        }

        private Task AddWorkerTaskCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        InsertWorker();
                        AddWorkerNameCustomID.Text = "";
                        AddWorkerPool.Text = "";
                        AddWorkerWalletCustomID.Text = "";
                        AddWorkerName.Text = "";
                        AddWorkerElectricity.Text = "";

                    });
                }
                catch (Exception)
                {
                    dispatcherTimer.Stop();
                    this.Dispatcher.Invoke(() =>
                    {
                        ServeSideError(ProgressDialog,AddWorkerInfo,AddWorkerInfoTitle,AddWorkerInfo1,AddWorkerInfo2);
                        AddWorkerInfo3.Opacity = 0;
                        AddWorkerInfo4.Opacity = 0;
                    });
                }
            });
        }

        async Task RemoveWorkerTask()
        {
            await RemoveWorkerTaskCalculation();
        }

        private Task RemoveWorkerTaskCalculation()
        {
            return Task.Run(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        DeleteWorker();
                        MiningRemoveComboBox.Text = "";
                    });
                }
                catch (Exception)
                {
                    dispatcherTimer.Stop();
                    ServeSideError(ProgressDialog, RemoveWorkerInfo,RemoveWorkerInfoTitle,RemoveWorkerInfo1,RemoveWorkerInfo2);
                }
            });
        }

        private Task ServeSideError(DialogHost Progress, DialogHost InfoBox, TextBlock Title, TextBlock Line1, TextBlock Line2)
        {
            return Task.Run(() =>
            {
                Progress.IsOpen = false;
                InfoBox.IsOpen = true;
                Title.Text = "Server-Side Issue";
                Line1.Text = "Please try again later.";
                Line2.Opacity = 0;
            });
            
        }

        private void btnForceSignup_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AccountSettings.SignupButton);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void btnForceLogin_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AccountSettings.LoginEditButton);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void btnForceSaveWallet_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AccountSettings.btnSaveWalletID);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void btnForceRemoveWallet_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AccountSettings.btnRemoveWalletID);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void btnForceAddWorker_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(AddWorkerButton);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void btnForceRemoveWorker_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(RemoveWorkerButton);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void ResetInputs(object sender, DialogClosingEventArgs eventArgs)
        {
            LoginUsername.Text = "";
            LoginPassword.Password = "";

            txtSignupEmail.Text = "";
            txtSignupUsername.Text = "";
            txtSignupPassword.Password = "";

            CustomID.Text = "";
            WalletAddress.Text = "";
            RemoveComboBoxIDs.Text = "";

            AddWorkerNameCustomID.Text = "";
            AddWorkerPool.Text = "";
            AddWorkerWalletCustomID.Text = "";
            AddWorkerName.Text = "";
            AddWorkerElectricity.Text = "(optional) Watts";

            CoinComboBox.Text = "";
            ComboCoinPercentage.Text = "";

            ComboEditCoinName.Text = "";
            ComboEditCoinPercentage.Text = "";

            ComboEditWorkerName.Text = "";
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;
            if (Button.Name == "SignupInfoButton")
            {
                SignUp.IsOpen = false;
                SignupInfo.IsOpen = true;
                SignupInfoTitle.Text = "Signup Info";
                SignupInfo1.Text = "Must be a valid email address";
                SignupInfo2.Opacity = 100;
                SignupInfo3.Opacity = 100;
            }
            else if (Button.Name == "LoginInfoButton")
            {
                Login.IsOpen = false;
                LoginInfo.IsOpen = true;
                LoginInfoTitle.Text = "Login Info";
                LoginInfo1.Text = "Username cannot be longer than 20 characters.";
                LoginInfo2.Opacity = 100;
            }
            else if (Button.Name == "SaveWalletIDInfoButton")
            {
                SaveWalletID.IsOpen = false;
                SaveWalletIDInfo.IsOpen = true;
                SaveWalletIDInfoTitle.Text = "Save Wallet ID Form Info";
                SaveWalletIDInfo1.Text = "Wallet ID and Wallet Address fields cannot be empty.";
                SaveWalletIDInfo2.Opacity = 100;
                SaveWalletIDInfo3.Opacity = 100;
            }
            else if (Button.Name == "RemoveWalletIDInfoButton")
            {
                RemoveWalletID.IsOpen = false;
                RemoveWalletIDInfo.IsOpen = true;
                RemoveWalletIDInfoTitle.Text = "Remove Wallet ID Form Info";
                RemoveWalletIDInfo1.Text = "Wallet ID field cannot be empty.";
                RemoveWalletIDInfo2.Opacity = 100;
            }
        }

        private void btnWorker_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;
            if (Button.Name == "AddWorkerButton")
            {
                AddWorker.IsOpen = true;
            }
            else if (Button.Name == "RemoveWorkerButton")
            {
                RemoveWorker.IsOpen = true;
            }
            else if (Button.Name == "AddWorkerInfoButton")
            {
                AddWorker.IsOpen = false;
                AddWorkerInfo.IsOpen = true;
            }
            else if (Button.Name == "RemoveWorkerInfoButton")
            {
                RemoveWorker.IsOpen = false;
                RemoveWorkerInfo.IsOpen = true;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !NumericalInput.IsMatch(text);
        }

        private void NumbersOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void AddWorkerElectricity_LostFocus(object sender, RoutedEventArgs e)
        {
            AddWorkerElectricity.Text = EaseMethods.KeepOnlyNumbers(AddWorkerElectricity.Text);
            if (AddWorkerElectricity.Text.Length == 0)
            {
                AddWorkerElectricity.Text = "(optional) Watts";
            }
            else
            {
                AddWorkerElectricity.Text = AddWorkerElectricity.Text + " Watts";
            }
        }

        private void AddWorkerElectricity_GotFocus(object sender, RoutedEventArgs e)
        {
            AddWorkerElectricity.Text = EaseMethods.KeepOnlyNumbers(AddWorkerElectricity.Text);
        }

        async Task LookUpWalletAddress()
        {
            await LookUpWalletAddressCalculation();
        }

        private Task LookUpWalletAddressCalculation()
        {
            return Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (strAddWorkerPool == "NiceHash")
                    {
                        try
                        {
                            string url = @"https://api.nicehash.com/api?method=stats.provider.workers&addr=" + strAddWorkerWalletAddress;

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                            request.AutomaticDecompression = DecompressionMethods.GZip;

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                initialWorkerInfo = reader.ReadToEnd();
                                if (!initialWorkerInfo.Contains("Incorrect BTC address specified"))
                                {
                                    relevantWorkerInfo = References.EaseMethods.getBetween(initialWorkerInfo, "workers\":[", "],\"algo\"");
                                    WorkerInfoA = Regex.Split(relevantWorkerInfo, "],");
                                    SepWorkerInfoA = WorkerInfoA.OfType<string>().ToList();
                                    for (int x = 0; x < SepWorkerInfoA.Count; x++)
                                    {
                                        initialWorker = SepWorkerInfoA[x];
                                        RegexSplitWorkerInfo = Regex.Split(initialWorker, ",");
                                        sessWorkerName = References.EaseMethods.getBetween(RegexSplitWorkerInfo[0], "[\"", "\"");
                                        if (sessWorkerName.Length == 0)
                                        {
                                            sessWorkerName = "unknown";
                                        }

                                        WorkerList.Add(sessWorkerName);
                                    }
                                }
                                else
                                {
                                    WorkerList.Add("no data found");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            WorkerList.Add("no data found");
                        }

                        AddWorkerName.ItemsSource = WorkerList;
                    }
                    else if (strAddWorkerPool == "SiaMining")
                    {
                        try
                        {
                            string url = @"https://siamining.com/api/v1/addresses/" + strAddWorkerWalletAddress + "/workers";

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                            request.AutomaticDecompression = DecompressionMethods.GZip;

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (Stream stream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                initialWorkerInfo = reader.ReadToEnd();
                                if (initialWorkerInfo.Contains("\"interval\": 300"))
                                {
                                    WorkerInfoA = Regex.Split(initialWorkerInfo, "]\n    },");
                                    SepWorkerInfoA = WorkerInfoA.OfType<string>().ToList();
                                    for (int x = 0; x < SepWorkerInfoA.Count; x++)
                                    {
                                        initialWorker = SepWorkerInfoA[x];
                                        RegexSplitWorkerInfo = Regex.Split(initialWorker, ",");
                                        sessWorkerName = References.EaseMethods.getBetween(RegexSplitWorkerInfo[0], "\"name\": \"", "\"");
                                        if (sessWorkerName.Length == 0)
                                        {
                                            sessWorkerName = "unknown";
                                        }

                                        WorkerList.Add(sessWorkerName);
                                    }
                                }
                                else
                                {
                                    WorkerList.Add("no data found");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            WorkerList.Add("no data found");
                        }

                        AddWorkerName.ItemsSource = WorkerList;
                    }
                });

            });
        }

        private void AddWorkerName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddWorkerWalletCustomID.Text.Length != 0 && LoggedIn == true && AddWorkerPool.Text.Length != 0 && WalletSelected == false)
            {
                WalletSelected = true;
                WorkerList.Clear();
                strAddWorkerWalletID = AddWorkerWalletCustomID.Text;
                int Index = SelectMiningSettingsForUser(WalletCustomIDs).IndexOf(strAddWorkerWalletID);
                strAddWorkerWalletAddress = WalletCustomAddresses.ElementAt(Index);
                strAddWorkerPool = AddWorkerPool.Text;

                LookUpWalletAddress();

                AddWorkerWalletCustomID.Text = strAddWorkerWalletID;
            }
            else if (WalletSelected == true && LoggedIn == true)
            {
                if (AddWorkerWalletCustomID.Text != strAddWorkerWalletID || AddWorkerPool.Text != strAddWorkerPool)
                {
                    WorkerList.Clear();
                    strAddWorkerWalletID = AddWorkerWalletCustomID.Text;
                    int Index = SelectMiningSettingsForUser(WalletCustomIDs).IndexOf(strAddWorkerWalletID);
                    strAddWorkerWalletAddress = WalletCustomAddresses.ElementAt(Index);
                    strAddWorkerPool = AddWorkerPool.Text;

                    LookUpWalletAddress();

                    AddWorkerWalletCustomID.Text = strAddWorkerWalletID;
                }

            }
            else
            {
                WorkerList.Clear();
                WorkerList.Add("no data found");
                AddWorkerName.ItemsSource = WorkerList;
            }
        }

        private void WorkerComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WorkerCustomList.Count == 0 || LoggedIn == false)
            {
                WorkerCustomList.Clear();
                WorkerCustomList.Add("no data found");
                WorkerComboBox.ItemsSource = WorkerCustomList;
            }
            else
            {
                WorkerComboBox.ItemsSource = WorkerCustomList;
            }
        }

        private void AddWorkerWalletCustomID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (WalletCustomIDs.Count == 0)
            {
                WalletCustomIDs.Add("no data found");
            }
        }
    }
}
