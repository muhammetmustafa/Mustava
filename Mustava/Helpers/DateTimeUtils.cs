using System;
using Mustava.Extensions;

namespace Mustava.Helpers
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
            return string.Format("{0:D2}:{1:D2}", (minutes/60), (minutes%60));
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