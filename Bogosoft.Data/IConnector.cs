using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents any type capable of creating a connection to a data source.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the data source connection which must be derived from the <see cref="DbConnection"/> type.
    /// </typeparam>
    public interface IConnector<T> where T : DbConnection
    {
        /// <summary>
        /// Connect to a data source and return an object representing the connection.
        /// </summary>
        /// <returns>An object representing a connection to a data source.</returns>
        T Connect();

        /// <summary>
        /// Connect to a data source and return an object representing the connection.
        /// </summary>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object representing a connection to a data source.</returns>
        Task<T> ConnectAsync(CancellationToken token);
    }
}