using System;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    sealed class TypedSequenceDataReader<T> : DataReaderBase
    {
        object[] buffer;
        string[] columns;
        ValueExtractor<T>[] extractors;
        IEnumerator<T> objects;

        public override int Depth => 0;

        public override int FieldCount => columns.Length;

        public override bool HasRows => true;

        public override bool IsClosed => false;

        public override int RecordsAffected => -1;

        internal TypedSequenceDataReader(
            IEnumerator<T> objects,
            string[] columns,
            ValueExtractor<T>[] extractors
            )
        {
            if (extractors.Length < columns.Length)
            {
                throw new InvalidOperationException(Messages.TooFewExtractors);
            }

            buffer = new object[columns.Length];

            this.columns = columns;
            this.extractors = extractors;
            this.objects = objects;
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
            if (objects.MoveNext())
            {
                for (var i = 0; i < extractors.Length; i++)
                {
                    buffer[i] = extractors[i].Invoke(objects.Current);
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