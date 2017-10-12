using System;
using System.Collections;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a buffer of information.
    /// </summary>
    /// <typeparam name="T">The type of the values being buffered.</typeparam>
    public sealed class Buffer<T> : IEnumerable<T>
    {
        internal class Enumerator : IEnumerator<T>
        {
            T[] data;
            int index = 0;
            int length;

            public T Current => data[index];

            object IEnumerator.Current => data[index];

            internal Enumerator(T[] data, int length)
            {
                this.data = data;
                this.length = length;
            }

            public void Dispose()
            {
            }

            public bool MoveNext() => index++ < length;

            public void Reset()
            {
                index = 0;
            }
        }

        T[] data;
        int length = 0;

        /// <summary>
        /// Get the value at the position corresponding to a given index.
        /// </summary>
        /// <param name="index">
        /// A value corresponding to the position to return.
        /// </param>
        /// <returns>A value of the buffered type.</returns>
        public T this[int index] => data[index];

        /// <summary>
        /// Create a new instance of the <see cref="Buffer{T}"/> class.
        /// </summary>
        /// <param name="size">
        /// A value corresponding to the size of the new buffer.
        /// </param>
        public Buffer(int size)
        {
            data = new T[size];
        }

        /// <summary>
        /// Flush the current buffer of its values, returning an array of the specified type
        /// and of a length equal to the length of the currently buffered data. This method
        /// will reset the buffer.
        /// </summary>
        /// <returns>An array of values of the specified type.</returns>
        public T[] Flush()
        {
            var copied = data.Copy(length);

            length = 0;

            return copied;
        }

        /// <summary>
        /// Flush the current buffer of its values, returning an object of a specified output type.
        /// </summary>
        /// <typeparam name="TOut">The type of the output.</typeparam>
        /// <param name="converter">
        /// A delegate that takes an array of values of the specified type as well as
        /// the number of values in the array.
        /// </param>
        /// <returns>An object of the output type.</returns>
        public TOut Flush<TOut>(Func<T[], int, TOut> converter)
        {
            var result = converter.Invoke(data, length);

            length = 0;

            return result;
        }

        /// <summary>
        /// Get a data structure capable of enumerating over the values of the current buffer.
        /// </summary>
        /// <returns>An enumerator of the values of the current buffer.</returns>
        public IEnumerator<T> GetEnumerator() => new Enumerator(data, length);

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(data, length);

        /// <summary>
        /// Push a value into the current buffer and advance its internal pointer.
        /// </summary>
        /// <param name="item">An object of the buffered type.</param>
        public void Push(T item)
        {
            if (length > data.Length)
            {
                throw new InvalidOperationException();
            }

            data[length++] = item;
        }
    }
}