using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class SynchronousCollectionToDataReaderAdapter<T> : CollectionToDataReaderAdapterBase<T>
    {
        readonly IEnumerator<T> source;

        internal SynchronousCollectionToDataReaderAdapter(
            object[] buffer,
            Dictionary<string, int> fieldIndicesByName,
            FieldAdapter<T>[] fields,
            DataTable schemaTable,
            IEnumerable<T> source
            )
            : base(buffer, fieldIndicesByName, fields, schemaTable)
        {
            this.source = source.GetEnumerator();
        }

        protected override void Dispose(bool disposing)
        {
            source?.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Dispose();

            return new ValueTask();
        }

        public override bool Read()
        {
            if (source.MoveNext())
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

        public override Task<bool> ReadAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.FromResult(Read());
        }
    }
}