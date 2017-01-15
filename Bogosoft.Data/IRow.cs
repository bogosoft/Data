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
        /// Get the value of a given column (field) as a sequence of bytes.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numeric index of the desired column (field).
        /// </param>
        /// <returns>A sequence of bytes.</returns>
        ITraversable<byte> GetBytes(int ordinal);

        /// <summary>
        /// Get the value of a given column (field) as a sequence of chars.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the zero-based numeric index of the desired column (field).
        /// </param>
        /// <returns>A sequence of chars.</returns>
        ITraversable<char> GetChars(int ordinal);

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