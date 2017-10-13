using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for char sequences.
    /// </summary>
    public static class CharEnumerableExtensions
    {
        /// <summary>
        /// Parse a given char sequence into a sequence of fields.
        /// </summary>
        /// <param name="chars">The current sequence of chars.</param>
        /// <param name="buffer">A buffer to use during parsing.</param>
        /// <param name="quoteChar">
        /// A character which identifies a valid quotation character. This character is escaped
        /// when it is doubled up with itself.
        /// </param>
        /// <param name="fieldSeparator">
        /// A character which identifies the end of a field.
        /// </param>
        /// <returns>
        /// A collection of strings as fields separated by the given field separation character.
        /// </returns>
        public static IEnumerable<string> GetFields(
            this IEnumerable<char> chars,
            char[] buffer,
            char quoteChar = '"',
            char fieldSeparator = ','
            )
        {
            var length = 0;
            var quoted = false;

            char c = '\0', p;

            using (var enumerator = chars.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    p = c;

                    c = enumerator.Current;

                    if (c == quoteChar)
                    {
                        quoted = !quoted;

                        if (p == '"')
                        {
                            buffer[length++] = '"';
                        }

                        continue;
                    }

                    if (!quoted && c == ',')
                    {
                        yield return new string(buffer, 0, length);

                        length = 0;
                    }
                    else
                    {
                        buffer[length++] = c;
                    }
                }

                if (length > 0 || c == ',')
                {
                    yield return new string(buffer, 0, length);
                }
            }
        }
    }
}