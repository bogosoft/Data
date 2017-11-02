using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for enumerable sequences of rows.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Prepend a given row to the current sequence of rows.
        /// </summary>
        /// <typeparam name="T">The type of the values in a row.</typeparam>
        /// <param name="rows">The current sequence of rows.</param>
        /// <param name="row">
        /// An additional row to prepend to the current sequence.
        /// </param>
        /// <returns>
        /// A new sequence of rows as the given row prepended to the current sequence.
        /// </returns>
        public static IEnumerable<T[]> Prepend<T>(this IEnumerable<T[]> rows, params T[] row)
        {
            yield return row;

            foreach (var r in rows)
            {
                yield return r;
            }
        }

        /// <summary>
        /// Append a given row to the curent sequence of rows.
        /// </summary>
        /// <typeparam name="T">The type of the values in a row.</typeparam>
        /// <param name="rows">The current sequence of rows.</param>
        /// <param name="row">
        /// An additional row to append to the current sequence.
        /// </param>
        /// <returns>
        /// A new sequence of rows as the given row appended to the current sequence.
        /// </returns>
        public static IEnumerable<T[]> Append<T>(this IEnumerable<T[]> rows, params T[] row)
        {
            foreach (var r in rows)
            {
                yield return r;
            }

            yield return row;
        }
    }
}