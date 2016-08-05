using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the Bogosoft.Data.ILocate interface.</summary>
    public static class LocatorExtensions
    {
        /// <summary>Locate an entity by its unique identifier.</summary>
        /// <typeparam name="TEntity">The type of the entity to be located.</typeparam>
        /// <typeparam name="TId">The type of the entity's unique identifier.</typeparam>
        /// <param name="locator">The current locator strategy.</param>
        /// <param name="id">The unique identifier by which to locate an entity.</param>
        /// <returns>An object of the entity type.</returns>
        public static TEntity Locate<TEntity, TId>(this ILocate<TEntity, TId> locator, TId id)
        {
            TEntity entity = default(TEntity);

            Task.Run(async () => entity = await locator.LocateAsync(id)).Wait();

            return entity;
        }
    }
}