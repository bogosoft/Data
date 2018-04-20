using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="DbDataReader"/> type.
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        /// Locate a field by its ordinal position (index) and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="ordinal">The ordinal position (index) of the field to access.</param>
        /// <param name="ifDBNull">
        /// A value to return in the event that the accessed value equals <see cref="DBNull.Value"/>.
        /// </param>
        /// <returns>The value of the specified field, or a fallback value, of the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given position does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static T GetFieldValue<T>(this DbDataReader reader, int ordinal, T ifDBNull)
        {
            return reader.IsDBNull(ordinal) ? ifDBNull : reader.GetFieldValue<T>(ordinal);
        }

        /// <summary>
        /// Locate a field by its name and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="name">The name of the field to access.</param>
        /// <param name="ifDBNull">
        /// A value to return in the event that the accessed value equals <see cref="DBNull.Value"/>.
        /// </param>
        /// <returns>The value of the specified field, or a fallback value, of the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static T GetFieldValue<T>(this DbDataReader reader, string name, T ifDBNull = default(T))
        {
            return reader.GetFieldValue(reader.GetOrdinal(name), ifDBNull);
        }

        /// <summary>
        /// Locate a field by its ordinal position (index) and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="ordinal">The ordinal position (index) of the field to access.</param>
        /// <param name="ifDBNull">
        /// A value to return in the event that the accessed value equals <see cref="DBNull.Value"/>.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>The value of the specified field, or a fallback value, of the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given position does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static async Task<T> GetFieldValueAsync<T>(
            this DbDataReader reader,
            int ordinal,
            T ifDBNull,
            CancellationToken token = default(CancellationToken)
            )
        {
            if (await reader.IsDBNullAsync(ordinal, token))
            {
                return ifDBNull;
            }
            else
            {
                return await reader.GetFieldValueAsync<T>(ordinal, token);
            }
        }

        /// <summary>
        /// Locate a field by its name and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="name">The name of the field to access.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>The value of the specified field as the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static Task<T> GetFieldValueAsync<T>(
            this DbDataReader reader,
            string name,
            CancellationToken token = default(CancellationToken)
            )
        {
            return reader.GetFieldValueAsync<T>(reader.GetOrdinal(name), token);
        }

        /// <summary>
        /// Locate a field by its name and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="name">The name of the field to access.</param>
        /// <param name="ifDBNull">
        /// A value to return in the event that the accessed value equals <see cref="DBNull.Value"/>.
        /// </param>
        /// <returns>The value of the specified field, or a fallback value, of the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static Task<T> GetFieldValueAsync<T>(
            this DbDataReader reader,
            string name,
            T ifDBNull = default(T)
            )
        {
            return reader.GetFieldValueAsync(reader.GetOrdinal(name), ifDBNull);
        }

        /// <summary>
        /// Locate a field by its name and return its value as an object of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="name">The name of the field to access.</param>
        /// <param name="ifDBNull">
        /// A value to return in the event that the accessed value equals <see cref="DBNull.Value"/>.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>The value of the specified field, or a fallback value, of the specified type.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown in the event that the type of the value returned by the data source either does
        /// not match, or cannot be cast to, the specified type.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static Task<T> GetFieldValueAsync<T>(
            this DbDataReader reader,
            string name,
            T ifDBNull,
            CancellationToken token = default(CancellationToken)
            )
        {
            return reader.GetFieldValueAsync(reader.GetOrdinal(name), ifDBNull, token);
        }

        /// <summary>
        /// Locate a field by its name and determine whether its associated value is null.
        /// </summary>
        /// <param name="reader">The current <see cref="DbDataReader"/> implementation.</param>
        /// <param name="name">The name of the field to access.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>True if the value of the specified field is null; false otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data reader.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the connection drops or is closed during data retrieval; the data reader
        /// is closed during data retrieval; there is no data ready to read (i.e., the <see cref="DbDataReader.Read"/>
        /// method has not yet been called for this first time or has returned false); a previously read column
        /// was accessed for a second time in sequential mode; there was an asynchronous operation in progress.
        /// </exception>
        public static Task<bool> IsDBNullAsync(
            this DbDataReader reader,
            string name,
            CancellationToken token = default(CancellationToken)
            )
        {
            return reader.IsDBNullAsync(reader.GetOrdinal(name), token);
        }

        /// <summary>
        /// Convert the current .NET data reader into an enumerable sequence.
        /// </summary>
        /// <typeparam name="T">The type of the items in the new collection.</typeparam>
        /// <param name="reader">The current <see cref="DbDataReader"/>.</param>
        /// <param name="mapper">
        /// A strategy for mapping each record of the current data reader to an object of the specified type.
        /// </param>
        /// <returns>An enumerable sequence of items of the specified type.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this DbDataReader reader, Func<DbDataReader, T> mapper)
        {
            while (reader.Read())
            {
                yield return mapper.Invoke(reader);
            }
        }
    }
}