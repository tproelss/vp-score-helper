using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPScoreHelper
{
    class ConversionHelper
    {
        public static ulong GetScoreFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            str = str.Replace(",", "").Replace(".", "");

            ulong result = 0;

            if (ulong.TryParse(str, out result))
                return result;

            throw new ArgumentException($"'{str}' is not a valid score format");
        }

        public static string GetStringFromScore(ulong score)
        {
            IFormatProvider usFormatProvider =
                new System.Globalization.CultureInfo("en-US");

            return score.ToString("#,##", usFormatProvider);
        }

        public static string GetSpreadSheetId(string input)
        {
            // e.g.
            // https://docs.google.com/spreadsheets/d/<spreadsheetId>/edit#gid=0

            if (string.IsNullOrEmpty(input))
            {
                Log.Instance.WriteLine(LogLevel.Info, $"Url seems to be empty");
                return input;
            }

            if (!input.Contains(@"/d/"))
            {
                Log.Instance.WriteLine(LogLevel.Info, $"Url seems to have unusual format");
                return input;
            }

            string result = "";
            bool nextOne = false;

            foreach (string part in input.Split(new char[] { '/' }))
            {
                if (part == "d")
                {
                    nextOne = true;
                    continue;
                }

                if (nextOne)
                {
                    result = part;
                    break;
                }
            }

            Log.Instance.WriteLine(LogLevel.Info, $"Identified spreadSheetId {result}");

            return result;
        }

        public static string GetSimplifiedString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder sb = new StringBuilder();

            foreach (var c in input.ToCharArray())
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                    sb.Append(c);
            }

            var result = sb.ToString().ToLower();

            return result;
        }

        public static string GetReplacedAndSimplifiedString(string input)
        {
            var letters = "áéèêžčôőóø".ToCharArray();
            var replacements = "aeeezcoooo".ToCharArray();

            var sb = new StringBuilder(input);

            for (int i = 0; i < letters.Length; i++)
            {
                sb = sb.Replace(letters[i], replacements[i]);
            }

            return GetSimplifiedString(sb.ToString());
        }
    }
}
