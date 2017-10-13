using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Bogosoft.Data
{
    /// <summary>
    /// An implementation of the <see cref="DbDataReader"/> class capable of
    /// reading over a sequence of rows as string arrays.
    /// </summary>
    public sealed class RowSequenceDataReader : DbDataReader
    {
        /// <summary>
        /// Create a new instance of the <see cref="RowSequenceDataReader"/> class
        /// and use the values on the first line as column names.
        /// </summary>
        /// <param name="rows">A sequence of rows.</param>
        /// <returns>A new data reader.</returns>
        public static RowSequenceDataReader WithFieldNamesOnFirstLine(IEnumerable<string[]> rows)
        {
            var enumerator = rows.GetEnumerator();

            var columns = enumerator.MoveNext() ? enumerator.Current : new string[0];

            return new RowSequenceDataReader(enumerator, columns);
        }

        IEnumerator<string[]> enumerator;
        string[] fields;

        /// <summary>
        /// Get or set a strategy for decoding a string value into an array of bytes.
        /// By default, this is set to <see cref="Convert.FromBase64String(string)"/>.
        /// </summary>
        public Converter<string, byte[]> ByteDecoder = Convert.FromBase64String;

        /// <summary>
        /// Get a dictionary of column data with keys as field names
        /// and values as the ordinal position of a field.
        /// </summary>
        public readonly IDictionary<string, int> Columns;

        /// <summary>
        /// Get the value of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field.</returns>
        public override object this[int ordinal] => fields[ordinal];

        /// <summary>
        /// Get the value of a field in the current row by its name.
        /// </summary>
        /// <param name="name">The name of a field to retrieve.</param>
        /// <returns>The value of the specified field.</returns>
        public override object this[string name] => fields[Columns[name]];

        /// <summary>
        /// Get the depth of the current data reader. Note: always returns zero.
        /// </summary>
        public override int Depth => 0;

        /// <summary>
        /// Get the number of fields in the current data reader.
        /// </summary>
        public override int FieldCount => Columns.Count;

        /// <summary>
        /// Get a value indicating whether or not the current data reader has rows.
        /// Note: always returns true.
        /// </summary>
        public override bool HasRows => true;

        /// <summary>
        /// Get a value indicating whether or not the current data reader has been closed.
        /// </summary>
        public override bool IsClosed => enumerator == null;

        /// <summary>
        /// Get or set a value indicating whether or not an empty string
        /// qualifies as being equivalent to <see cref="DBNull.Value"/>.
        /// </summary>
        public bool NullIfEmpty = true;

        /// <summary>
        /// Get the number of records affected by the source of the current reader.
        /// Note: always returns zero.
        /// </summary>
        public override int RecordsAffected => 0;

        /// <summary>
        /// Create a new instance of the <see cref="RowSequenceDataReader"/> class.
        /// </summary>
        /// <param name="enumerator">
        /// An enumerator to use as the source of rows for the new reader.
        /// </param>
        /// <param name="names">
        /// A sequence of names to use as column names. The names will be assigned to ordinal
        /// positions of fields in the order they are given.
        /// </param>
        public RowSequenceDataReader(IEnumerator<string[]> enumerator, IEnumerable<string> names)
        {
            Columns = new Dictionary<string, int>();

            var i = 0;

            foreach (var name in names)
            {
                Columns[name] = i++;
            }

            this.enumerator = enumerator;
        }

        /// <summary>
        /// Create a new instance of the <see cref="RowSequenceDataReader"/> class.
        /// </summary>
        /// <param name="rows">A sequence of rows as string arrays.</param>
        /// <param name="names">
        /// A sequence of names to use as column names. The names will be assigned to ordinal
        /// positions of fields in the order they are given.
        /// </param>
        public RowSequenceDataReader(IEnumerable<string[]> rows, IEnumerable<string> names)
            : this(rows.GetEnumerator(), names)
        {
        }

        /// <summary>
        /// Dispose of any unmanaged (and optionally) managed resources.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether or not managed resources are to be disposed of as well.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            enumerator?.Dispose();

            enumerator = null;
        }

        /// <summary>
        /// Get the value of a field as a boolean in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a boolean.</returns>
        public override bool GetBoolean(int ordinal) => bool.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a byte in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a byte.</returns>
        public override byte GetByte(int ordinal) => byte.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as an array of bytes in the current row by its ordinal position.
        /// The string value of the specified field will be decoded into a byte array by the
        /// strategy stored in the <see cref="ByteDecoder"/> property.
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
            var decoded = ByteDecoder.Invoke(fields[ordinal]);

            long i = 0;

            while (true)
            {
                if (i + dataOffset >= decoded.Length || i >= length)
                {
                    break;
                }

                buffer[i + bufferOffset] = decoded[i + dataOffset];
            }

            return i;
        }

        /// <summary>
        /// Get the value of a field as a char in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a char.</returns>
        public override char GetChar(int ordinal) => char.Parse(fields[ordinal]);

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
            long i = 0;

            while (i < length)
            {
                buffer[i + bufferOffset] = fields[ordinal][(int)(i + dataOffset)];
            }

            return i;
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
        public override string GetDataTypeName(int ordinal) => typeof(object).Name;

        /// <summary>
        /// Get the value of a field as a datetime in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a datetime.</returns>
        public override DateTime GetDateTime(int ordinal) => DateTime.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a decimal in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a decimal.</returns>
        public override decimal GetDecimal(int ordinal) => decimal.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a double in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a double.</returns>
        public override double GetDouble(int ordinal) => double.Parse(fields[ordinal]);

        /// <summary>
        /// Get a data structure capable of enumerating over the current data reader.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public override IEnumerator GetEnumerator() => enumerator;

        /// <summary>
        /// Get the type of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// The type of the specified field. Note: always returns the type of <see cref="object"/>.
        /// </returns>
        public override Type GetFieldType(int ordinal) => typeof(object);

        /// <summary>
        /// Get the value of a field as a float in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a float.</returns>
        public override float GetFloat(int ordinal) => float.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a guid in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a guid.</returns>
        public override Guid GetGuid(int ordinal) => Guid.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a short in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of a specified field as a short.</returns>
        public override short GetInt16(int ordinal) => short.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as an int in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as an int.</returns>
        public override int GetInt32(int ordinal) => int.Parse(fields[ordinal]);

        /// <summary>
        /// Get the value of a field as a long in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a long.</returns>
        public override long GetInt64(int ordinal) => long.Parse(fields[ordinal]);

        /// <summary>
        /// Get the name of a field by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// A value corresponding to the name of a field at the specified ordinal position.
        /// </returns>
        public override string GetName(int ordinal) => Columns.First(x => x.Value == ordinal).Key;

        /// <summary>
        /// Get the ordinal position of a field by its name.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name of a field.
        /// </param>
        /// <returns>
        /// A value corresponding to the ordinal position of the named field.
        /// </returns>
        public override int GetOrdinal(string name) => Columns[name];

        /// <summary>
        /// Get the value of a field as a string in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field as a string.</returns>
        public override string GetString(int ordinal) => fields[ordinal];

        /// <summary>
        /// Get the value of a field in the current row by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>The value of the specified field.</returns>
        public override object GetValue(int ordinal) => fields[ordinal];

        /// <summary>
        /// Populate a given array with values from the current row.
        /// </summary>
        /// <param name="values">An array to populate with values.</param>
        /// <returns>
        /// A value corresponding to the number of values copied.
        /// </returns>
        public override int GetValues(object[] values)
        {
            var i = 0;

            while (i < fields.Length)
            {
                values[i] = fields[i];
            }

            return i;
        }

        /// <summary>
        /// Determine if a field in the current row is equivalent to <see cref="DBNull.Value"/>
        /// by its ordinal position.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position of a field within the current row.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the value of a field at the specified
        /// ordinal position is equivalent to <see cref="DBNull.Value"/>.
        /// </returns>
        public override bool IsDBNull(int ordinal)
        {
            return fields[ordinal] == null || fields[ordinal].Length == 0 && NullIfEmpty;
        }

        /// <summary>
        /// Advance the current data reader to the next result set.
        /// </summary>
        /// <returns>
        /// A value indicating whether or not rows can be read after advancing.
        /// Note: always returns false.
        /// </returns>
        public override bool NextResult() => false;

        /// <summary>
        /// Advance the current data reader to the next (or first) row of data.
        /// </summary>
        /// <returns>
        /// A value indicating whether or not data is present at the new position.
        /// </returns>
        public override bool Read()
        {
            if (!enumerator.MoveNext())
            {
                return false;
            }

            fields = enumerator.Current;

            return true;
        }
    }
}