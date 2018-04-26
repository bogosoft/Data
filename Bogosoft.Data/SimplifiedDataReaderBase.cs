using System;
using System.Collections;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// A partial implementation of the <see cref="DbDataReader"/> type that reduces the number
    /// of methods and properties deriving classes need to implement.
    /// </summary>
    public abstract class SimplifiedDataReaderBase : DbDataReader
    {
        /// <summary>
        /// Locate a field in the current row by a given name and return its value.
        /// </summary>
        /// <param name="name">A value corresponding to the name of a field.</param>
        /// <returns>The value of the named field.</returns>
        public override object this[string name] => GetValue(GetOrdinal(name));

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a boolean.</returns>
        public override bool GetBoolean(int ordinal) => GetFieldValue<bool>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a byte.</returns>
        public override byte GetByte(int ordinal) => GetFieldValue<byte>(ordinal);

        /// <summary>
        /// Read a stream of bytes from from the specified column, starting at the location indicated by a given
        /// data offset, into a buffer, starting at the location indicated by a given buffer offset.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a column to read from.
        /// </param>
        /// <param name="dataOffset">The index from within the row to begin the read operation.</param>
        /// <param name="buffer">The buffer into which data is to be copied.</param>
        /// <param name="bufferOffset">The position within the buffer to begin the write operation.</param>
        /// <param name="length">The maximum number of bytes to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return GetValue(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a char.</returns>
        public override char GetChar(int ordinal) => GetFieldValue<char>(ordinal);

        /// <summary>
        /// Read a stream of characters from from the specified column, starting at the location indicated by a given
        /// data offset, into a buffer, starting at the location indicated by a given buffer offset.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a column to read from.
        /// </param>
        /// <param name="dataOffset">The index from within the row to begin the read operation.</param>
        /// <param name="buffer">The buffer into which data is to be copied.</param>
        /// <param name="bufferOffset">The position within the buffer to begin the write operation.</param>
        /// <param name="length">The maximum number of characters to read.</param>
        /// <returns>The actual number of characters read.</returns>
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return GetValue(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index)
        /// and return the name of its associated data type.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The data type name of the value associated with the specified field.</returns>
        public override string GetDataTypeName(int ordinal) => GetFieldType(ordinal).Name;

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a date-time.</returns>
        public override DateTime GetDateTime(int ordinal) => GetFieldValue<DateTime>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a decimal.</returns>
        public override decimal GetDecimal(int ordinal) => GetFieldValue<decimal>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a double.</returns>
        public override double GetDouble(int ordinal) => GetFieldValue<double>(ordinal);

        /// <summary>
        /// Get an object capable of enumerating over the current data reader.
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
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a float.</returns>
        public override float GetFloat(int ordinal) => GetFieldValue<float>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a GUID.</returns>
        public override Guid GetGuid(int ordinal) => GetFieldValue<Guid>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a short.</returns>
        public override short GetInt16(int ordinal) => GetFieldValue<short>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as an int.</returns>
        public override int GetInt32(int ordinal) => GetFieldValue<int>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a long.</returns>
        public override long GetInt64(int ordinal) => GetFieldValue<long>(ordinal);

        /// <summary>
        /// Locate a field in the current row by a given ordinal position (index) and return its value.
        /// </summary>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a field.
        /// </param>
        /// <returns>The value of the specified field as a string.</returns>
        public override string GetString(int ordinal) => GetFieldValue<string>(ordinal);

        /// <summary>
        /// Read a stream of data from from the specified column, starting at the location indicated by a given
        /// data offset, into a buffer, starting at the location indicated by a given buffer offset.
        /// </summary>
        /// <typeparam name="T">The type of the values to read into a given buffer.</typeparam>
        /// <param name="ordinal">
        /// A value corresponding to the ordinal position (index) of a column to read from.
        /// </param>
        /// <param name="dataOffset">The index from within the row to begin the read operation.</param>
        /// <param name="buffer">The buffer into which data is to be copied.</param>
        /// <param name="bufferOffset">The position within the buffer to begin the write operation.</param>
        /// <param name="length">The maximum number of units to read.</param>
        /// <returns>The actual number of units read.</returns>
        public abstract long GetValue<T>(int ordinal, long dataOffset, T[] buffer, int bufferOffset, int length);
    }
}