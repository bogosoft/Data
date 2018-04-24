using System;
using System.Collections.Generic;

namespace Bogosoft.Data.Tests
{
    static class IEnumerableExtensions
    {
        internal static int FirstIndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var index = 0;

            foreach (var item in items)
            {
                if (predicate.Invoke(item))
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        internal static IEnumerable<object[]> ToRecords<T>(this IEnumerable<T> items, FieldAdapter<T>[] fields)
        {
            var buffer = new object[fields.Length];

            foreach (var item in items)
            {
                for (var i = 0; i < fields.Length; i++)
                {
                    buffer[i] = fields[i].ExtractValueFrom(item);
                }

                yield return buffer;
            }
        }
    }
}