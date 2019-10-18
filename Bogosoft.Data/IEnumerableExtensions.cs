using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for an <see cref="IEnumerable{T}"/> type.
    /// </summary>
    public static class IEnumerableExtensions
    {
        static DbDataReader CreateDataReader<T>(
            IAsyncEnumerable<T> source,
            FieldAdapter<T>[] fields,
            CancellationToken token
            )
        {
            var dependencies = GetDataReaderDependencies(fields);

            return new AsyncCollectionToDataReaderAdapter<T>(
                dependencies.Item1,
                dependencies.Item2,
                fields,
                dependencies.Item3,
                source,
                token
                );
        }

        static DbDataReader CreateDataReader<T>(IEnumerable<T> source, FieldAdapter<T>[] fields)
        {
            var dependencies = GetDataReaderDependencies(fields);

            return new SynchronousCollectionToDataReaderAdapter<T>(
                dependencies.Item1,
                dependencies.Item2,
                fields,
                dependencies.Item3,
                source
                );
        }

        static (object[], Dictionary<string, int>, DataTable) GetDataReaderDependencies<T>(FieldAdapter<T>[] fields)
        {
            var buffer = new object[fields.Length];
            var fieldIndicesByName = new Dictionary<string, int>();
            var schemaTable = new DataTable();

            for (var i = 0; i < fields.Length; i++)
            {
                fieldIndicesByName[fields[i].Name] = i;

                schemaTable.Columns.Add(fields[i].Name, fields[i].Type);
            }

            return (buffer, fieldIndicesByName, schemaTable);
        }

        /// <summary>
        /// Convert the current asynchronously enumerable sequence to a data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="source">The current sequence of items.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <param name="token">An opportunity to respond to a cancellation request.</param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDataReader<T>(
            this IAsyncEnumerable<T> source,
            IEnumerable<FieldAdapter<T>> fields,
            CancellationToken token = default
            )
        {
            return CreateDataReader(source, fields.ToArray(), token);
        }

        /// <summary>
        /// Convert the current asynchronously enumerable sequence to a data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="source">The current sequence of items.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDataReader<T>(
            this IAsyncEnumerable<T> source,
            params FieldAdapter<T>[] fields
            )
        {
            return CreateDataReader(source, fields, CancellationToken.None);
        }

        /// <summary>
        /// Convert the current asynchronously enumerable sequence to a data reader.
        /// </summary>
        /// <typeparam name="T">The type of the items in the current sequence.</typeparam>
        /// <param name="source">The current sequence of items.</param>
        /// <param name="token">An opportunity to respond to a cancellation request.</param>
        /// <param name="fields">
        /// A sequence of field adapters for the new data record. The ordinal position of each field in the sequence
        /// will populate the corresponding ordinal position in a record with its value extractor.
        /// </param>
        /// <returns>A new data reader.</returns>
        public static DbDataReader ToDataReader<T>(
            this IAsyncEnumerable<T> source,
            CancellationToken token,
            params FieldAdapter<T>[] fields
            )
        {
            return CreateDataReader(source, fields, CancellationToken.None);
        }

        /// <summary>
        /// Convert the current enumerable sequence to a data reader.
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