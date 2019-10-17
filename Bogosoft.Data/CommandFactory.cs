using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// A set of static members for working with types that implement <see cref="ICommandFactory{T}"/>.
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Create an executable database command. The command type of the newly generated command
        /// will be <see cref="CommandType.Text"/>.
        /// </summary>
        /// <typeparam name="T">The type of generated commands.</typeparam>
        /// <param name="provider">The current command provider.</param>
        /// <param name="commandText">The text of the newly generated command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        public static T Create<T>(this ICommandFactory<T> provider, string commandText) where T : DbCommand
        {
            return provider.Create(commandText, CommandType.Text);
        }

        /// <summary>
        /// Execute a simple SQL statement.
        /// </summary>
        /// <typeparam name="T">The type of the command to be executed.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <returns>The number of records affected by the executed command.</returns>
        public static int ExecuteNonQuery<T>(this ICommandFactory<T> self, string commandText) where T : DbCommand
        {
            using var command = self.Create(commandText);

            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Asynchronously execute a simple SQL statement.
        /// </summary>
        /// <typeparam name="T">The type of the command to be executed.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>The number of records affected by the executed command.</returns>
        public static async Task<int> ExecuteNonQueryAsync<T>(
            this ICommandFactory<T> self,
            string commandText,
            CancellationToken token = default
            )
            where T : DbCommand
        {
            using var command = self.Create(commandText);

            return await command.ExecuteNonQueryAsync(token);
        }

        /// <summary>
        /// Execute a SQL statement and return the value in the first column of the first row,
        /// ignoring all other rows and columns.
        /// </summary>
        /// <typeparam name="T">The type of the command to be executed.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <returns>
        /// The value in the first column of the first row of the result set.
        /// </returns>
        public static object ExecuteScalar<T>(this ICommandFactory<T> self, string commandText) where T : DbCommand
        {
            using var command = self.Create(commandText);

            return command.ExecuteScalar();
        }

        /// <summary>
        /// Execute an SQL statement and return the value in the first column of the first row
        /// as an object of a specified type, ignoring all other rows and columns.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
        /// <typeparam name="TResult">The type of the scalar-valued result.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <returns>
        /// The value in the first column of the first row of the result set as an object of the specified type.
        /// </returns>
        public static TResult ExecuteScalar<TCommand, TResult>(this ICommandFactory<TCommand> self, string commandText)
            where TCommand : DbCommand
        {
            using var command = self.Create(commandText);

            return (TResult)command.ExecuteScalar();
        }

        /// <summary>
        /// Asynchronously execute an SQL statement and return the value in the first column
        /// of the first row, ignoring all other columns and rows.
        /// </summary>
        /// <typeparam name="T">The type of the command to be executed.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>
        /// The value in the first column of the first row of the result set.
        /// </returns>
        public static async Task<object> ExecuteScalarAsync<T>(
            this ICommandFactory<T> self,
            string commandText,
            CancellationToken token = default
            )
            where T : DbCommand
        {
            using var command = self.Create(commandText);

            return await command.ExecuteScalarAsync(token);
        }

        /// <summary>
        /// Asynchronously execute an SQL statement and return the value in the first column of
        /// the first row as an object of a specified type, ignoring all other columns and rows.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
        /// <typeparam name="TResult">The type of the scalar-valued result.</typeparam>
        /// <param name="self">The current command provider.</param>
        /// <param name="commandText">The text of a command to execute.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>
        /// The value in the first column of the first row of the result set as an object of the specified type.
        /// </returns>
        public static async Task<TResult> ExecuteScalarAsync<TCommand, TResult>(
            this ICommandFactory<TCommand> self,
            string commandText,
            CancellationToken token = default
            )
            where TCommand : DbCommand
        {
            using var command = self.Create(commandText);

            return (TResult)(await command.ExecuteScalarAsync(token));
        }
    }
}