using Bogosoft.Collections.Async;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a single row of zero or more values organized into fields or columns.
    /// </summary>
    public interface IRow
    {
        /// <summary>
        /// Get a value corresponding to the number of columns (fields) in the current row.
        /// </summary>
        int FieldCount { get; }

        /// <summary>
        /// Reads a sequence of bytes from the specified column into an array.
        /// </summary>
        /// <param name="ordinal">
        /// The zero-based numeric index of the desired column.
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
        long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length);

        /// <summary>
        /// Reads a sequence of chars from the specified column into an array.
        /// </summary>
        /// <param name="ordinal">
        /// The zero-based numeric index of the desired column.
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
        long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length);

        /// <summary>
        /// Get the value of a given column or field as an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to return.</typeparam>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numerical index of the desired column.
        /// </param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>An object of the specified type.</returns>
        Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken token);

        /// <summary>
        /// Get the name of a column or field by its index.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numeric index of the column or field in question.
        /// </param>
        /// <returns>
        /// A value corresponding to the name of the referenced field.
        /// </returns>
        string GetName(int ordinal);

        /// <summary>
        /// Get the index of a field or column by its name.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name of a field or column whose index is requested.
        /// </param>
        /// <returns>
        /// A value corresponding to the zero-based, numberical index of the given field or column name,
        /// or -1 if no such field or column name exists.
        /// </returns>
        int GetOrdinal(string name);

        /// <summary>
        /// Determine whether the given column or field contains a null value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numerical index of the column to inspect.
        /// </param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A value indicating whether or not the given column or field contains
        /// a value equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        Task<bool> IsDBNullAsync(int ordinal, CancellationToken token);
    }
}