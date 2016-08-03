using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the Bogosoft.Data.IDelete interface.</summary>
    public static class DeleteExtensions
    {
        /// <summary>Delete a collection of entities from the current repository.</summary>
        /// <typeparam name="TEntity">The type of entity to be deleted.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of an entity to be deleted.</typeparam>
        /// <param name="repository">The current repository capable of deleting entities.</param>
        /// <param name="entities">A collection of entities to be deleted.</param>
        /// <returns>A collection of unique identifier corresponding to the entities successfully deleted.</returns>
        public static IEnumerable<TId> Delete<TEntity, TId>(
            this IDelete<TEntity, TId> repository,
            IEnumerable<TEntity> entities
            )
        {
            IEnumerable<TId> ids = null;

            Task.Run(async () => ids = await repository.DeleteAsync(entities)).Wait();

            return ids ?? new TId[0];
        }

        /// <summary>Delete a collection of entities from the current repository.</summary>
        /// <typeparam name="TEntity">The type of entity to be deleted.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of an entity to be deleted.</typeparam>
        /// <param name="repository">The current repository capable of deleting entities.</param>
        /// <param name="entities">A collection of entities to be deleted.</param>
        /// <returns>A collection of unique identifiers corresponding to the entities successfully deleted.</returns>
        public static IEnumerable<TId> Delete<TEntity, TId>(
            this IDelete<TEntity, TId> repository,
            params TEntity[] entities
            )
        {
            IEnumerable<TId> ids = null;

            Task.Run(async () => ids = await repository.DeleteAsync(entities)).Wait();

            return ids ?? new TId[0];
        }

        /// <summary>Delete a collection of entities from the current repository.</summary>
        /// <typeparam name="TEntity">The type of the entity to be deleted.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of the entity to be deleted.</typeparam>
        /// <param name="repository">The current repository capable of deleting entities.</param>
        /// <param name="entities">A collection of entities to be deleted.</param>
        /// <returns>
        /// A task which will eventually yield a collection of unique identifiers corresponding
        /// to the unique identifiers of the entities successfully deleted.
        /// </returns>
        public static async Task<IEnumerable<TId>> DeleteAsync<TEntity, TId>(
            this IDelete<TEntity, TId> repository,
            params TEntity[] entities
            )
        {
            return await repository.DeleteAsync(entities);
        }
    }
}