using Bogosoft.Collections.Async;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class CollectionToDataReaderAdapter<T> : ExtendedDataReaderBase
    {
        object[] buffer;
        Dictionary<string, int> columnIndicesByName = new Dictionary<string, int>();
        Column<T>[] columns;
        DataTable schemaTable;
        object source;

        public override object this[int ordinal] => buffer[ordinal];

        public override int Depth => 0;

        public override int FieldCount => columns.Length;

        public override bool HasRows => true;

        public override bool IsClosed => source is null;

        public override int RecordsAffected => 0;

        internal CollectionToDataReaderAdapter(IAsyncEnumerator<T> source, Column<T>[] columns)
            : this(source as object, columns)
        {
        }

        internal CollectionToDataReaderAdapter(IEnumerator<T> source, Column<T>[] columns)
            : this(source as object, columns)
        {
        }

        CollectionToDataReaderAdapter(object source, Column<T>[] columns)
        {
            buffer = new object[columns.Length];

            this.columns = columns;

            schemaTable = new DataTable();

            for (var i = 0; i < buffer.Length; i++)
            {
                columnIndicesByName[columns[i].Name] = i;

                schemaTable.Columns.Add(columns[i].Name, columns[i].Type);
            }

            this.source = source;
        }

        public override void Close()
        {
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (source is IDisposable)
            {
                (source as IDisposable).Dispose();

                source = null;
            }
        }

        public override Type GetFieldType(int ordinal) => columns[ordinal].Type;

        public override string GetName(int ordinal) => columns[ordinal].Name;

        public override int GetOrdinal(string name) => columnIndicesByName[name];

        public override DataTable GetSchemaTable() => schemaTable;

        public override object GetValue(int ordinal) => buffer[ordinal];

        public override long GetValue<TUnit>(int ordinal, long dataOffset, TUnit[] buffer, int bufferOffset, int length)
        {
            var field = this.buffer[ordinal] as TUnit[];

            long readLength = length;

            if (dataOffset + length > field.Length)
            {
                readLength = dataOffset + length;
            }

            if (readLength + bufferOffset > buffer.Length)
            {
                readLength = buffer.Length;
            }

            for (var i = 0; i < readLength; i++)
            {
                buffer[bufferOffset + i] = field[dataOffset + i];
            }

            return readLength;
        }

        public override int GetValues(object[] values)
        {
            var len = values.Length;

            if (len > buffer.Length)
            {
                len = buffer.Length;
            }

            for (var i = 0; i < len; i++)
            {
                values[i] = buffer[i];
            }

            return len;
        }

        public override bool IsDBNull(int ordinal) => buffer[ordinal] == DBNull.Value;

        public override bool NextResult() => false;

        public override bool Read()
        {
            if (source is IEnumerator<T>)
            {
                return Read(source as IEnumerator<T>);
            }
            else if (source is IAsyncEnumerator<T>)
            {
                return ReadAsync(source as IAsyncEnumerator<T>, CancellationToken.None).GetAwaiter().GetResult();
            }
            else
            {
                return false;
            }
        }

        bool Read(IEnumerator<T> source)
        {
            if (source.MoveNext())
            {
                for (var i = 0; i < columns.Length; i++)
                {
                    buffer[i] = columns[i].ExtractValueFrom(source.Current);
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
            if (source is IAsyncEnumerator<T>)
            {
                return ReadAsync(source as IAsyncEnumerator<T>, token);
            }
            else if (source is IEnumerable<T>)
            {
                return Task.FromResult(!token.IsCancellationRequested && Read(source as IEnumerator<T>));
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        async Task<bool> ReadAsync(IAsyncEnumerator<T> source, CancellationToken token)
        {
            if (await source.MoveNextAsync(token))
            {
                for (var i = 0; i < columns.Length; i++)
                {
                    buffer[i] = columns[i].ExtractValueFrom(source.Current);
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