using System.Text.RegularExpressions;

namespace RunLengthEncoding.Encoding
{
    public static class RLE
    {
        public const char Splitter = '|';
        private const string RegexPattern = @"\d+\D";

        public static string Encode(string text)
        {
            var encodedText = string.Empty;
            var previousChar = text[0];
            var count = 1;

            for (var c = 1; c < text.Length; c++)
            {
                var currentChar = text[c];

                if (currentChar.Equals(previousChar))
                {
                    count++;
                }
                else
                {
                    encodedText = UpdateEncodedValue(encodedText, count, previousChar);
                    previousChar = currentChar;
                    count = 1;
                }

                if (c == text.Length - 1)
                {
                    encodedText = UpdateEncodedValue(encodedText, count, previousChar);
                }
            }

            return encodedText;
        }

        public static string Decode(string text)
        {
            string decodedText = string.Empty;

            string[] parts = text.SplitText();

            foreach (var part in parts)
            {
                decodedText = UpdateDecodedValue(decodedText, part);
            }

            return decodedText;
        }

        #region Helpers

        private static string UpdateEncodedValue(string encodedValue, int count, char character)
        {
            string text = char.IsDigit(character)
                ? string.Concat(character, Splitter)
                : character.ToString();

            return string.Concat(encodedValue, count, text);
        }

        private static string UpdateDecodedValue(string decodedValue, string textPart)
        {
            (char character, int occurences) = textPart.SplitPart();

            return string.Concat(decodedValue, new string(character, occurences));
        }

        #endregion

        #region Extensions

        private static string[] SplitText(this string text)
        {
            return Regex.Matches(text, RLE.RegexPattern)
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToArray();
        }

        private static (char character, int occurences) SplitPart(this string part)
        {
            char character = part.Last();

            if (character.Equals(RLE.Splitter))
            {
                part = part.RemoveLastCharacter();
                character = part.Last();
            }

            int occurences = int.Parse(part.RemoveLastCharacter());

            return (character, occurences);
        }

        private static string RemoveLastCharacter(this string text)
        {
            return text.Remove(text.Length - 1);
        }

        #endregion
    }
}
