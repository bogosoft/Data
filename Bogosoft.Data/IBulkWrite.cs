using System;
using DataTable = System.Data.DataTable;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Indicates that an implementation is capable of bulk writing data to a data source.
    /// </summary>
    public interface IBulkWrite : IDisposable
    {
        /// <summary>
        /// Get or set the number of rows to batch before writing to the data source.
        /// </summary>
        int BatchSize { get; set; }

        /// <summary>
        /// Get or set the connection to a data source to be used for bulk write operations.
        /// </summary>
        IConnection Connection { get; set; }

        /// <summary>
        /// Get or set the destination relation (table, view, etc.) name at the data source.
        /// </summary>
        string Destination { get; set; }

        /// <summary>
        /// Get or set the time, in seconds, for an operation to complete before it terminates.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Bulk write the contents of a <see cref="DataTable"/> to the data source.
        /// </summary>
        /// <param name="table">A data table.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A task which represents the asynchronous operation.</returns>
        Task WriteAsync(DataTable table, CancellationToken token);

        /// <summary>
        /// Bulk write the contents of a data reader to the data source.
        /// </summary>
        /// <param name="reader">A data reader.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A task which represents the asynchronous operation.</returns>
        Task WriteAsync(IReader reader, CancellationToken token);
    }
}