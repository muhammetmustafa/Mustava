using System;
using System.Collections.Generic;

namespace Mustava.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWeeks(this DateTime dateTime, int weeks)
        {
            return dateTime.AddDays(7*weeks);
        }

        public static bool IsMinOrMax(this DateTime dateTime)
        {
            return dateTime.Equals(DateTime.MinValue) || dateTime.Equals(DateTime.MaxValue);
        }

        /// <summary>
        /// Şimdiki tarihden itibaren geçmişteki yılları int listesi olarak döndürür.
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="howManyYearsBack"> Listedeki geçmiş yıl sayısı </param>
        /// <param name="howManyYearsFuture">Listedeki gelecek yıl sayısı </param>
        /// <returns></returns>
        public static List<int> GetYearsBackFromNow(this DateTime dateTime, int howManyYearsBack, int howManyYearsFuture = 0)
        {
            var yearList = new List<int>();
            var backDate = dateTime.AddYears(-howManyYearsBack);
            var futureDate = dateTime.AddYears(howManyYearsFuture);

            for (var year = backDate.Year; year <= futureDate.Year; year++)
            {
                yearList.Add(year);
            }

            return yearList;
        }

        public static DateTime? ExToDateTimeNullable(this DateTime dateTime)
        {
            if (dateTime.IsMinOrMax())
                return null;

            return dateTime;
        }
        
        public static DateTime? ExToDateTimeNullable(this object value)
        {
            var result = default(DateTime);
            if (value == null) 
                return result;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;
            return default(DateTime?);
        }

        /// <summary>
        /// 2017-10-12 18:08:34.3434 ===> 2017-10-12 00:00:00.0000
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ResetHourToZero(this DateTime dateTime)
        {
            var tmp = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                0, 0, 0, 0);

            return tmp;
        }

        /// <summary>
        /// 2017-10-12 18:08:34.3434 ===> 2017-10-12 23:59:59.9999
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ResetHourToMidnight(this DateTime dateTime)
        {
            var tmp = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                23, 59, 59, 999);

            return tmp;
        }
    }
}