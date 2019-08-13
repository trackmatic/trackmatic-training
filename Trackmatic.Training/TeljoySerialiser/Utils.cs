using System;
using System.Text.RegularExpressions;
using TeljoySerialiser.Model;
using Action = TeljoySerialiser.Model.Action;

namespace TeljoySerialiser
{
    public static class Utils
    {
        public static string RemoveIllegalChars(string inputString)
        {
            var reg = new Regex("[+!%-/]");
            var result = reg.Replace(inputString, "_");
            return result;
        }

        public static string RemoveIllegalCharsFromTags(string inputString)
        {
            var reg = new Regex(@"\n\s+");
            return reg.Replace(inputString, "");
        }

        public static TimeSpan DetermineMst(Action action)
        {
            var mst = Convert.ToDouble(action.Properties.FixedMst);
            return TimeSpan.FromMinutes(mst);
        }

        public static decimal CheckDecimalEmpty(string value)
        {
            decimal result = 0.0m;
            var formattedString = RemoveIllegalCharsFromTags(value);
            if (string.IsNullOrEmpty(formattedString))
            {
                return result;
            }
            else
            {
                decimal.TryParse(value, out result);
                return result;
            }
        }

        public static int CheckIntEmpty(string value)
        {
            int result = 0;
            var formattedString = RemoveIllegalCharsFromTags(value);
            if (string.IsNullOrEmpty(value))
            {
                return result;
            }
            else
            {
                Int32.TryParse(value, out result);
                return result;
            }
        }
    }
}
