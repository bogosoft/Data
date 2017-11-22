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
                throw new InvalidOperationException("Sequence contains zero records.");
            }

            return records.ToDataReader(records.Current.Select(x => x is string ? x as string : x.ToString()));
        }

        /// <summary>
        /// Convert the current record enumerator into a data reader.
        /// </summary>
        /// <param name="records">The current sequence of records.</param>
        /// <param name="columns">
        /// A sequence of column names. The ordinality of each column will correspond
        /// to the position of the column name within the given sequence.
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
        /// A sequence of column names. The ordinality of each column will correspond
        /// to the position of the column name within the given sequence.
        /// </param>
        /// <returns>The current sequence of records wrapped in a data reader.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thronw in the event that the given sequence of records or sequence of column names is null.
        /// </exception>
        public static DbDataReader ToDataReader(this IEnumerable<object[]> records, IEnumerable<string> columns)
        {
            return records.GetEnumerator().ToDataReader(columns);
        }
    }
}