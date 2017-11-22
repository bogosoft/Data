using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Data
{
    class StringArraySequenceDataReader : DataReaderBase
    {
        string[] columns;
        IDictionary<string, int> indices;
        IEnumerator<string[]> records;

        public override int Depth => 0;

        public override int FieldCount => indices.Count;

        public override bool HasRows => true;

        public override bool IsClosed => false;

        public override int RecordsAffected => -1;

        internal StringArraySequenceDataReader(IEnumerator<string[]> records, IEnumerable<string> columns)
        {
            this.columns = columns.ToArray();

            indices = columns.ToIndices();

            this.records = records;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            records?.Dispose();
        }

        public override Type GetFieldType(int ordinal)
        {
            if (ordinal < 0 || ordinal >= indices.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return typeof(string);
        }

        public override string GetName(int ordinal) => indices.First(x => x.Value == ordinal).Key;

        public override int GetOrdinal(string name) => indices[name];

        public override object GetValue(int ordinal) => records.Current[ordinal];

        public override int GetValues(object[] values)
        {
            var i = 0;

            while (i < records.Current.Length)
            {
                values[i] = records.Current[i];
            }

            return i;
        }

        public override bool IsDBNull(int ordinal) => string.IsNullOrEmpty(records.Current[ordinal]);

        public override bool NextResult() => false;

        public override bool Read() => records.MoveNext();
    }
}