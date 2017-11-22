using System;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for enumerable sequences of records.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Append a given row to the curent sequence of records.
        /// </summary>
        /// <typeparam name="T">The type of the values in a record.</typeparam>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="record">
        /// An additional records to append to the current sequence.
        /// </param>
        /// <returns>
        /// A new sequence of records as the given record appended to the current sequence.
        /// </returns>
        public static IEnumerable<T[]> Append<T>(this IEnumerable<T[]> records, params T[] record)
        {
            foreach (var r in records)
            {
                yield return r;
            }

            yield return record;
        }

        /// <summary>
        /// Convert the current string-valued record enumerator into a sequence of parsed object records.
        /// </summary>
        /// <param name="records">The current string-valued record enumerator.</param>
        /// <param name="parsers">
        /// A sequence of parsers with which to convert record values as strings into parsed objects.
        /// Each parser will act upon the string value at its corresponding position within the
        /// sequence, i.e. the parser at postion n+1 will parse the string value at n+1 for each record.
        /// </param>
        /// <returns>A sequence of parsed object records.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the given record enumerator is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given record enumerator is empty.
        /// </exception>
        public static IEnumerable<object[]> Parse(this IEnumerator<string[]> records, IEnumerable<Parser> parsers)
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (!records.MoveNext())
            {
                throw new InvalidOperationException("Sequence contains zero records.");
            }

            return records.Parse(records.Current, parsers);
        }

        /// <summary>
        /// Convert the current string-valued record enumerator into a parsed object record enumerator.
        /// </summary>
        /// <param name="records">The current string-valued record enumerator.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinality of each column will correspond
        /// to the position of the column name within the given sequence.
        /// </param>
        /// <param name="parsers">
        /// A sequence of parsers with which to convert record values as strings into parsed objects.
        /// Each parser will act upon the string value at its corresponding position within the
        /// sequence, i.e. the parser at postion n+1 will parse the string value at n+1 for each record.
        /// </param>
        /// <returns>A sequence of parsed object records.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the given record enumerator, the given sequence of columns
        /// or the given sequence of parsers is null.
        /// </exception>
        public static IEnumerable<object[]> Parse(
            this IEnumerator<string[]> records,
            IEnumerable<string> columns,
            IEnumerable<Parser> parsers
            )
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (columns is null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (parsers is null)
            {
                throw new ArgumentNullException(nameof(parsers));
            }

            if (!records.MoveNext())
            {
                yield break;
            }

            object[] buffer = records.Current;

            yield return buffer;

            var i = 0;

            while (records.MoveNext())
            {
                foreach (var parser in parsers)
                {
                    buffer[i] = parser.Invoke(records.Current[i++]);
                }

                yield return buffer;

                i = 0;
            }
        }

        /// <summary>
        /// Convert the current sequence of string-valued records into a sequence of parsed object records.
        /// </summary>
        /// <param name="records">The current sequence of string-valued records.</param>
        /// <param name="parsers">
        /// A sequence of parsers with which to convert record values as strings into parsed objects.
        /// Each parser will act upon the string value at its corresponding position within the
        /// sequence, i.e. the parser at postion n+1 will parse the string value at n+1 for each record.
        /// </param>
        /// <returns>A sequence of parsed object records.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the given sequence of records is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given sequence of records is empty.
        /// </exception>
        public static IEnumerable<object[]> Parse(this IEnumerable<string[]> records, IEnumerable<Parser> parsers)
        {
            return records.GetEnumerator().Parse(parsers);
        }

        /// <summary>
        /// Convert the current sequence of string-valued records into a sequence of parsed object records.
        /// </summary>
        /// <param name="records">The current sequence of string-valued records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinality of each column will correspond
        /// to the position of the column name within the given sequence.
        /// </param>
        /// <param name="parsers">
        /// A sequence of parsers with which to convert record values as strings into parsed objects.
        /// Each parser will act upon the string value at its corresponding position within the
        /// sequence, i.e. the parser at postion n+1 will parse the string value at n+1 for each record.
        /// </param>
        /// <returns>A sequence of parsed object records.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the given sequence of records, the given sequence of columns
        /// or the given sequence of parsers is null.
        /// </exception>
        public static IEnumerable<object[]> Parse(
            this IEnumerable<string[]> records,
            IEnumerable<string> columns,
            IEnumerable<Parser> parsers
            )
        {
            return records.GetEnumerator().Parse(columns, parsers);
        }

        /// <summary>
        /// Prepend a given records to the current sequence of records.
        /// </summary>
        /// <typeparam name="T">The type of the values in a record.</typeparam>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="record">
        /// An additional record to prepend to the current sequence.
        /// </param>
        /// <returns>
        /// A new sequence of records as the given record prepended to the current sequence.
        /// </returns>
        public static IEnumerable<T[]> Prepend<T>(this IEnumerable<T[]> records, params T[] record)
        {
            yield return record;

            foreach (var r in records)
            {
                yield return r;
            }
        }
    }
}