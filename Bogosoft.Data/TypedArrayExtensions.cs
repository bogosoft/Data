using System;

namespace Bogosoft.Data
{
    static class TypedArrayExtensions
    {
        internal static T[] Copy<T>(this T[] source) => source.Copy(source.Length);

        internal static T[] Copy<T>(this T[] source, int size)
        {
            var destination = new T[size];

            Array.Copy(source, destination, size);

            return destination;
        }
    }
}