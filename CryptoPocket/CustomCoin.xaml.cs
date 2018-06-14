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

namespace CryptoPocket
{
    /// <summary>
    /// Interaction logic for CustomCoin.xaml
    /// </summary>
    public partial class CustomCoin : UserControl
    {
        public CustomCoin()
        {
            InitializeComponent();
            PortfolioCoin.Opacity = 100;
            PortfolioCoinV.Opacity = 100;
            txt24Hours.Opacity = 0;
            txt7Days.Opacity = 0;
            txt24HoursV.Opacity = 0;
            txt7DaysV.Opacity = 0;
            Icon24Hours.Opacity = 0;
            Icon7Days.Opacity = 0;
            ToggleCoinText.Text = "Portfolio Value";
        }


        bool Toggled = false;

        private void CustomCoinToggle_Click(object sender, RoutedEventArgs e)
        {
            if (Toggled == false)
            {
                PortfolioCoin.Opacity = 0;
                PortfolioCoinV.Opacity = 0;
                txt24Hours.Opacity = 100;
                txt7Days.Opacity = 100;
                txt24HoursV.Opacity = 100;
                txt7DaysV.Opacity = 100;
                Icon24Hours.Opacity = 100;
                Icon7Days.Opacity = 100;
                Toggled = true;
                ToggleCoinText.Text = "Recent Change";
            }
            else if (Toggled == true)
            {
                PortfolioCoin.Opacity = 100;
                PortfolioCoinV.Opacity = 100;
                txt24Hours.Opacity = 0;
                txt7Days.Opacity = 0;
                txt24HoursV.Opacity = 0;
                txt7DaysV.Opacity = 0;
                Icon24Hours.Opacity = 0;
                Icon7Days.Opacity = 0;
                Toggled = false;
                ToggleCoinText.Text = "Portfolio Value";
            }
        }
    }
}
