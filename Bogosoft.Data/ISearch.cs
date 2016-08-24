using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Provides a means of searching for entities with a given set of parameters.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to search for.</typeparam>
    /// <typeparam name="TParameters">The type of the parameters to search against.</typeparam>
    public interface ISearch<TEntity, TParameters>
    {
        /// <summary>Get a paged search result for entities with a given set of parameters.</summary>
        /// <param name="parameters">A set of parameters to search against.</param>
        /// <param name="page">The page number of results to return.</param>
        /// <param name="limit">The number of results per page.</param>
        /// <returns>
        /// A task which will eventually return a collection of entities.
        /// Implementers SHOULD return an empty collection if no entities were found.
        /// </returns>
        Task<IEnumerable<TEntity>> SearchAsync(
            TParameters parameters,
            Int32 page = 0,
            Int32 limit = Int32.MaxValue
            );
    }
}