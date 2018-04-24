using System;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents an adapter between an object and a field which will extract its
    /// value from one of the associated object's properties.
    /// </summary>
    /// <typeparam name="T">The type of the object from which a field value will be extracted.</typeparam>
    public class FieldAdapter<T>
    {
        Func<T, object> extractor;

        /// <summary>
        /// Get the name of the current field.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the data type of the values in the current field.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Create a new instance of the <see cref="FieldAdapter{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the adapted field.</param>
        /// <param name="value">A constant value to use for the value of the field.</param>
        public FieldAdapter(string name, object value)
        {
            Name = name;
            Type = value.GetType();

            extractor = x => value;
        }

        /// <summary>
        /// Create a new instance of the <see cref="FieldAdapter{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the adapted field.</param>
        /// <param name="type">The data type of the values in the adapted field.</param>
        /// <param name="extractor">
        /// A strategy for extracting a field value from objects of the specified type.
        /// </param>
        public FieldAdapter(string name, Type type, Func<T, object> extractor)
        {
            Name = name;
            Type = type;

            this.extractor = extractor;
        }

        /// <summary>
        /// Extract a value for the current field from a given object.
        /// </summary>
        /// <param name="object">An object from which a value is to be extracted.</param>
        /// <returns>A value extracted from the given object.</returns>
        public object ExtractValueFrom(T @object) => extractor.Invoke(@object);
    }

    /// <summary>
    /// Represents an adapter between an object and a field which will extract its
    /// value from one of the associated object's properties.
    /// </summary>
    /// <typeparam name="TInitial">The type of the object from which a field value will be extracted.</typeparam>
    /// <typeparam name="TComputed">
    /// The type of the object which is extracted, i.e. the data type of the values in the adapted field.
    /// .</typeparam>
    public sealed class FieldAdapter<TInitial, TComputed> : FieldAdapter<TInitial>
    {
        /// <summary>
        /// Create a new instance of the <see cref="FieldAdapter{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the adapted field.</param>
        /// <param name="value">A constant value to use for the adapted field.</param>
        public FieldAdapter(string name, TComputed value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="FieldAdapter{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the adapted field.</param>
        /// <param name="extractor">
        /// A strategy for extracting a field value from objects of the specified type.
        /// </param>
        public FieldAdapter(string name, Func<TInitial, TComputed> extractor)
            : base(name, typeof(TComputed), x => extractor.Invoke(x))
        {
        }
    }
}