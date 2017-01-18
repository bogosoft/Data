using Bogosoft.Collections.Async;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a sequential data reader.
    /// </summary>
    public interface IReader : IDisposable, IRow, ITraversable<IRow>
    {
        /// <summary>
        /// Get a value indicating whether or not the current reader has been closed.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Get a value corresponding to the number of records affected in the current result set.
        /// </summary>
        int RecordsAffected { get; }

        /// <summary>
        /// Advance the current reader to the next result.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// True if the current reader is positioned on a valid result set after advancing;
        /// false if the reader has moved past its last result set.
        /// </returns>
        Task<bool> NextResultAsync(CancellationToken token);

        /// <summary>
        /// Advance the current reader to the next row.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// True if the current reader is positioned on a valid row after advancing;
        /// false if the reader has moved past its last row.
        /// </returns>
        Task<bool> ReadAsync(CancellationToken token);
    }
}