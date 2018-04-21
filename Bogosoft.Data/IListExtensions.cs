using System;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for an <see cref="IList{T}"/> implementation.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Add a new computed column to the current list by providing its components separately.
        /// </summary>
        /// <typeparam name="T">The type of the object to be used when extracting a field value.</typeparam>
        /// <param name="columns">The current list of computed column definitions.</param>
        /// <param name="name">
        /// A value corresponding to the name of the column.
        /// </param>
        /// <param name="type">The data type of the values in the new column.</param>
        /// <param name="extractor">
        /// A strategy for extracting a column value from objects of the specified type.
        /// </param>
        public static IList<Column<T>> Add<T>(
            this IList<Column<T>> columns,
            string name,
            Type type,
            Func<T, object> extractor
            )
        {
            columns.Add(new Column<T>(name, type, extractor));

            return columns;
        }
    }
}