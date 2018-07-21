using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPocket
{
    class Trading
    {
        public static void GETINFO(string CRYPTO, string customCoin)
        {
            if (PublicCurrencyIndex == 0)       //0 is the index for USD.
            {
                CURRENCY = "USD";
            }
            else if (PublicCurrencyIndex == 1)      //1 is the index for AUD.
            {
                CURRENCY = "AUD";
            }

            if (CRYPTO == "" || customCoin == "")       //If the coin is empty set labels to NO DATA.
            {
                if (PublicCurrencyIndex == 0)
                {
                    xC.Text = "XXX/USD";
                }
                else if (PublicCurrencyIndex == 1)
                {
                    xC.Text = "XXX/AUD";
                }
                xBTC.Text = "XXX/BTC";
                xCv.Text = "No Data";
                xBTCv.Text = "No Data";
                xUSDp.Text = "No Data";
                xUSD24p.Text = "No Data";
                xUSD7p.Text = "No Data";
                xUSDc.Text = "";
                xUSD24c.Text = "";
                xUSD7c.Text = "";
                Number.Text = "      " + "XXX";
                xTimev.Text = "No Data";

                //Reset Label Forcolor to Black.
                xCv.ForeColor = Color.Black;
                xBTCv.ForeColor = Color.Black;
                xUSDp.ForeColor = Color.Black;
                xUSD24p.ForeColor = Color.Black;
                xUSD7p.ForeColor = Color.Black;
            }
            else
            {
                //Deserialize API into jsonString.


                //Set CoinsDetailed to the new jsonString from above API.
                CoinsDetailed = JsonConvert.DeserializeObject<List<MarketCap>>(EaseMethods.API(@"https://api.coinmarketcap.com/v1/ticker/" + CRYPTO + @"/?convert=" + CURRENCY));

                //Default Labelling based on first data.
                xBTC.Text = customCoin + "/BTC";
                Number.Text = "";
                Number.Text = "      " + customCoin;

                //Calculating Coin Price Against USD or AUD.
                if (PublicCurrencyIndex == 0)
                {
                    xC.Text = customCoin + "/USD";
                    foreach (var data in CoinsDetailed)             //Coin Against USD
                    {
                        string price_usd;
                        string percent;
                        decimal Dpercent = 0;
                        decimal newprice;
                        price_usd = data.price_usd;                   //Set varaible price usd to the value found from the CoinsDetailed data.
                        price_usd = EaseMethods.RemoveExtraText(price_usd);
                        newprice = Convert.ToDecimal(price_usd);      //Converting to decimal for more accurate financial calculations.
                        if (CRYPTO == "bitcoin")
                        {
                            Crypto.UniversalBTCPrice = Convert.ToDouble(newprice);     //Setting Universal BTC Price for Mining Page.                                                    
                        }
                        price_usd = string.Format("{0:#,0.00##}", newprice);    //Format the price to include commas and up to 4 decimal points.
                        price_usd = "$" + price_usd;
                        percent = data.percent_change_1h;
                        CalculateCoinAgainstFiatPercentage(price_usd, percent, Dpercent, xCv);          //Checking if it is increasing or decreasing for the triangle arrow and forecolor.
                        break;
                    }
                }
                else if (PublicCurrencyIndex == 1)
                {
                    xC.Text = customCoin + "/AUD";
                    foreach (var data in CoinsDetailed)             //Coin Against AUD
                    {
                        string price_aud;
                        string percent;
                        decimal Dpercent = 0;
                        decimal newprice;
                        price_aud = data.price_aud;                 //Set varaible price usd to the value found from the CoinsDetailed data.
                        price_aud = EaseMethods.RemoveExtraText(price_aud);
                        newprice = Convert.ToDecimal(price_aud);    //Converting to decimal for more accurate financial calculations.
                        price_aud = string.Format("{0:#,0.00##}", newprice);    //Format the price to include commas and up to 4 decimal points.
                        price_aud = "$" + price_aud;
                        percent = data.percent_change_1h;
                        CalculateCoinAgainstFiatPercentage(price_aud, percent, Dpercent, xCv);      //Checking if it is increasing or decreasing for the triangle arrow and forecolor.
                        break;
                    }
                }
                foreach (var data in CoinsDetailed)         //Calculate Last Updated Time.
                {
                    string Time;
                    string FinalTime;
                    double DTime;
                    Time = data.last_updated;
                    Time = EaseMethods.RemoveExtraText(Time);
                    DTime = Convert.ToDouble(Time);
                    DateTime UniversalTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(DTime);        //Converting UNIX time to DateTime.
                    DateTime LocalTime = UniversalTime.ToLocalTime();                                       //Converting DateTime to LocatTime.
                    if (TimeZoneNumber == 0)
                    {
                        FinalTime = UniversalTime.ToString("r");            //'r' is a format of DateTime display.
                        FinalTime = FinalTime.Substring(0, FinalTime.Length - 4);   //Remove Extra Text Caused by 'r'.
                        xTimev.Text = FinalTime + " UTC";
                    }
                    else if (TimeZoneNumber == 1)
                    {
                        FinalTime = LocalTime.ToString("r");                //'r' is a format of DateTime display.
                        FinalTime = FinalTime.Substring(0, FinalTime.Length - 4); //Remove Extra Text Caused by 'r'.
                        xTimev.Text = FinalTime + " LOCAL";
                    }
                    break;
                }
                foreach (var data in CoinsDetailed)         //Coin Against BTC
                {
                    string percent_change_1h;
                    decimal Dchange;
                    percent_change_1h = data.percent_change_1h;
                    percent_change_1h = EaseMethods.RemoveExtraText(percent_change_1h);
                    Dchange = Convert.ToDecimal(percent_change_1h);
                    percent_change_1h = percent_change_1h + "%";
                    if (Dchange > 0)                                                //No Change is seen when BTC is compared against BTC.
                    {
                        if (data.symbol == "BTC") { xBTCv.ForeColor = Color.Black; }
                        else { xBTCv.ForeColor = Color.Green; }
                    }
                    else if (Dchange < 0)
                    {
                        if (data.symbol == "BTC") { xBTCv.ForeColor = Color.Black; }
                        else { xBTCv.ForeColor = Color.Red; }
                    }
                    else if (Dchange == 0)
                    {
                        if (data.symbol == "BTC") { xBTCv.ForeColor = Color.Black; }
                        else { xBTCv.ForeColor = Color.Black; }
                    }
                    string price_btc;
                    decimal dprice_btc;
                    price_btc = data.price_btc;
                    price_btc = EaseMethods.RemoveExtraText(price_btc);
                    dprice_btc = Convert.ToDecimal(price_btc);
                    if (data.symbol == "BTC")
                    { price_btc = String.Format("{0:0.0}", dprice_btc); }
                    else
                    { price_btc = String.Format("{0:0.00000000}", dprice_btc); }
                    price_btc = price_btc + " BTC";
                    xBTCv.Text = price_btc;
                    break;
                }
                foreach (var data in CoinsDetailed)         //Calculating all things related to percentages for ANY COIN.
                {
                    string value_changed = null;
                    string convertedPercent = null;
                    string StartingPercent;
                    string price_usd;
                    decimal Dprice_usd = 0;
                    string price_aud;
                    decimal Dprice_aud = 0;
                    decimal DChange = 0;
                    string price_btc;
                    decimal Dprice_btc = 0;

                    double Dchange = 0;
                    string Data = null;
                    string StartingPercentP;

                    price_usd = data.price_usd;
                    price_aud = data.price_aud;
                    price_btc = data.price_btc;

                    StartingPercent = data.percent_change_1h;
                    CalculatePercentageValue(value_changed, convertedPercent, StartingPercent, price_usd, Dprice_usd, price_aud, Dprice_aud, price_btc, Dprice_btc, DChange, xUSDc);
                    StartingPercent = data.percent_change_24h;
                    CalculatePercentageValue(value_changed, convertedPercent, StartingPercent, price_usd, Dprice_usd, price_aud, Dprice_aud, price_btc, Dprice_btc, DChange, xUSD24c);
                    StartingPercent = data.percent_change_7d;
                    CalculatePercentageValue(value_changed, convertedPercent, StartingPercent, price_usd, Dprice_usd, price_aud, Dprice_aud, price_btc, Dprice_btc, DChange, xUSD7c);

                    StartingPercentP = data.percent_change_1h;
                    CalculatePercentage(Dchange, Data, StartingPercentP, xUSDp);
                    StartingPercentP = data.percent_change_24h;
                    CalculatePercentage(Dchange, Data, StartingPercentP, xUSD24p);
                    StartingPercentP = data.percent_change_7d;
                    CalculatePercentage(Dchange, Data, StartingPercentP, xUSD7p);
                    break;
                }
            }

        }
        //Calculating ANY COIN Against FIAT Currency.
        public static void CalculateCoinAgainstFiatPercentage(string price_currency, string percent, decimal Dpercent, Label xCv)
        {
            percent = EaseMethods.RemoveExtraText(percent);
            Dpercent = Convert.ToDecimal(percent);
            if (Dpercent > 0)
            {
                price_currency = "▲ " + price_currency;
                xCv.ForeColor = Color.Green;
            }
            else if (Dpercent < 0)
            {
                price_currency = "▼ " + price_currency;
                xCv.ForeColor = Color.Red;
            }
            else if (Dpercent == 0)
            {
                xCv.ForeColor = Color.Black;
            }
            xCv.Text = price_currency;
        }
        //Calculating Percentage VALUE Change (1Hour 24Hour 7Days).
        public static void CalculatePercentageValue(string value_changed, string convertedPercent, string StartingPercent, string price_usd, decimal Dprice_usd, string price_aud, decimal Dprice_aud, string price_btc, decimal Dprice_btc, decimal DChange, Label ValueText)
        {
            if (StartingPercent == null)
            {
                ValueText.Text = "";
            }
            else
            {
                convertedPercent = EaseMethods.RemoveExtraText(StartingPercent);        //Remove (" ").
                if (CURRENCY == "USD" && TimePeriodNumber == 0)
                {
                    CalculatePercentageValueDetailed(value_changed, price_usd, convertedPercent, Dprice_usd, DChange, ValueText);
                }
                else if (CURRENCY == "USD" && TimePeriodNumber == 2)
                {
                    CalculatePercentageValueDetailed(value_changed, price_btc, convertedPercent, Dprice_btc, DChange, ValueText);
                }
                else if (CURRENCY == "AUD" && TimePeriodNumber == 1)
                {
                    CalculatePercentageValueDetailed(value_changed, price_aud, convertedPercent, Dprice_aud, DChange, ValueText);
                }
                else if (CURRENCY == "AUD" && TimePeriodNumber == 2)
                {
                    CalculatePercentageValueDetailed(value_changed, price_btc, convertedPercent, Dprice_btc, DChange, ValueText);
                }
            }
        }
        public static void CalculatePercentageValueDetailed(string value_changed, string price, string convertedPercent, decimal Dprice, decimal DChange, Label ValueText)
        {
            //Percentage Value Calculation Logic.
            price = EaseMethods.RemoveExtraText(price);
            decimal totalPercent;
            totalPercent = Convert.ToDecimal(convertedPercent);     //Convert Percentage to a Decimal.
            totalPercent = totalPercent / 100;                      //Divide percentage by 100 to get actual percentage change. Eg: 5% = 0.05%
            Dprice = Convert.ToDecimal(price);                      //Decimal Value of the price taken from CoinsDetailed.
            DChange = Dprice * totalPercent;                        //Represents the Change in Value.

            //Confirms Correct Color Label is Displayed.
            if (DChange < 0)
            {
                DChange = DChange * -1;
                if (DChange == 0)
                {
                    ValueText.ForeColor = Color.Black;
                }
                else
                {
                    ValueText.ForeColor = Color.Red;
                    value_changed = string.Format("{0:#,0.00000000}", DChange);    //Format the price to include commas and up to 4 decimal points.
                    value_changed = value_changed.Substring(0, 8);
                }
            }
            else if (DChange > 0)
            {
                if (DChange == 0)
                {
                    ValueText.ForeColor = Color.Black;
                }
                else
                {
                    ValueText.ForeColor = Color.Green;
                    value_changed = string.Format("{0:#,0.00000000}", DChange);    //Format the price to include commas and up to 4 decimal points.
                    value_changed = value_changed.Substring(0, 8);
                }

            }
            //Sets Label to the Calculated Figures.
            value_changed = "$" + value_changed;
            ValueText.Text = value_changed;
        }
        //Calculating Readable Percentage Values.
        public static void CalculatePercentage(double Dchange, string Data, string StartingPercentP, Label PercentText)
        {
            if (StartingPercentP == null)
            {
                PercentText.Text = "No Data";
                PercentText.ForeColor = Color.Black;
            }
            else
            {
                Data = EaseMethods.RemoveExtraText(StartingPercentP);
                Dchange = Convert.ToDouble(Data);
                Data = Data + "%";
                //Making Sure the Labels are Colored Correctly.
                if (Dchange > 0)
                {
                    PercentText.ForeColor = Color.Green;
                }
                else if (Dchange < 0)
                {
                    PercentText.ForeColor = Color.Red;
                }
                else if (Dchange == 0)
                {
                    PercentText.ForeColor = Color.Black;
                }
                PercentText.Text = Data;
            }
        }
    }
}
