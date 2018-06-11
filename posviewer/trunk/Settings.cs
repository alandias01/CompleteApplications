using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maple.Dtc
{
    public static class Settings
    {
        public static string Account;
        public static string BookName;
        public static string DatabaseName;
        
    }


    public static class MyExtensions
    {
        public static string Padded(this String str)
        {
            return str.PadLeft(8, '0');
        }

        public static int ToInt(this String str)
        {
            return int.Parse(str);
        }
    }   
}
