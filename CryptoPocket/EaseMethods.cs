using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPocket
{
    public class EaseMethods
    {
        //Variety of Functions to Strip Text / Numbers / Symbols
        public static string KeepOnlyNumbers(string value)
        {
            var allowedChars = "01234567890";
            try
            {
                return new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            }
            catch (Exception)
            {
                return value;
            }

        }
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
        public static string KeepNumbersAndDots(string value)
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
        public static string RemoveKeepingColumnAndDots(string value)
        {
            var allowedChars = "1234567890:.";
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
            MainWindow.TimeIndexRemove = Remove.LastIndexOf(Letter);
            if (MainWindow.TimeIndexRemove > 0) { Remove = Remove.Substring(0, MainWindow.TimeIndexRemove); }
            return Remove;
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
}
