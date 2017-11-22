using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Bogosoft.Data
{
    abstract class DataReaderBase : DbDataReader, IEnumerable<IDataRecord>
    {
        public override object this[int ordinal] => GetValue(ordinal);

        public override object this[string name] => GetValue(GetOrdinal(name));

        public override bool GetBoolean(int ordinal) => GetFieldValue<bool>(ordinal);

        public override byte GetByte(int ordinal) => GetFieldValue<byte>(ordinal);

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal) => GetFieldValue<char>(ordinal);

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal) => GetFieldType(ordinal).Name;

        public override DateTime GetDateTime(int ordinal) => GetFieldValue<DateTime>(ordinal);

        public override decimal GetDecimal(int ordinal) => GetFieldValue<decimal>(ordinal);

        public override double GetDouble(int ordinal) => GetFieldValue<double>(ordinal);

        public override IEnumerator GetEnumerator()
        {
            while (Read())
            {
                yield return this;
            }
        }

        IEnumerator<IDataRecord> IEnumerable<IDataRecord>.GetEnumerator()
        {
            while (Read())
            {
                yield return this;
            }
        }

        public override T GetFieldValue<T>(int ordinal) => (T)GetValue(ordinal);

        public override float GetFloat(int ordinal) => GetFieldValue<float>(ordinal);

        public override Guid GetGuid(int ordinal) => GetFieldValue<Guid>(ordinal);

        public override short GetInt16(int ordinal) => GetFieldValue<short>(ordinal);

        public override int GetInt32(int ordinal) => GetFieldValue<int>(ordinal);

        public override long GetInt64(int ordinal) => GetFieldValue<long>(ordinal);

        public override string GetString(int ordinal) => GetFieldValue<string>(ordinal);

        public override int GetValues(object[] values)
        {
            var length = values.Length > FieldCount ? FieldCount : values.Length;

            for (var i = 0; i < length; i++)
            {
                values[i] = GetValue(i);
            }

            return length;
        }
    }
}