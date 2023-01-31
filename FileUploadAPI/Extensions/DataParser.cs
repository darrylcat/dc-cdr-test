using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Extensions
{
    public static class DataParser
    {
        private static CultureInfo UK_CULTURE = CultureInfo.GetCultureInfo("en-GB");

        public static DateTime ShortDate(this string data)
        {
            return DateTime.Parse(data);
        }

        public static DateTime ShortTime(this string data)
        {
            string Data = $"01/01/0001 {data}";
            return DateTime.ParseExact(Data, "dd/MM/yyyy HH:mm:ss", UK_CULTURE);
        }

        public static Decimal ThreeDecimalPlaces(this string data)
        {
            var setPrecision = new NumberFormatInfo();
            setPrecision.NumberDecimalDigits = 3;
            var value = Decimal.Parse(data);
            return value;
        }
    }
}
