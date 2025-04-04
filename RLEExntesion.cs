using System.Data;
using System.Text.RegularExpressions;

namespace RunLengthEncoding
{
    internal static class RLEExntesion
    {
        internal static string RemoveLastCharacter(this string text)
        {
            return text.Remove(text.Length - 1);
        }

        internal static string[] SplitText(this string value)
        {
            return Regex.Matches(value, RLE.RegexPattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToArray();
        }

        internal static (char character, int occurences) SplitPart(this string part)
        {
            char character = part.Last();

            if (character.Equals(RLE.Splitter))
            {
                part = part.RemoveLastCharacter();
                character = part.Last();
            }

            int occurences = Int32.Parse(part.RemoveLastCharacter());

            return (character, occurences);
        }
    }
}
