namespace RunLengthEncoding
{
    public static class RLE
    {
        public const char Splitter = '|';
        internal const string RegexPattern = @"\d+\D";

        public static string Encode(string text)
        {
            var encodedText = String.Empty;
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
            string text = Char.IsDigit(character)
                ? string.Concat(character, Splitter)
                : character.ToString();

            return string.Concat(encodedValue, count, text);
        }

        private static string UpdateDecodedValue(string decodedValue, string textPart)
        {
            (char character, int occurences) = textPart.SplitPart();

            return string.Concat(decodedValue, new String(character, occurences));
        }

        #endregion
    }
}
