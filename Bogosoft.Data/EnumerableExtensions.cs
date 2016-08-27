using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the <see cref="IEnumerable{T}"/> contract.</summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Filters a sequence of values bases on a <see cref="IConstrain{T}"/> contract.
        /// </summary>
        /// <typeparam name="T">The type of the values in the sequence.</typeparam>
        /// <param name="items">The current <see cref="IEnumerable{T}"/> object.</param>
        /// <param name="qualifier">A filter to constrain the sequence by.</param>
        /// <returns>A filtered sequence.   </returns>
        public static IEnumerable<T> Where<T>(
            this IEnumerable<T> items,
            IConstrain<T> qualifier
            )
        {
            return items.Where(i => qualifier.Validate(i));
        }
    }
}