using System;
using System.Data;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the <see cref="DataColumnCollection"/> class.</summary>
    public static class DataColumnCollectionExtensions
    {
        /// <summary>
        /// Creates and adds a <see cref="DataColumn"/> object that has the specified name
        /// and type to the <see cref="DataColumnCollection"/>. 
        /// </summary>
        /// <typeparam name="T">The DataColumn.DataType of the new column.</typeparam>
        /// <param name="columns">The current <see cref="DataColumnCollection"/>.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The newly-created <see cref="DataColumn"/>.</returns>
        public static DataColumn Add<T>(this DataColumnCollection columns, String columnName)
        {
            return columns.Add(columnName, typeof(T));
        }
    }
}