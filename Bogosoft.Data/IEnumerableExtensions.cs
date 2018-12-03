using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for an <see cref="IEnumerable{T}"/> type.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Convert the current enumerable sequence to a .NET data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="items">The current <see cref="IEnumerable{T}"/> implementation.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDbDataReader<T>(this IEnumerable<T> items, IEnumerable<FieldAdapter<T>> fields)
        {
            return ToDbDataReader(items.GetEnumerator(), fields.ToArray());
        }

        /// <summary>
        /// Convert the current enumerable sequence to a .NET data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="items">The current <see cref="IEnumerable{T}"/> implementation.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDbDataReader<T>(this IEnumerable<T> items, params FieldAdapter<T>[] fields)
        {
            return ToDbDataReader(items.GetEnumerator(), fields);
        }

        static DbDataReader ToDbDataReader<T>(this IEnumerator<T> source, FieldAdapter<T>[] fields)
        {
            var reader = new CollectionToDataReaderAdapter<T>
            {
                Buffer = new object[fields.Length],
                FieldIndicesByName = new Dictionary<string, int>(),
                Fields = fields,
                SchemaTable = new DataTable(),
                Source = source
            };

            for (var i = 0; i < fields.Length; i++)
            {
                reader.FieldIndicesByName[fields[i].Name] = i;

                reader.SchemaTable.Columns.Add(fields[i].Name, fields[i].Type);
            }

            return reader;
        }
    }
}