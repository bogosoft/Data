using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a connection to a data source.
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Get or set the connection string used to connect to a data source.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Get or set the name of the database to connect to, or if already connected,
        /// the name of the database currently being used.
        /// </summary>
        string Database { get; set; }

        /// <summary>
        /// Get the state of the current connection.
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Get the time, in seconds, to wait while establishing a connection
        /// before terminating the attempt.
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// Close the current connection to a data source.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A value indicating whether or not the current connection was successfully closed.
        /// </returns>
        Task<bool> CloseAsync(CancellationToken token);

        /// <summary>
        /// Create an executable command scoped to the current connection.
        /// </summary>
        /// <returns>A database command.</returns>
        ICommand CreateCommand();

        /// <summary>
        /// Attempt to open the current connection to a data source.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A value indicating whether or not the connection was successfully opened.
        /// </returns>
        Task<bool> OpenAsync(CancellationToken token);
    }
}