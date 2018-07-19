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

namespace CryptoPocket
{

    public partial class MainWindow : Window
    {

        public class CustomCoinBox
        {
            public string eg { get; set; }
        }

        public class CustomWorkerBox
        {
            public string eg { get; set; }
        }

        MainWindow mw = (MainWindow)Application.Current.MainWindow;


        private MySqlConnection connection;
        bool CoinToggled = false;
        bool WorkerToggled = false;
        public bool AlwaysOnTop = false;

        public static bool LoggedIn = false;
        public static bool DefaultLogin = true;
        public static string PreviousUsername;

        public static int CurrentID = 0;

        Color HoverColour = (Color)ColorConverter.ConvertFromString("#FFFFCC33");
        Color UnhoverColour = (Color)ColorConverter.ConvertFromString("#FFFEC007");

        public MainWindow()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
            HeaderTabs.SelectedIndex = 0;

            //string connectionstring2 = "SERVER=27.121.66.21;DATABASE=goldli00_CryptoPocket;UID=goldli00_admin;PWD=passCrypto123;";

            string DatabaseConnectionString = Properties.Settings.Default.ConnectionString;
            connection = new MySqlConnection(DatabaseConnectionString);
            
        }

        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        private void ThemeChecked(object sender, RoutedEventArgs e)
        {
            if (AccountSettings.Theme == false)
            {
                this.Background = new SolidColorBrush(Colors.Gray);
                AccountSettings.Theme = true;
            }
            else if (AccountSettings.Theme == true)
            {
                this.Background = new SolidColorBrush(Colors.WhiteSmoke);
                AccountSettings.Theme = false;
            }
        }

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
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;

                    case 1130:
                        Console.WriteLine("Not allowed to connected to server.");
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Select statement
        public List<string> Select(List<string> Ref)
        {
            string query = "SELECT * FROM CryptoCustomCoins";

            MySQLReference.CCID.Clear();
            MySQLReference.CCoin.Clear();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    MySQLReference.CCID.Add(dataReader["ID"] + "");
                    MySQLReference.CCoin.Add(dataReader["COIN"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return Ref;
            }
            else
            {
                return Ref;
            }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

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

        private void btnCustomCoin_Click(object sender, RoutedEventArgs e)
        {
            CoinBox.Items.Add(new CustomCoinBox() { });
        }

        private void btnTradingAddCoin_Click(object sender, RoutedEventArgs e)
        {
            CoinBox.Items.Add(new CustomCoinBox() { });
        }

        private void btnTradingRemoveCoin_Click(object sender, RoutedEventArgs e)
        {
            CoinBox.Items.Add(new CustomCoinBox() { });
        }

        private void btnCustomWorker_Click(object sender, RoutedEventArgs e)
        {
            WorkerBox.Items.Add(new CustomWorkerBox() { });
        }

        private void btnMiningAddWorker_Click(object sender, RoutedEventArgs e)
        {
            CoinBox.Items.Add(new CustomCoinBox() { });
        }

        private void btnMiningRemoveWorker_Click(object sender, RoutedEventArgs e)
        {
            CoinBox.Items.Add(new CustomCoinBox() { });
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
                    tempPortfolioCoin.Opacity = 0;
                    tempPortfolioCoinV.Opacity = 0;
                    temptxt24Hours.Opacity = 100;
                    temptxt7Days.Opacity = 100;
                    temptxt24HoursV.Opacity = 100;
                    temptxt7DaysV.Opacity = 100;
                    tempIcon24Hours.Opacity = 100;
                    tempIcon7Days.Opacity = 100;
                    CoinToggled = true;
                    tempToggleCoinText.Text = "Recent Change";
                }
                else if (CoinToggled == true)
                {
                    tempPortfolioCoin.Opacity = 100;
                    tempPortfolioCoinV.Opacity = 100;
                    temptxt24Hours.Opacity = 0;
                    temptxt7Days.Opacity = 0;
                    temptxt24HoursV.Opacity = 0;
                    temptxt7DaysV.Opacity = 0;
                    tempIcon24Hours.Opacity = 0;
                    tempIcon7Days.Opacity = 0;
                    CoinToggled = false;
                    tempToggleCoinText.Text = "Portfolio Value";
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

        private void Settings_Selected(object sender, RoutedEventArgs e)
        {
            //ListBoxItem lbi = e.Source as ListBoxItem;
            //if(lbi.IsSelected == true)
            //{
            //    AccountSettings.Visibility = Visibility.Visible;
            //    lbi.IsSelected = false;
            //}
            //else
            //{
            //    AccountSettings.Visibility = Visibility.Hidden;
            //}
        }

        void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AccountSettings.Visibility = Visibility.Visible;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            AccountSettings.Visibility = Visibility.Visible;
        }

        private void Header_Click(object sender, MouseButtonEventArgs e)
        {
            AccountSettings.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (LoggedIn == true)
            {
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", HeaderUser.Text);
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
            if (!File.Exists(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt"))
            {
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session");
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Documents\CryptoPocket\Session\Username.txt", "");
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
    }
}
