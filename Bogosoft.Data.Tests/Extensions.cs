using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data.Tests
{
    static class Extensions
    {
        class SynchronousSequenceAdapter<T> : IAsyncEnumerable<T>
        {
            class Enumerator : IAsyncEnumerator<T>
            {
                readonly IEnumerator<T> source;

                public T Current => source.Current;

                internal Enumerator(IEnumerator<T> source)
                {
                    this.source = source;
                }

                public ValueTask DisposeAsync()
                {
                    source.Dispose();

                    return new ValueTask();
                }

                public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(source.MoveNext());
            }

            readonly IEnumerable<T> source;

            internal SynchronousSequenceAdapter(IEnumerable<T> source)
            {
                this.source = source;
            }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new Enumerator(source.GetEnumerator());
            }
        }

        internal static IAsyncEnumerable<T> AsAsync<T>(this IEnumerable<T> source)
        {
            return new SynchronousSequenceAdapter<T>(source);
        }
    }
}