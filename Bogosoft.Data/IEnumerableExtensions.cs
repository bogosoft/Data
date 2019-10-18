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
        static DbDataReader CreateDataReader<T>(IEnumerable<T> source, FieldAdapter<T>[] fields)
        {
            var buffer = new object[fields.Length];
            var fieldIndicesByName = new Dictionary<string, int>();
            var schemaTable = new DataTable();

            for (var i = 0; i < fields.Length; i++)
            {
                fieldIndicesByName[fields[i].Name] = i;

                schemaTable.Columns.Add(fields[i].Name, fields[i].Type);
            }

            return new SynchronousCollectionToDataReaderAdapter<T>(
                buffer,
                fieldIndicesByName,
                fields.ToArray(),
                schemaTable,
                source
                );
        }

        /// <summary>
        /// Convert the current enumerable sequence to a .NET data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="source">The current sequence of items.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDataReader<T>(this IEnumerable<T> source, IEnumerable<FieldAdapter<T>> fields)
        {
            return CreateDataReader(source, fields.ToArray());
        }

        /// <summary>
        /// Convert the current enumerable sequence to a .NET data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="source">The current sequence of items.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDataReader<T>(this IEnumerable<T> source, params FieldAdapter<T>[] fields)
        {
            return CreateDataReader(source, fields);
        }
    }
}