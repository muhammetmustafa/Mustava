using System;

namespace Mustava.Helper
{
    internal class Randomizer
    {
        private static int seed = 1;
        private const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string randomString(int minLength = 3, int maxLength = 10)
        {
            Random rng = new Random(seed);

            char[] chars = new char[maxLength];
            int setLength = allowedChars.Length;

            int length = rng.Next(minLength, maxLength + 1);

            for (int i = 0; i < length; ++i)
            {
                chars[i] = allowedChars[rng.Next(setLength)];
            }

            seed++;

            return new string(chars, 0, length);
        }

        public static int randomListMember(int listCount)
        {
            Random rng = new Random(seed);

            seed++;

            return rng.Next(0, listCount - 1);
        }

        public static int randomHour(int bas = 0, int son = 23)
        {
            Random rng = new Random(seed);

            seed++;

            return rng.Next(bas, son);
        }

        public static int randomMinute(int bas = 0, int son = 59)
        {
            Random rng = new Random(seed);

            seed++;

            return rng.Next(bas, son);
        }

        public static bool randomBool()
        {
            Random rng = new Random(seed);

            seed++;

            return rng.Next(-seed, seed) > 0;
        }
        public static Color randomColor()
        {
            return Color.FromArgb(randomHour(0, 255), randomHour(0, 255), randomHour(0, 255));
        }
    }
}