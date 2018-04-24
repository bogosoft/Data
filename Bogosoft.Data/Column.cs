using System;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a collection of metadata about a data column.
    /// </summary>
    /// <typeparam name="T">The type of the object from which a column value will be extracted.</typeparam>
    public class Column<T>
    {
        Func<T, object> extractor;

        /// <summary>
        /// Get the name of the current column.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the data type of the values in the current column.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Create a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the new column.</param>
        /// <param name="value">A constant value to use for the new column.</param>
        public Column(string name, object value)
        {
            Name = name;
            Type = value.GetType();

            extractor = x => value;
        }

        /// <summary>
        /// Create a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the new column.</param>
        /// <param name="type">The data type of the values in the new column.</param>
        /// <param name="extractor">
        /// A strategy for extracting a column value from objects of the specified type.
        /// </param>
        public Column(string name, Type type, Func<T, object> extractor)
        {
            Name = name;
            Type = type;

            this.extractor = extractor;
        }

        /// <summary>
        /// Extract a value for the current column from a given object.
        /// </summary>
        /// <param name="object">An object from which a value is to be extracted.</param>
        /// <returns>A value extracted from the given object.</returns>
        public object ExtractValueFrom(T @object) => extractor.Invoke(@object);
    }

    /// <summary>
    /// Represents a collection of metadata about a data column. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TInitial">The type of the object from which a column value will be extracted.</typeparam>
    /// <typeparam name="TComputed">The type of the object which is extracted.</typeparam>
    public sealed class Column<TInitial, TComputed> : Column<TInitial>
    {
        /// <summary>
        /// Create a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the new column.</param>
        /// <param name="value">A constant value to use for the new column.</param>
        public Column(string name, TComputed value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the new column.</param>
        /// <param name="extractor">
        /// A strategy for extracting a column value from objects of the specified type.
        /// </param>
        public Column(string name, Func<TInitial, TComputed> extractor)
            : base(name, typeof(TComputed), x => extractor.Invoke(x))
        {
        }
    }
}