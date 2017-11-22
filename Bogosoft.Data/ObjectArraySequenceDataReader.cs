using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Data
{
    class ObjectArraySequenceDataReader : DataReaderBase
    {
        IEnumerator<object[]> records;
        string[] columns;

        public override int FieldCount => columns.Length;

        public override int Depth => 0;

        public override bool HasRows => true;

        public override bool IsClosed => false;

        public override int RecordsAffected => -1;

        internal ObjectArraySequenceDataReader(IEnumerator<object[]> records, IEnumerable<string> columns)
        {
            this.columns = columns.ToArray();
            this.records = records;
        }

        protected override void Dispose(bool disposing)
        {
            records?.Dispose();

            base.Dispose(disposing);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal) => records.Current[ordinal].GetType();

        public override string GetName(int ordinal) => columns[ordinal];

        public override int GetOrdinal(string name)
        {
            for (var i = 0; i < FieldCount; i++)
            {
                if (columns[i] == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public override object GetValue(int ordinal) => records.Current[ordinal];

        public override bool IsDBNull(int ordinal) => ReferenceEquals(null, records.Current[ordinal]);

        public override bool NextResult() => false;

        public override bool Read() => records.MoveNext();
    }
}