using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class CollectionToDataReaderAdapter<T> : SimplifiedDataReaderBase
    {
        object[] buffer;
        Dictionary<string, int> fieldIndicesByName = new Dictionary<string, int>();
        FieldAdapter<T>[] fields;
        DataTable schemaTable;
        IEnumerator<T> source;

        public override object this[int ordinal] => buffer[ordinal];

        public override int Depth => 0;

        public override int FieldCount => fields.Length;

        public override bool HasRows => true;

        public override bool IsClosed => source is null;

        public override int RecordsAffected => 0;

        internal CollectionToDataReaderAdapter(IEnumerator<T> source, FieldAdapter<T>[] fields)
        {
            buffer = new object[fields.Length];

            this.fields = fields;

            schemaTable = new DataTable();

            for (var i = 0; i < buffer.Length; i++)
            {
                fieldIndicesByName[fields[i].Name] = i;

                schemaTable.Columns.Add(fields[i].Name, fields[i].Type);
            }

            this.source = source;
        }

        public override void Close()
        {
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            source.Dispose();

            source = null;
        }

        public override Type GetFieldType(int ordinal) => fields[ordinal].Type;

        public override string GetName(int ordinal) => fields[ordinal].Name;

        public override int GetOrdinal(string name) => fieldIndicesByName[name];

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
            if (source.MoveNext())
            {
                for (var i = 0; i < fields.Length; i++)
                {
                    buffer[i] = fields[i].ExtractValueFrom(source.Current);
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