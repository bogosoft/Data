using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Provides a means of saving a collection of entities.</summary>
    /// <typeparam name="TEntity">The type of an entity to be saved.</typeparam>
    /// <typeparam name="TId">The type of an entity's unique identifier.</typeparam>
    public interface ISave<TEntity, TId>
    {
        /// <summary>Save a collection of entities.</summary>
        /// <param name="entities">A collection of entities.</param>
        /// <returns>
        /// A task which will eventually yield a collection of unique identifiers corresponding
        /// to successfully save entities.
        /// </returns>
        Task<IEnumerable<TId>> SaveAsync(IEnumerable<TEntity> entities);
    }
}