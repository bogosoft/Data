using System;
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
        /// <param name="configure">A configuration strategy to be applied to a newly created command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        T Create(Action<T> configure);
    }
}