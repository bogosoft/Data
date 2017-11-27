using System;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    sealed class ParsingDataReader : DataReaderBase
    {
        object[] buffer;
        string[] columns;
        Parser[] parsers;
        IEnumerator<string[]> records;

        public override int Depth => 0;

        public override int FieldCount => columns.Length;

        public override bool HasRows => true;

        public override bool IsClosed => false;

        public override int RecordsAffected => -1;

        internal ParsingDataReader(
            IEnumerator<string[]> records,
            string[] columns,
            Parser[] parsers
            )
        {
            if (parsers.Length < columns.Length)
            {
                throw new InvalidOperationException(Messages.TooFewParsers);
            }

            buffer = new object[columns.Length];

            this.columns = columns;
            this.parsers = parsers;
            this.records = records;
        }

        public override Type GetFieldType(int ordinal) => buffer[ordinal].GetType();

        public override string GetName(int ordinal) => columns[ordinal];

        public override int GetOrdinal(string name)
        {
            for (var i = 0; i < columns.Length; i++)
            {
                if (columns[i] == name)
                {
                    return i;
                }
            }

            return -1;
        }

        public override object GetValue(int ordinal) => buffer[ordinal];

        public override bool IsDBNull(int ordinal) => ReferenceEquals(null, buffer[ordinal]);

        public override bool NextResult() => false;

        public override bool Read()
        {
            if (records.MoveNext())
            {
                for (var i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = parsers[i].Invoke(records.Current[i]);
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