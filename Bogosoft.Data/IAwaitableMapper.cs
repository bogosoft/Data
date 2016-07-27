using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Provides a means of mapping an object of one type to another.</summary>
    /// <typeparam name="TIn">The type of the input object.</typeparam>
    /// <typeparam name="TOut">The type of the output object.</typeparam>
    public interface IAwaitableMapper<TIn, TOut>
    {
        /// <summary>Map an input object to an output object.</summary>
        /// <param name="input">The object to map to an output object.</param>
        /// <returns>A task which will eventually return an object of the output type.</returns>
        Task<TOut> MapAsync(TIn input);
    }
}