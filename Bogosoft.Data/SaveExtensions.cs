using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the Bogosoft.Data.ISave interface.</summary>
    public static class SaveExtensions
    {
        /// <summary>Save a collection of entities.</summary>
        /// <typeparam name="TEntity">The type of entity to be saved.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of an entity to be saved.</typeparam>
        /// <param name="repository">The current repository capable of saving entities.</param>
        /// <param name="entities">A collection of entities to be saved.</param>
        /// <returns>A collection of unique identifiers corresponding to the entities successfully saved.</returns>
        public static IEnumerable<TId> Save<TEntity, TId>(
            this ISave<TEntity, TId> repository,
            IEnumerable<TEntity> entities
            )
        {
            IEnumerable<TId> ids = null;

            Task.Run(async () => ids = await repository.SaveAsync(entities)).Wait();

            return ids ?? new TId[0];
        }

        /// <summary>Save a collection of entities.</summary>
        /// <typeparam name="TEntity">The type of entity to be saved.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of an entity to be saved.</typeparam>
        /// <param name="repository">The current repository capable of saving entities.</param>
        /// <param name="entities">A collection of entities to be saved.</param>
        /// <returns>A collection of unique identifiers corresponding to the entities successfully saved.</returns>
        public static IEnumerable<TId> Save<TEntity, TId>(
            this ISave<TEntity, TId> repository,
            params TEntity[] entities
            )
        {
            IEnumerable<TId> ids = null;

            Task.Run(async () => ids = await repository.SaveAsync(entities)).Wait();

            return ids ?? new TId[0];
        }

        /// <summary>Save a collection of entities.</summary>
        /// <typeparam name="TEntity">The type of entity to be saved.</typeparam>
        /// <typeparam name="TId">The type of the unique identifier of an entity to be saved.</typeparam>
        /// <param name="repository">The current repository capable of saving entities.</param>
        /// <param name="entities">A collection of entities.</param>
        /// <returns>
        /// A task which will eventually yield a collection of unique identifiers corresponding
        /// to successfully save entities.
        /// </returns>
        public static async Task<IEnumerable<TId>> SaveAsync<TEntity, TId>(
            this ISave<TEntity, TId> repository,
            params TEntity[] entities
            )
        {
            return await repository.SaveAsync(entities);
        }
    }
}