using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="ICommand"/> contract.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Execute the current command and return the number of records that were affected
        /// by its execution. Calling this method is equivalent to calling
        /// <see cref="ICommand.ExecuteNonQueryAsync(CancellationToken)"/> with a value of
        /// <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <param name="command">The current <see cref="ICommand"/> implementation.</param>
        /// <returns>
        /// A value corresponding to the number of records affected by the execution of the command.
        /// </returns>
        public static async Task<long> ExecuteNonQueryAsync(this ICommand command)
        {
            return await command.ExecuteNonQueryAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute the current command and return the value of the first column of the first row
        /// of the first result set as a typed scalar.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="command">The current <see cref="ICommand"/> implementation.</param>
        /// <returns>A value of the given type.</returns>
        public static async Task<T> ToScalarAsync<T>(this ICommand command)
        {
            return await command.ToScalarAsync<T>(CancellationToken.None).ConfigureAwait(false);
        }
    }
}