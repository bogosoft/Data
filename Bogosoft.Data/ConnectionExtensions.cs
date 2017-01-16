using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IConnection"/> contract.
    /// </summary>
    public static class ConnectionExtensions
    {
        /// <summary>
        /// Close the current connection to a data source. Calling this method is equivalent to calling
        /// <see cref="IConnection.CloseAsync(CancellationToken)"/> with a value of
        /// <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <param name="connection">The current <see cref="IConnection"/> implementation.</param>
        /// <returns>
        /// A value indicating whether or not the current connection was successfully closed.
        /// </returns>
        public static async Task<bool> CloseAsync(this IConnection connection)
        {
            return await connection.CloseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Create an executable command scoped to the current connection.
        /// </summary>
        /// <param name="connection">The current <see cref="IConnection"/> implementation.</param>
        /// <param name="text">
        /// A value corresponding to the text of the current command.
        /// </param>
        /// <param name="type">
        /// A value corresponding to the context within which the <see cref="ICommand.Text"/>
        /// value is to be interpreted.
        /// </param>
        /// <returns></returns>
        public static ICommand CreateCommand(
            this IConnection connection,
            string text,
            CommandType type = CommandType.Text
            )
        {
            var command = connection.CreateCommand();

            command.Text = text;
            command.Type = type;

            return command;
        }

        /// <summary>
        /// Attempt to open the current connection to a data source. Calling this method is equivalent to calling
        /// <see cref="IConnection.OpenAsync(CancellationToken)"/> with a value of
        /// <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <param name="connection">The current <see cref="IConnection"/> implementation.</param>
        /// <returns>
        /// A value indicating whether or not the connection was successfully opened.
        /// </returns>
        public static async Task<bool> OpenAsync(this IConnection connection)
        {
            return await connection.OpenAsync(CancellationToken.None);
        }
    }
}