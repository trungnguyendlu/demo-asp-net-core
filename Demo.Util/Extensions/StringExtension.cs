using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Util
{
    public static class StringExtension
    {
        public static string FormatPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }

            phoneNumber = phoneNumber
                .Replace("(", "")
                .Replace(")", "")
                .Replace(".", "")
                .Replace("-", "")
                .Replace("_", "")
                .Replace(" ", "");

            if (phoneNumber.Length == 10)
            {
                phoneNumber = phoneNumber.Insert(6, "-").Insert(3, "-");
            }
            return phoneNumber;
        }

        public static string ToUrl(this string input, string themeUrl, Func<string, string> func)
        {
            return input.ToLowerInvariant().StartsWith("http") ? input : func($"{themeUrl}/{input}");
        }

        public static List<string> ToFiles(this string input, string[] separators = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<string>();
            }
            return input.Split(separators ??  new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string CombineFiles(this List<string> files, string separator = null)
        {
            if (files == null || files.Count == 0)
            {
                return string.Empty;
            }
            return string.Join(separator ?? Environment.NewLine, files);
        }

        public static string TagReplace(this string input, string tag, string content, bool allowReplace = true)
        {
            if (allowReplace)
            {
                return input.Replace(tag, content, StringComparison.OrdinalIgnoreCase);
            }
            return input;
        }

        public static string Truncate(this string input, int length, string escape = "...")
        {
            if (length <= 0 || string.IsNullOrEmpty(input) || input.Length < length)
            {
                return input;
            }
            return $"{input.Substring(0, length)} {escape}";
        }
    }
}