using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for sequence of typed arrays.
    /// </summary>
    public static class ArraySequenceExtensions
    {
        /// <summary>
        /// Convert the current record enumerator into a data reader. Column names
        /// will be taken from the first record retrieved from the enumerator.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <returns>
        /// The current record enumerator wrapped in a data reader.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given enumerator is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given enumerator contains no records.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerator<object[]> records)
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (!records.MoveNext())
            {
                throw new InvalidOperationException(Messages.NoRecords);
            }

            return records.ToDataReader(records.Current.Select(x => x is string ? x as string : x.ToString()));
        }

        /// <summary>
        /// Convert the current record enumerator into a data reader.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <returns>
        /// The current record enumerator wrapped in a data reader.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given enumerator or the given column name sequence is null.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerator<object[]> records, IEnumerable<string> columns)
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (columns is null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            return new ObjectArraySequenceDataReader(records, columns);
        }

        /// <summary>
        /// Convert the current sequence of records into a data reader. Column names
        /// will be taken from the first record retrieved from the sequence.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <returns>The current sequence of records wrapped in a data reader.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given sequene is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given sequence contains no records.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerable<object[]> records)
        {
            return records.GetEnumerator().ToDataReader();
        }

        /// <summary>
        /// Convert the current sequence of records into a data reader.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <returns>The current sequence of records wrapped in a data reader.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given sequence of records or sequence of column names is null.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerable<object[]> records, IEnumerable<string> columns)
        {
            return records.GetEnumerator().ToDataReader(columns);
        }

        /// <summary>
        /// Convert the current record enumerator into a data reader. Column names
        /// will be taken from the first record retrieved from the enumerator.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="parsers">
        /// A sequence of parsers to use for converting string values to objects. The ordinal position of a
        /// parser within the sequence will correspond exactly to the ordinal position of the value to be
        /// parsed within each record.
        /// </param>
        /// <returns>
        /// The current record enumerator wrapped in a data reader.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given enumerator is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given enumerator contains no records.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerator<string[]> records, IEnumerable<Parser> parsers)
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            if (!records.MoveNext())
            {
                throw new InvalidOperationException(Messages.NoRecords);
            }

            return records.ToDataReader(records.Current, parsers);
        }

        /// <summary>
        /// Convert the current record enumerator into a data reader.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <param name="parsers">
        /// A sequence of parsers to use for converting string values to objects. The ordinal position of a
        /// parser within the sequence will correspond exactly to the ordinal position of the value to be
        /// parsed within each record.
        /// </param>
        /// <returns>
        /// The current record enumerator wrapped in a data reader.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given enumerator or the given column name sequence is null.
        /// </exception>
        public static DbDataReader ToDataReader(
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

            return new ParsingDataReader(records, columns.ToArray(), parsers.ToArray());
        }

        /// <summary>
        /// Convert the current sequence of records into a data reader. Column names
        /// will be taken from the first record retrieved from the sequence.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="parsers">
        /// A sequence of parsers to use for converting string values to objects. The ordinal position of a
        /// parser within the sequence will correspond exactly to the ordinal position of the value to be
        /// parsed within each record.
        /// </param>
        /// <returns>The current sequence of records wrapped in a data reader.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given sequene is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given sequence contains no records.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerable<string[]> records, IEnumerable<Parser> parsers)
        {
            return records.GetEnumerator().ToDataReader(parsers);
        }

        /// <summary>
        /// Convert the current sequence of records into a data reader.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <param name="parsers">
        /// A sequence of parsers to use for converting string values to objects. The ordinal position of a
        /// parser within the sequence will correspond exactly to the ordinal position of the value to be
        /// parsed within each record.
        /// </param>
        /// <returns>The current sequence of records wrapped in a data reader.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given sequence of records or sequence of column names is null.
        /// </exception>
        public static DbDataReader ToDataReader(
            this IEnumerable<string[]> records,
            IEnumerable<string> columns,
            IEnumerable<Parser> parsers
            )
        {
            return records.GetEnumerator().ToDataReader(columns, parsers);
        }

        /// <summary>
        /// Convert the current sequence of objects into a data reader.
        /// </summary>
        /// <typeparam name="T">The type of each object in the given sequence.</typeparam>
        /// <param name="objects">A sequence of objects.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <param name="extractors">
        /// A sequence of value extractors to use for extracting values from objects. The ordinal position
        /// of a value extractor within the sequence will correspond exactly to the ordinal position of the
        /// extracted value to be placed within each record.
        /// </param>
        /// <returns>The current sequence of objects as a data reader.</returns>
        public static DbDataReader ToDataReader<T>(
            this IEnumerable<T> objects,
            IEnumerable<string> columns,
            IEnumerable<ValueExtractor<T>> extractors
            )
        {
            return objects.GetEnumerator().ToDataReader(columns, extractors);
        }

        /// <summary>
        /// Convert the current typed enumerator into a data reader.
        /// </summary>
        /// <typeparam name="T">The type of each object in the given sequence.</typeparam>
        /// <param name="objects">A typed enumerator.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinal position of each column in the resulting data reader
        /// will correspond exactly to the position of the column name within the given sequence.
        /// </param>
        /// <param name="extractors">
        /// A sequence of value extractors to use for extracting values from objects. The ordinal position
        /// of a value extractor within the sequence will correspond exactly to the ordinal position of the
        /// extracted value to be placed within each record.
        /// </param>
        /// <returns>The current sequence of objects as a data reader.</returns>
        public static DbDataReader ToDataReader<T>(
            this IEnumerator<T> objects,
            IEnumerable<string> columns,
            IEnumerable<ValueExtractor<T>> extractors
            )
        {
            if (objects is null)
            {
                throw new ArgumentNullException(nameof(objects));
            }

            if (columns is null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            if (extractors is null)
            {
                throw new ArgumentNullException(nameof(extractors));
            }

            return new TypedSequenceDataReader<T>(objects, columns.ToArray(), extractors.ToArray());
        }
    }
}