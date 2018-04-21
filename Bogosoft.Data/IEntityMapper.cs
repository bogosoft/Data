using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents any type capable of mapping each row of a data reader to an object of a specified type.
    /// </summary>
    /// <typeparam name="TReader">The type of the data reader to map row-by-row.</typeparam>
    /// <typeparam name="TEntity">The type of the object to emit for each row.</typeparam>
    public interface IEntityMapper<TReader, TEntity> where TReader : DbDataReader
    {
        /// <summary>
        /// Map the current row of a given data reader to an object of the entity type.
        /// </summary>
        /// <param name="reader">A data reader.</param>
        /// <returns>An object of the entity type.</returns>
        TEntity Map(TReader reader);

        /// <summary>
        /// Map the current row of a given data reader to an object of the entity type.
        /// </summary>
        /// <param name="reader">A data reader.</param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>An object of the entity type.</returns>
        Task<TEntity> MapAsync(TReader reader, CancellationToken token);
    }
}