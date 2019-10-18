using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class AsyncCollectionToDataReaderAdapter<T> : CollectionToDataReaderAdapterBase<T>
    {
        readonly IAsyncEnumerator<T> source;

        internal AsyncCollectionToDataReaderAdapter(
            object[] buffer,
            Dictionary<string, int> fieldIndicesByName,
            FieldAdapter<T>[] fields,
            DataTable schemaTable,
            IAsyncEnumerable<T> source,
            CancellationToken token = default
            )
            : base(buffer, fieldIndicesByName, fields, schemaTable)
        {
            this.source = source.GetAsyncEnumerator(token);
        }

        protected override void Dispose(bool disposing)
        {
            DisposeAsync().GetAwaiter().GetResult();
        }

        public override ValueTask DisposeAsync() => source?.DisposeAsync() ?? new ValueTask();

        public override bool Read()
        {
            return ReadAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        public override async Task<bool> ReadAsync(CancellationToken token)
        {
            if (!token.IsCancellationRequested && await source.MoveNextAsync())
            {
                for (var i = 0; i < Fields.Length; i++)
                {
                    Buffer[i] = Fields[i].ExtractValueFrom(source.Current);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}