using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    abstract class CollectionToDataReaderAdapterBase<T> : SimplifiedDataReader
    {
        bool closed = false;

        readonly Dictionary<string, int> fieldIndicesByName = new Dictionary<string, int>();
        readonly DataTable schemaTable;

        protected object[] Buffer { get; private set; }
        protected FieldAdapter<T>[] Fields { get; private set; }

        public override object this[int ordinal] => Buffer[ordinal];

        public override int Depth => 0;

        public override int FieldCount => Fields.Length;

        public override bool HasRows => true;

        public override bool IsClosed => closed;

        public override int RecordsAffected => 0;

        protected CollectionToDataReaderAdapterBase(
            object[] buffer,
            Dictionary<string, int> fieldIndicesByName,
            FieldAdapter<T>[] fields,
            DataTable schemaTable
            )
        {
            Buffer = buffer;
            this.fieldIndicesByName = fieldIndicesByName;
            Fields = fields;
            this.schemaTable = schemaTable;
        }

        public override void Close()
        {
            Dispose();

            closed = true;
        }

        protected abstract override void Dispose(bool disposing);

        public abstract override ValueTask DisposeAsync();

        public override Type GetFieldType(int ordinal) => Fields[ordinal].Type;

        public override string GetName(int ordinal) => Fields[ordinal].Name;

        public override int GetOrdinal(string name) => fieldIndicesByName[name];

        public override DataTable GetSchemaTable() => schemaTable;

        public override object GetValue(int ordinal) => Buffer[ordinal];

        public override long GetValue<TUnit>(
            int ordinal,
            long dataOffset,
            TUnit[] buffer,
            int bufferOffset,
            int length
            )
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

        public abstract override bool Read();

        public abstract override Task<bool> ReadAsync(CancellationToken token);
    }
}