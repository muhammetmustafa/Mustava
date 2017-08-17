namespace Mustava.Extensions
{
    public static class PrimitivesExtensions
    {
        public static bool ExIsEmpty(this string s)
        {
            return s == null || s.Equals(string.Empty);
        }

        public static bool ExIsNotEmpty(this string s)
        {
            return !s.ExIsEmpty();
        }

        public static string ToClockFormat(this int i)
        {
            return string.Format("{0:D2}:{1:D2}", (i / 60), (i % 60));
        }
    }
}