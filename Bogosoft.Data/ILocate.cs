using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Provides a means of locating an entity by a unique identifier.</summary>
    /// <typeparam name="TEntity">The type of the entity to be located.</typeparam>
    /// <typeparam name="TId">The type of the entity's unique identifier.</typeparam>
    public interface ILocate<TEntity, TId>
    {
        /// <summary>Locate an entity by its unique identifier.</summary>
        /// <param name="id">The unique identifier by which to locate an entity.</param>
        /// <returns>A task which will eventually return an object of the entity type.</returns>
        Task<TEntity> LocateAsync(TId id);
    }
}