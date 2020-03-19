using System;
using System.Linq;

namespace Demo.Util
{
    public static class StringGenerator
    {
        private static readonly Random Random = new Random();

        public static string Generate(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GenerateOrderNumber()
        {
            return $"{DateTime.Now.ToString("yy")}{Generate(6)}";
        }
    }
}
