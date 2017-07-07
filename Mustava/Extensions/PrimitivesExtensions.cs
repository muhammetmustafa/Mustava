namespace Mustava.Extensions
{
    public static class PrimitivesExtensions
    {
        public static float toFloat(this int i)
        {
            return i;
        }

        public static bool IsEmpty(this string s)
        {
            return s == null || s.Equals(string.Empty);
        }

        public static bool IsNotEmpty(this string s)
        {
            return !s.IsEmpty();
        }

        public static string ToClockFormat(this int i)
        {
            return string.Format("{0}:{1}", (i / 60).ToString("D2"), (i % 60).ToString("D2"));
        }
    }
}