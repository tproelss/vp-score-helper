using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPScoreHelper
{
    class ClipboardParser
    {
        public static ScoreEntry GetNewScoreEntryFromClipboardContent(string clipboard, List<ScoreEntry> knownScoreEntries)
        {
            ScoreEntry se = MatchKnownScoreEntry(clipboard, knownScoreEntries);

            if (se == null)
                return null;

            var score = GetScoreFromClipboard(clipboard);

            if (score <= 0)
                return null;

            se.Score = score;

            return se;
        }

        private static ScoreEntry MatchKnownScoreEntry(string clipboard, List<ScoreEntry> knownScoreEntries)
        {           
            // direct match
            foreach (ScoreEntry se in knownScoreEntries)
            {
                if (clipboard.Contains(se.Name))
                {
                    return se;
                }
            }

            // match simplified text
            var simplifiedClipboard = ConversionHelper.GetSimplifiedString(clipboard);
            foreach (ScoreEntry se in knownScoreEntries)
            {
                if (simplifiedClipboard.Contains(se.SimplifiedName))
                {
                    return se;
                }
            }

            // match with replaced chars and simplyfied
            var replacedAndsimplifiedClipboard = ConversionHelper.GetReplacedAndSimplifiedString(clipboard);
            foreach (ScoreEntry se in knownScoreEntries)
            {
                if (replacedAndsimplifiedClipboard.Contains(se.ReplacedAndSimplifiedName))
                {
                    return se;
                }
            }

            return null;
        }

        private static ulong GetScoreFromClipboard(string clipboard)
        {            
            StringBuilder sb = new StringBuilder();
            foreach (var c in clipboard.ToCharArray())
            {
                if (c == '.' || c == ',')
                    continue;

                if (c >= '0' && c <= '9')
                {
                    sb.Append(c);
                    continue;
                }

                sb.Append(' ');
            }
            
            foreach (var part in sb.ToString().Split(new char[] { ' ' }))
            {
                if (ValidateScoreChars(part))
                {
                    ulong scoreValue = 0;
                    if (ulong.TryParse(part, out scoreValue))
                    {
                        return scoreValue;
                    }
                }
            }

            return 0;
        }

        private static bool ValidateScoreChars(string part)
        {
            if (string.IsNullOrEmpty(part))
                return false;

            if (part.Length < 3)
                return false;

            if (part.Length > 15)
                return false;

            foreach (char c in part.ToCharArray())
            {
                if ("0123456789".IndexOf(c) < 0)
                    return false;
            }

            return true;
        }        
    }
}
