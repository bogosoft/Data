using System;
using System.Data;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IDataRecord"/> contract.
    /// </summary>
    public static class IDataRecordExtensions
    {
        /// <summary>
        /// Locate a field by its name and return the name of the data type associated with it.
        /// </summary>
        /// <param name="record">The current <see cref="IDataRecord"/> implementation.</param>
        /// <param name="name">The name of the field to locate.</param>
        /// <returns>The name of the data type associated with the specified field.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data record.
        /// </exception>
        public static string GetDataTypeName(this IDataRecord record, string name)
        {
            return record.GetDataTypeName(record.GetOrdinal(name));
        }

        /// <summary>
        /// Locate a field by its name and return the data type information associated with it.
        /// </summary>
        /// <param name="record">The current <see cref="IDataRecord"/> implementation.</param>
        /// <param name="name">The name of the field to locate.</param>
        /// <returns>Data type information associated with the specified field.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data record.
        /// </exception>
        public static Type GetFieldType(this IDataRecord record, string name)
        {
            return record.GetFieldType(record.GetOrdinal(name));
        }

        /// <summary>
        /// Locate a field by its name and return its value.
        /// </summary>
        /// <param name="record">The current <see cref="IDataRecord"/> implementation.</param>
        /// <param name="name">The name of the field to locate.</param>
        /// <returns>The value of the specified field.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data record.
        /// </exception>
        public static object GetValue(this IDataRecord record, string name)
        {
            return record.GetValue(record.GetOrdinal(name));
        }

        /// <summary>
        /// Locate a field by its name and get a value indicating whether or not its value is null.
        /// </summary>
        /// <param name="record">The current <see cref="IDataRecord"/> implementation.</param>
        /// <param name="name">The name of the field to locate.</param>
        /// <returns>True if the value of the specified field is null; false otherwise.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown in the event that the given field name does not correspond to any field in the current data record.
        /// </exception>
        public static bool IsDBNull(this IDataRecord record, string name)
        {
            return record.IsDBNull(record.GetOrdinal(name));
        }
    }
}