using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPocket
{
    class EaseMethods
    {
        public static string jsonString;

        public static string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890.,-";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }

        }
        public static string RemoveonlyCurly(string value)
        {
            var allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890,:[].\"";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }
        }
        public static string RemoveforMining(string value)
        {
            var allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }
        }
        public static string RemoveforMiningKeepingCurly(string value)
        {
            var allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890{}";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }
        }
        public static string RemoveCommas(string value)
        {
            var allowedChars = "1234567890.";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }
        }

        public static string RemoveAfterLetter(string Remove, string Letter)
        {
            Worker.TimeIndexRemove = Remove.LastIndexOf(Letter);
            if (Worker.TimeIndexRemove > 0) { Remove = Remove.Substring(0, Worker.TimeIndexRemove); }
            return Remove;
        }

        public static string API(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonString = reader.ReadToEnd();
                    return jsonString;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getBetween(string strSource, string strStart, string strEnd)
        {
            try
            {
                int Start, End;
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End - Start);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }

    public class EaseOptionsMethods : Options
    {
        //EASE OF ACCESS
        //HIDE ALL LBL EXCEPT SAVE FOUND
        public void HideConfirmationLabelsSave()
        {
            lblMaxPages.Visible = false;
            lblConfirmed.Visible = false;
            lblSaved.Visible = false;
            lblNewPage.Visible = false;
            lblNoSave.Visible = false;
            lblSaveFound.Visible = true;
        }
        //HIDE ALL LBL EXCEPT NO SAVE FOUND
        public void HideConfirmationLabelsNoSave()
        {
            lblConfirmed.Visible = false;
            lblSaved.Visible = false;
            lblNewPage.Visible = false;
            lblSaveFound.Visible = false;
            lblMaxPages.Visible = false;
            lblNoSave.Visible = true;
        }
        //HIDE ALL LBL EXCEPT NEW PAGE CREATED
        public void HideConfirmationLabelsNewSave()
        {
            lblMaxPages.Visible = false;
            lblConfirmed.Visible = false;
            lblSaved.Visible = false;
            lblNoSave.Visible = false;
            lblSaveFound.Visible = false;
            lblNewPage.Visible = true;
        }
    }
}
