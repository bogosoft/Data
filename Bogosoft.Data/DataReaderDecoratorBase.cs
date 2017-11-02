using System;
using System.Collections;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// A partial implementation of the <see cref="DbDataReader"/> class intended to
    /// be used as a base for data reader decorators.
    /// </summary>
    public abstract class DataReaderDecoratorBase : DbDataReader
    {
        /// <summary>
        /// Get or set the source data reader associated with the current decorator.
        /// </summary>
        protected DbDataReader Source;

        /// <summary>
        /// Get the value of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field.</returns>
        public override object this[int ordinal] => GetValue(ordinal);

        /// <summary>
        /// Get the value of a field in the current row by its name.
        /// </summary>
        /// <param name="name">The name of a field to retrieve.</param>
        /// <returns>The value of the specified field.</returns>
        public override object this[string name] => GetValue(GetOrdinal(name));

        /// <summary>
        /// Get the depth of the current data reader. Note: always returns zero.
        /// </summary>
        public override int Depth => Source.Depth;

        /// <summary>
        /// When overridden in a derived class, gets the number of fields in the current data reader.
        /// </summary>
        public abstract override int FieldCount { get; }

        /// <summary>
        /// Get a value indicating whether or not the current data reader has rows.
        /// </summary>
        public override bool HasRows => Source.HasRows;

        /// <summary>
        /// Get a value indicating whether or not the current data reader has been closed.
        /// </summary>
        public override bool IsClosed => Source.IsClosed;

        /// <summary>
        /// Get the number of records affected by the source of the current reader.
        /// </summary>
        public override int RecordsAffected => Source.RecordsAffected;

        /// <summary>
        /// Create a new instance of the <see cref="DataReaderDecoratorBase"/> class.
        /// </summary>
        /// <param name="source">
        /// A source data reader that is to be decorated.
        /// </param>
        protected DataReaderDecoratorBase(DbDataReader source)
        {
            Source = source;
        }

        /// <summary>
        /// Get the value of a field as a boolean in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a boolean.</returns>
        public override bool GetBoolean(int ordinal) => GetFieldValue<bool>(ordinal);

        /// <summary>
        /// Get the value of a field as a byte in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a byte.</returns>
        public override byte GetByte(int ordinal) => GetFieldValue<byte>(ordinal);

        /// <summary>
        /// Get the value of a field as an array of bytes in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <param name="dataOffset">
        /// A value corresponding to the position within the decoded byte array from which to begin copying.
        /// </param>
        /// <param name="buffer">A destination byte array.</param>
        /// <param name="bufferOffset">
        /// A value corresponding to the position within the buffer to begin copying.
        /// </param>
        /// <param name="length">
        /// A value corresponding to the number of bytes to copy.
        /// </param>
        /// <returns>
        /// A value corresponding to the number of bytes copied. Depending upon the offsets and lengths
        /// of the arrays, this value may not equal the given length.
        /// </returns>
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the value of a field as a char in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a char.</returns>
        public override char GetChar(int ordinal) => GetFieldValue<char>(ordinal);

        /// <summary>
        /// Copy the value of a field as an array of chars in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <param name="dataOffset">
        /// A value corresponding to the position within the decoded byte array from which to begin copying.
        /// </param>
        /// <param name="buffer">A destination char array.</param>
        /// <param name="bufferOffset">
        /// A value corresponding to the position within the buffer to begin copying.
        /// </param>
        /// <param name="length">
        /// A value corresponding to the number of chars to copy.
        /// </param>
        /// <returns>
        /// A value corresponding to the number of chars copied. Depending upon the offsets and lengths
        /// of the arrays, this value may not equal the given length.
        /// </returns>
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the name of the data type of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// The name of the type of the specified field. Note: always returns the
        /// type name for <see cref="object"/>.
        /// </returns>
        public override string GetDataTypeName(int ordinal) => GetFieldType(ordinal).Name;

        /// <summary>
        /// Get the value of a field as a datetime in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a datetime.</returns>
        public override DateTime GetDateTime(int ordinal) => GetFieldValue<DateTime>(ordinal);

        /// <summary>
        /// Get the value of a field as a decimal in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a decimal.</returns>
        public override decimal GetDecimal(int ordinal) => GetFieldValue<decimal>(ordinal);

        /// <summary>
        /// Get the value of a field as a double in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a double.</returns>
        public override double GetDouble(int ordinal) => GetFieldValue<double>(ordinal);

        /// <summary>
        /// Get a data structure capable of enumerating over the current data reader.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public override IEnumerator GetEnumerator()
        {
            while (Read())
            {
                yield return this;
            }
        }

        /// <summary>
        /// Get the type of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// The type of the specified field.
        /// </returns>
        public abstract override Type GetFieldType(int ordinal);

        /// <summary>
        /// Get the value of a field as a specified type in the current row by its ordinal position.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as an object of the given type.</returns>
        public override T GetFieldValue<T>(int ordinal) => (T)GetValue(ordinal);

        /// <summary>
        /// Get the value of a field as a float in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a float.</returns>
        public override float GetFloat(int ordinal) => GetFieldValue<float>(ordinal);

        /// <summary>
        /// Get the value of a field as a guid in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a guid.</returns>
        public override Guid GetGuid(int ordinal) => GetFieldValue<Guid>(ordinal);

        /// <summary>
        /// Get the value of a field as a short in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of a specified field as a short.</returns>
        public override short GetInt16(int ordinal) => GetFieldValue<short>(ordinal);

        /// <summary>
        /// Get the value of a field as an int in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as an int.</returns>
        public override int GetInt32(int ordinal) => GetFieldValue<int>(ordinal);

        /// <summary>
        /// Get the value of a field as a long in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a long.</returns>
        public override long GetInt64(int ordinal) => GetFieldValue<long>(ordinal);

        /// <summary>
        /// When overridden in a derived class gets the name of a field by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// A value corresponding to the name of a field at the specified ordinal position.
        /// </returns>
        public abstract override string GetName(int ordinal);

        /// <summary>
        /// Get the ordinal position of a field by its name.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name of a field.
        /// </param>
        /// <returns>
        /// A value corresponding to the ordinal position of the named field.
        /// </returns>
        public abstract override int GetOrdinal(string name);

        /// <summary>
        /// Get the value of a field as a string in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a string.</returns>
        public override string GetString(int ordinal) => GetFieldValue<string>(ordinal);

        /// <summary>
        /// When overridden in a derived class, gets the value of
        /// a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field.</returns>
        public abstract override object GetValue(int ordinal);

        /// <summary>
        /// Populate a given array with values from the current row.
        /// </summary>
        /// <param name="values">An array to populate with values.</param>
        /// <returns>
        /// A value corresponding to the number of values copied.
        /// </returns>
        public override int GetValues(object[] values)
        {
            var length = values.Length > FieldCount ? FieldCount : values.Length;

            for (var i = 0; i < length; i++)
            {
                values[i] = GetValue(i);
            }

            return length;
        }

        /// <summary>
        /// When overridden in a derived class, determines if a field in the current row
        /// is equivalent to <see cref="DBNull.Value"/> by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the value of a field at the specified
        /// ordinal position is equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        public abstract override bool IsDBNull(int ordinal);

        /// <summary>
        /// Advance the current data reader to the next result set.
        /// </summary>
        /// <returns>
        /// A value indicating whether or not rows can be read after advancing.
        /// </returns>
        public override bool NextResult() => Source.NextResult();

        /// <summary>
        /// Advance the current data reader to the next (or first) row of data.
        /// </summary>
        /// <returns>
        /// A value indicating whether or not data is present at the new position.
        /// </returns>
        public override bool Read() => Source.Read();
    }
}