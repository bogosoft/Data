using System;
using System.Data.Common;

namespace Bogosoft.Data
{
    sealed class PrependingDataReader<T> : DataReaderDecoratorBase
    {
        T buffer;
        string name;
        ValueComputor<T> computor;

        public override int FieldCount => Source.FieldCount + 1;

        internal PrependingDataReader(DbDataReader source, string name, ValueComputor<T> computor)
            : base(source)
        {
            this.computor = computor;
            this.name = name;
        }

        public override Type GetFieldType(int ordinal) => typeof(T);

        public override string GetName(int ordinal) => ordinal == 0 ? name : Source.GetName(ordinal - 1);

        public override int GetOrdinal(string name) => name == this.name ? 0 : Source.GetOrdinal(name) + 1;

        public override object GetValue(int ordinal) => ordinal == 0 ? buffer : Source.GetValue(ordinal - 1);

        public override bool IsDBNull(int ordinal) => ReferenceEquals(null, buffer);

        public override bool Read()
        {
            if (Source.Read())
            {
                buffer = computor.Invoke(Source);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}