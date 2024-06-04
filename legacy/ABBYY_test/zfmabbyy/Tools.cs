using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zfmabbyy
{
    public static class Tools
    {

        static void Main(string[] args)
        {
            Console.WriteLine(Tools.formatDate("3/12/20"));
            Console.ReadKey();
        }
        public static string formatDate(string date)
        {
            string[] words = date.Split(new char[] { ' ', '/', ',', '.'});
            string ans = "";

            if (words[0].Length == 4) swap(ref words[0], ref words[2]);
            if (words[2].Length == 2) words[2] = "20" + words[2];
            if (words[0].Length < 2) words[0] = "0" + words[0];

            if (date.IndexOf(" ") > -1)
            {
                if (words[1] == "января" || words[1] == "Jan" || words[1] == "jan" || words[1] == "January")
                {
                    ans = words[2] + "-01-" + words[0];
                }
                if (words[1] == "февраля" || words[1] == "Feb" || words[1] == "feb" || words[1] == "February")
                {
                    ans = words[2] + "-02-" + words[0];
                }
                if (words[1] == "марта" || words[1] == "Mar" || words[1] == "mar" || words[1] == "March")
                {
                    ans = words[2] + "-03-" + words[0];
                }
                if (words[1] == "апреля" || words[1] == "Apr" || words[1] == "apr" || words[1] == "April")
                {
                    ans = words[2] + "-04-" + words[0];
                }
                if (words[1] == "мая" || words[1] == "May" || words[1] == "may")
                {
                    ans = words[2] + "-05-" + words[0];
                }
                if (words[1] == "июня" || words[1] == "Jun" || words[1] == "jun" || words[1] == "June")
                {
                    ans = words[2] + "-06-" + words[0];
                }
                if (words[1] == "июля" || words[1] == "Jul" || words[1] == "jul" || words[1] == "July")
                {
                    ans = words[2] + "-07-" + words[0];
                }
                if (words[1] == "августа" || words[1] == "Aug" || words[1] == "aug" || words[1] == "August")
                {
                    ans = words[2] + "-08-" + words[0];
                }
                if (words[1] == "сентября" || words[1] == "Sep" || words[1] == "sep" || words[1] == "September")
                {
                    ans = words[2] + "-09-" + words[0];
                }
                if (words[1] == "октября" || words[1] == "Oct" || words[1] == "oct" || words[1] == "October")
                {
                    ans = words[2] + "-10-" + words[0];
                }
                if (words[1] == "ноября" || words[1] == "Nov" || words[1] == "nov" || words[1] == "November")
                {
                    ans = words[2] + "-11-" + words[0];
                }
                if (words[1] == "декабря" || words[1] == "Dec" || words[1] == "dec" || words[1] == "December")
                {
                    ans = words[2] + "-12-" + words[0];
                }
            }
            else
            {
                ans = words[2] + "-" + words[1] + "-" + words[0];
            }
            return ans;
        }
        static void swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
