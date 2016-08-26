using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the <see cref="ISearch{TEntity, TParameters}"/> contract.</summary>
    public static class SearchExtensions
    {
        /// <summary>
        /// Get a paged search result for entities with a given set of parameters synchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities to search for.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters to search against.</typeparam>
        /// <param name="repository">
        /// The current <see cref="ISearch{TEntity, TParameters}"/> implementation.
        /// </param>
        /// <param name="parameters">A set of parameters to search against.</param>
        /// <param name="page">The page number of results to return.</param>
        /// <param name="limit">The number of results per page.</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> Search<TEntity, TParameters>(
            this ISearch<TEntity, TParameters> repository,
            TParameters parameters,
            Int32 page = 0,
            Int32 limit = Int32.MaxValue
            )
        {
            IEnumerable<TEntity> entities = null;

            Task.Run(async () => entities = await repository.SearchAsync(parameters, page, limit)).Wait();

            return entities ?? new TEntity[0];
        }

        /// <summary>
        /// Copy a paged search result of a source <see cref="ISearch{TEntity, TParameters}"/> implementation
        /// to a destination <see cref="ISave{TEntity, TId}"/> implementation. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities to be searched/stored.</typeparam>
        /// <typeparam name="TId">The type of the entities' unique identifier.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters to search against.</typeparam>
        /// <param name="source">The current <see cref="ISearch{TEntity, TParameters}"/> implementation.</param>
        /// <param name="destination">A target <see cref="ISave{TEntity, TId}"/> implementation.</param>
        /// <param name="parameters">Parameters to search for entities against.</param>
        /// <param name="page">The page of results to search/store.</param>
        /// <param name="limit">The number of results per page.</param>
        /// <returns>
        /// A <see cref="Task"/> which will eventually return a collection of the unique identifiers
        /// corresponding to the successfully-saved entities.
        /// </returns>
        public static IEnumerable<TId> CopyTo<TEntity, TId, TParameters>(
            this ISearch<TEntity, TParameters> source,
            ISave<TEntity, TId> destination,
            TParameters parameters,
            Int32 page = 0,
            Int32 limit = Int32.MaxValue
            )
        {
            IEnumerable<TId> ids = null;

            Task.Run(async () => ids = await source.CopyToAsync(destination, parameters, page, limit)).Wait();

            return ids ?? new TId[0];
        }

        /// <summary>
        /// Copy a paged search result of a source <see cref="ISearch{TEntity, TParameters}"/> implementation
        /// to a destination <see cref="ISave{TEntity, TId}"/> implementation. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities to be searched/stored.</typeparam>
        /// <typeparam name="TId">The type of the entities' unique identifier.</typeparam>
        /// <typeparam name="TParameters">The type of the parameters to search against.</typeparam>
        /// <param name="source">The current <see cref="ISearch{TEntity, TParameters}"/> implementation.</param>
        /// <param name="destination">A target <see cref="ISave{TEntity, TId}"/> implementation.</param>
        /// <param name="parameters">Parameters to search for entities against.</param>
        /// <param name="page">The page of results to search/store.</param>
        /// <param name="limit">The number of results per page.</param>
        /// <returns>
        /// A collection of the unique identifiers corresponding to the successfully-saved entities.
        /// </returns>
        public static async Task<IEnumerable<TId>> CopyToAsync<TEntity, TId, TParameters>(
            this ISearch<TEntity, TParameters> source,
            ISave<TEntity, TId> destination,
            TParameters parameters,
            Int32 page = 0,
            Int32 limit = Int32.MaxValue
            )
        {
            return await destination.SaveAsync(await source.SearchAsync(parameters, page, limit));
        }
    }
}