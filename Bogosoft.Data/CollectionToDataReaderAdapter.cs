using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class CollectionToDataReaderAdapter<T> : SimplifiedDataReader
    {
        internal object[] Buffer;
        internal Dictionary<string, int> FieldIndicesByName = new Dictionary<string, int>();
        internal FieldAdapter<T>[] Fields;
        internal DataTable SchemaTable;
        internal IEnumerator<T> Source;

        public override object this[int ordinal] => Buffer[ordinal];

        public override int Depth => 0;

        public override int FieldCount => Fields.Length;

        public override bool HasRows => true;

        public override bool IsClosed => Source is null;

        public override int RecordsAffected => 0;

        public override void Close()
        {
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            Source.Dispose();

            Source = null;
        }

        public override Type GetFieldType(int ordinal) => Fields[ordinal].Type;

        public override string GetName(int ordinal) => Fields[ordinal].Name;

        public override int GetOrdinal(string name) => FieldIndicesByName[name];

        public override DataTable GetSchemaTable() => SchemaTable;

        public override object GetValue(int ordinal) => Buffer[ordinal];

        public override long GetValue<TUnit>(int ordinal, long dataOffset, TUnit[] buffer, int bufferOffset, int length)
        {
            var field = this.Buffer[ordinal] as TUnit[];

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

            if (len > Buffer.Length)
            {
                len = Buffer.Length;
            }

            for (var i = 0; i < len; i++)
            {
                values[i] = Buffer[i];
            }

            return len;
        }

        public override bool IsDBNull(int ordinal) => Buffer[ordinal] == DBNull.Value;

        public override bool NextResult() => false;

        public override bool Read()
        {
            if (Source.MoveNext())
            {
                for (var i = 0; i < Fields.Length; i++)
                {
                    Buffer[i] = Fields[i].ExtractValueFrom(Source.Current);
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