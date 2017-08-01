using System;
using Mustava.Extensions;

namespace Mustava.Helper
{
    public static class DateTimeUtils
    {
        public static bool CheckForOverlap(DateTime startStart, DateTime startEnd, DateTime endStart, DateTime endEnd)
        {
            if (startEnd < startStart)
                return false;

            if (endEnd < endStart)
                return false;

            if (startStart > endStart)
                return false;

            return startStart <= endEnd && endStart >= startEnd;
        }

        public static string MinuteToHour(int minutes)
        {
            return string.Format("{0}:{1}", (minutes/60).ToString("D2"), (minutes%60).ToString("D2"));
        }

        public static int HourToMinute(string hour)
        {
            if (hour.IsNullOrEmpty())
                return 0;

            var saatler = hour.Split(':');
            if (saatler.Length < 2)
                return 0;

            return saatler[0].ToInt() * 60 + saatler[1].ToInt();
        }
    }
}