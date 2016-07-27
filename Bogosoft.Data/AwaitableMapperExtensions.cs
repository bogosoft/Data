using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>Extensions to the Bogosoft.Data.IAwaitableMapper interface.</summary>
    public static class AwaitableMapperExtensions
    {
        /// <summary>Map an input object to an output object.</summary>
        /// <typeparam name="TIn">The type of the input object.</typeparam>
        /// <typeparam name="TOut">The type of the output object.</typeparam>
        /// <param name="mapper">The current awaitable mapping strategy.</param>
        /// <param name="input">The object to map to an output object.</param>
        /// <returns>An object of the output type.</returns>
        public static TOut Map<TIn, TOut>(this IAwaitableMapper<TIn, TOut> mapper, TIn input)
        {
            TOut output = default(TOut);

            Task.Run(async () => output = await mapper.MapAsync(input)).Wait();

            return output;
        }
    }
}