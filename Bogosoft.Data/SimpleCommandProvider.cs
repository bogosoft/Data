using System;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// A straight-forward implementation of the <see cref="ICommandFactory{T}"/> contract.
    /// </summary>
    /// <typeparam name="TConnection">
    /// The type of the connection against which database commands are to be generated.
    /// </typeparam>
    /// <typeparam name="TCommand">The type of generated database commands.</typeparam>
    public sealed class SimpleCommandProvider<TConnection, TCommand> : ICommandFactory<TCommand>
        where TCommand : DbCommand
        where TConnection : DbConnection
    {
        readonly TConnection connection;

        /// <summary>
        /// Create a new simple command provider.
        /// </summary>
        /// <param name="connection">A connection against which database commands will be generated.</param>
        public SimpleCommandProvider(TConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Create an executable database command.
        /// </summary>
        /// <param name="configure">A configuration strategy to be applied to a newly created command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        public TCommand Create(Action<TCommand> configure)
        {
            var command = connection.CreateCommand() as TCommand;

            configure.Invoke(command);

            return command as TCommand;
        }
    }
}