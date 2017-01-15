using Bogosoft.Collections.Async;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a command that is capable of being executed against
    /// a <see cref="IConnection"/> object.
    /// </summary>
    public interface ICommand : IDisposable
    {
        /// <summary>
        /// Get or set the database connection that this command will be executed against.
        /// </summary>
        IConnection Connection { get; set; }

        /// <summary>
        /// Get the collection of parameters associated with the current command.
        /// </summary>
        IParameterCollection Parameters { get; }

        /// <summary>
        /// Get or set the text for the current command.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Get or set the context within which the <see cref="Text"/> value is interpreted.
        /// </summary>
        CommandType Type { get; set; }

        /// <summary>
        /// Execute the current command and return the number of records that were affected
        /// by its execution.
        /// </summary>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A value corresponding to the number of records affected by the execution of the command.
        /// </returns>
        Task<ulong> ExecuteNonQueryAsync(CancellationToken token);

        /// <summary>
        /// Execute the current command and return a traversable set of results.
        /// </summary>
        /// <param name="behavior">
        /// A value corresponding to the intended behavior of a data source when executing the current command.
        /// </param>
        /// <returns>A sequence of zero or more results.</returns>
        ITraversable<IResult> ToResultSet(CommandBehavior behavior = CommandBehavior.Default);

        /// <summary>
        /// Execute the current command and return the value of the first column of the first row
        /// of the first result set as a typed scalar.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A value of the given type.</returns>
        Task<T> ToScalarAsync<T>(CancellationToken token);
    }
}