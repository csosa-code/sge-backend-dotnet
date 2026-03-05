using System.Text.RegularExpressions;
using System;
using System.Text.Json;
using System.Text;
using System.Globalization;

namespace Sge.Enterprise.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] Prepositions =
        [
            "de", "la", "el", "los", "las", "en", "a", "con", "por", "para"
        ];

        public static string TaxNumberConvert(this String str)
        {
            return str.Insert(str.Length - 1, "-");
        }

        public static string NormalizeIdentification(this String str)
        {
            return str.Replace("DPI", "").Replace(" ", "").Trim();
        }

        public static string Sanitization(this String str)
        {
            string data = str.Trim();
            return Regex.Replace(data, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);

        }

        public static string ConvertToJsonString(this String str)
        {
            string data = str.Trim();
            var firstChar = data.Substring(0, 1);
            string result;

            if (firstChar == "[")
            {
                JsonDocument doc = JsonDocument.Parse(data);
                JsonElement root = doc.RootElement[0];
                result = root.GetRawText();
            }
            else
            {
                result = firstChar != "{" ? "{}" : data;
            }

            return result;
        }

        public static (string FirstName, string LastName) GetFirstAndLastName( this String fullname)
        {
            var firstName = "";
            var lastName = "";

            var split = fullname.Split(new char[] { ' ' }, 2);

            firstName = split[0];
            lastName = split[1];

            return (firstName, lastName);
        }

        public static string SanitizationFilter(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            
            // Trim whitespace at the beginning and end
            var sanitized = input.Trim();

            // Remove prepositions
            foreach (var preposition in Prepositions)
            {
                var pattern = $@"\b{Regex.Escape(preposition)}\b";
                sanitized = Regex.Replace(sanitized, pattern, "", RegexOptions.IgnoreCase);
            }

            //  Convert to lowercase
            sanitized = sanitized.ToLowerInvariant();

            // Remove accents
            sanitized = RemoveAccents(sanitized);

            // Normalize spaces
            sanitized = Regex.Replace(sanitized, @"\s+", " ");

            return sanitized.Trim();
        }

        private static string RemoveAccents(string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}