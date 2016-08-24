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
    }
}