using Bogosoft.Collections.Async;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IRow"/> contract.
    /// </summary>
    public static class RowExtensions
    {
        /// <summary>
        /// Reads a sequence of bytes from the specified column into an array.
        /// </summary>
        /// <param name="row">The current <see cref="IRow"/> implementation</param>.
        /// <param name="name">
        /// The name of the desired column.
        /// </param>
        /// <param name="dataIndex">
        /// A value corresponding to the start position within the stream to copy from.
        /// </param>
        /// <param name="buffer">
        /// An array into which the read bytes will be copied.
        /// </param>
        /// <param name="bufferIndex">
        /// A value corresponding to the start position within the buffer to copy to.
        /// </param>
        /// <param name="length">
        /// A value corresponding to the maximum number of bytes to copy.
        /// </param>
        /// <returns>
        /// A value corresponding to the actual number of bytes copied.
        /// </returns>
        public static long GetBytes(
            this IRow row,
            string name,
            long dataIndex,
            byte[] buffer,
            int bufferIndex,
            int length
            )
        {
            return row.GetBytes(row.GetOrdinal(name), dataIndex, buffer, bufferIndex, length);
        }

        /// <summary>
        /// Reads a sequence of chars from the specified column into an array.
        /// </summary>
        /// <param name="row">The current <see cref="IRow"/> implementation</param>.
        /// <param name="name">
        /// The name of the desired column.
        /// </param>
        /// <param name="dataIndex">
        /// A value corresponding to the start position within the stream to copy from.
        /// </param>
        /// <param name="buffer">
        /// An array into which the read bytes will be copied.
        /// </param>
        /// <param name="bufferIndex">
        /// A value corresponding to the start position within the buffer to copy to.
        /// </param>
        /// <param name="length">
        /// A value corresponding to the maximum number of chars to copy.
        /// </param>
        /// <returns>
        /// A value corresponding to the actual number of chars copied.
        /// </returns>
        public static long GetChars(
            this IRow row,
            string name,
            long dataIndex,
            char[] buffer,
            int bufferIndex,
            int length
            )
        {
            return row.GetChars(row.GetOrdinal(name), dataIndex, buffer, bufferIndex, length);
        }

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type. Calling this method
        /// is equivalent to calling <see cref="IRow.GetFieldValueAsync{T}(int, CancellationToken)"/> with a
        /// value of <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numeric index of the desired column.
        /// </param>
        /// <returns>An object of the specified type.</returns>
        public static async Task<T> GetFieldValueAsync<T>(this IRow row, int ordinal)
        {
            return await row.GetFieldValueAsync<T>(ordinal, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type. Calling this method
        /// is equivalent to calling <see cref="GetFieldValueAsync{T}(IRow, int, T, CancellationToken)"/>
        /// with a value of <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numerical index of the desired column.
        /// </param>
        /// <param name="ifnull">
        /// A value to return in the case that the requested value is null.
        /// </param>
        /// <returns>An object of the specified type.</returns>
        public static async Task<T> GetFieldValueAsync<T>(this IRow row, int ordinal, T ifnull)
        {
            if(await row.IsDBNullAsync(ordinal, CancellationToken.None))
            {
                return ifnull;
            }
            else
            {
                return await row.GetFieldValueAsync<T>(ordinal, CancellationToken.None);
            }
        }

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numerical index of the desired column.
        /// </param>
        /// <param name="ifnull">
        /// A value to return in the case that the requested value is null.
        /// </param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>An object of the specified type.</returns>
        public static async Task<T> GetFieldValueAsync<T>(
            this IRow row,
            int ordinal,
            T ifnull,
            CancellationToken token
            )
        {
            if(await row.IsDBNullAsync(ordinal, token).ConfigureAwait(false))
            {
                return ifnull;
            }
            else
            {
                return await row.GetFieldValueAsync<T>(ordinal, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type. Calling this method
        /// is equivalent to calling <see cref="GetFieldValueAsync{T}(IRow, string, CancellationToken, T)"/>
        /// with a value of <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="name">
        /// A value corresponding to the name of the desired column.
        /// </param>
        /// <param name="ifnull">
        /// A value to return in the case that the requested value is null.
        /// </param>
        /// <returns>An object of the specified type.</returns>
        public static async Task<T> GetFieldValueAsync<T>(
            this IRow row,
            string name,
            T ifnull = default(T)
            )
        {
            var ordinal = row.GetOrdinal(name);

            if(await row.IsDBNullAsync(ordinal, CancellationToken.None).ConfigureAwait(false))
            {
                return ifnull;
            }
            else
            {
                return await row.GetFieldValueAsync<T>(ordinal, CancellationToken.None).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="name">
        /// A value corresponding to the name of the desired column.
        /// </param>
        /// <param name="ifnull">
        /// A value to return in the case that the requested value is null.
        /// </param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>An object of the specified type.</returns>
        public static async Task<T> GetFieldValueAsync<T>(
            this IRow row,
            string name,
            CancellationToken token,
            T ifnull = default(T)
            )
        {
            var ordinal = row.GetOrdinal(name);

            if(await row.IsDBNullAsync(ordinal, token).ConfigureAwait(false))
            {
                return ifnull;
            }
            else
            {
                return await row.GetFieldValueAsync<T>(ordinal, token).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Determine whether the given column or field contains a null value.
        /// </summary>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numeric index of the desired column or field.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the given column or field contains
        /// a value equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        public static async Task<bool> IsDBNullAsync(this IRow row, int ordinal)
        {
            return await row.IsDBNullAsync(ordinal, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Determine whether the given column or field contains a null value.
        /// </summary>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="name">
        /// A value corresponding to the name of the desired column or field.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the given column or field contains
        /// a value equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        public static async Task<bool> IsDBNullAsync(this IRow row, string name)
        {
            return await row.IsDBNullAsync(row.GetOrdinal(name), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Determine whether the given column or field contains a null value.
        /// </summary>
        /// <param name="row">The current <see cref="IRow"/> implementation.</param>
        /// <param name="name">
        /// A value corresponding to the name of the desired column or field.
        /// </param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A value indicating whether or not the given column or field contains
        /// a value equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        public static async Task<bool> IsDBNullAsync(this IRow row, string name, CancellationToken token)
        {
            return await row.IsDBNullAsync(row.GetOrdinal(name), token).ConfigureAwait(false);
        }
    }
}