using System;

namespace FullStackChallenge.Infra.Utils
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string stringToConvert)
        {
            return Char.ToLowerInvariant(stringToConvert[0]) + stringToConvert.Substring(1);
        }
    }
}