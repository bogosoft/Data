using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Provides a means of deleting entities.</summary>
    /// <typeparam name="TEntity">The type of entity to be deleted.</typeparam>
    /// <typeparam name="TId">The type of the unique identifier of an entity to be deleted.</typeparam>
    public interface IDelete<TEntity, TId>
    {
        /// <summary>Delete a collection of entities.</summary>
        /// <param name="entities">A collection of entities to be deleted.</param>
        /// <returns>
        /// A task which will eventually yield a collection of unique identifiers corresponding to the
        /// entities successfully deleted.
        /// </returns>
        Task<IEnumerable<TId>> DeleteAsync(IEnumerable<TEntity> entities);
    }
}