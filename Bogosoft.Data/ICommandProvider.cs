using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents any type capable of generating executable data source commands against data source connections.
    /// </summary>
    /// <typeparam name="TConnection">The type of the data source connection.</typeparam>
    /// <typeparam name="TCommand">The type of the executable command.</typeparam>
    public interface ICommandProvider<TConnection, TCommand>
        where TConnection : DbConnection
        where TCommand : DbCommand
    {
        /// <summary>
        /// Get an command executable against a given data source connection.
        /// </summary>
        /// <param name="connection">
        /// A data source connection against which an executable command is to be generated.
        /// </param>
        /// <returns>A command executable against the given data source connection.</returns>
        TCommand GetCommand(TConnection connection);
    }
}