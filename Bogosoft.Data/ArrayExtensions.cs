using System.Collections.Generic;

namespace Bogosoft.Data
{
    static class ArrayExtensions
    {
        internal static IDictionary<T, int> ToIndices<T>(this IEnumerable<T> items)
        {
            var indices = new Dictionary<T, int>();

            var i = 0;

            foreach (var x in items)
            {
                indices[x] = i;
            }

            return indices;
        }
    }
}