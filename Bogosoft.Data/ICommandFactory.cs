using System.Data;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents any type capable of creating commands that derive from <see cref="DbCommand"/>.
    /// </summary>
    /// <typeparam name="T">The type of generated commands.</typeparam>
    public interface ICommandFactory<T> where T : DbCommand
    {
        /// <summary>
        /// Create an executable database command.
        /// </summary>
        /// <param name="commandText">The text of the newly generated command.</param>
        /// <param name="commandType">The type of the newly generated command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        T Create(string commandText, CommandType commandType);
    }
}