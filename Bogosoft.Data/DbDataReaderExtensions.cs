using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="DbDataReader"/> type.
    /// </summary>
    public static class DbDataReaderExtensions
    {
        /// <summary>
        /// Return a new data reader that prepends a given value to the first ordinal
        /// position of every read row. Ordinal positions of the underlying data reader
        /// will be mapped as n + 1.
        /// </summary>
        /// <typeparam name="T">The type of the value to prepend.</typeparam>
        /// <param name="source">The current data reader.</param>
        /// <param name="name">
        /// A value corresponding to the name of the field to prepend.
        /// </param>
        /// <param name="value">A value to prepend.</param>
        /// <returns>
        /// The current data reader decorated with a value prepender.
        /// </returns>
        public static DbDataReader Prepend<T>(this DbDataReader source, string name, T value)
        {
            return new PrependingDataReader<T>(source, name, x => value);
        }

        /// <summary>
        /// Returns a new data reader that prepends the result of calling a given delegate
        /// to the first ordinal position of every read row. Ordinal positions of the
        /// underlying data reader will be mapped as n + 1.
        /// </summary>
        /// <typeparam name="T">The type of the value to prepend.</typeparam>
        /// <param name="source">The current data reader.</param>
        /// <param name="name">
        /// A value corresponding to the name of the field to prepend.
        /// </param>
        /// <param name="computor">A delegate which will provide the value to prepend.</param>
        /// <returns>
        /// The current data reader decorated with a value provider prepender.
        /// </returns>
        public static DbDataReader Prepend<T>(this DbDataReader source, string name, ValueComputor<T> computor)
        {
            return new PrependingDataReader<T>(source, name, computor);
        }
    }
}