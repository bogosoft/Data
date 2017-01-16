using System;
using System.Collections.Generic;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a collection of <see cref="IParameter"/> objects.
    /// </summary>
    public interface IParameterCollection : IEnumerable<IParameter>
    {
        /// <summary>
        /// Get the parameter from the current collection that corresponds to the given numerical index.
        /// </summary>
        /// <param name="index">
        /// A value corresponding to the zero-based index of the desired parameter.
        /// </param>
        /// <returns>The corresponding parameter.</returns>
        IParameter this[int index] { get; }

        /// <summary>
        /// Get the parameter from the current collection that corresponds to the given name.
        /// </summary>
        /// <param name="name">The name of the desired parameter.</param>
        /// <returns>The corresponding parameter.</returns>
        IParameter this[string name] { get; }

        /// <summary>
        /// Add a given parameter to the current parameter collection.
        /// </summary>
        /// <param name="parameter">A parameter to add to the current collection.</param>
        /// <returns>The added parameter.</returns>
        /// <exception cref="InvalidOperationException">
        /// Implementations SHOULD throw an <see cref="InvalidOperationException"/> in the event
        /// that the given parameter has the same name as a parameter that already exists in the
        /// current collection.
        /// </exception>
        IParameter Add(IParameter parameter);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="bool"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddBooleanParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing an array of <see cref="byte"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="size">
        /// A value corresponding to the maximum size, in bytes, that the value of the current
        /// parameter can be at the data source.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddByteArrayParameter(string name, int size);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="byte"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddByteParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="System.Data.DataTable"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddDataTableParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="DateTimeOffset"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddDateTimeOffsetParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="DateTime"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddDateTimeParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="decimal"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddDecimalParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="double"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddDoubleParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a fixed length <see cref="string"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="size">
        /// A value corresponding to the maximum length that the value can have at the data source.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddFixedLengthStringParameter(string name, int size);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="float"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddFloatParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="Guid"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddGuidParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="short"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddInt16Parameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="int"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddInt32Parameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="long"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddInt64Parameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing an <see cref="object"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddObjectParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="string"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="size">
        /// A value corresponding to the maximum length that the value can have at the data source.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddStringParameter(string name, int size);

        /// <summary>
        /// Create and return a parameter capable of storing a very large <see cref="string"/>.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddTextParameter(string name);

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="TimeSpan"/> as its value.
        /// </summary>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <returns>A parameter.</returns>
        IParameter AddTimeSpanParameter(string name);

        /// <summary>
        /// Have the current collection create a parameter according to its own internal algorithm
        /// and return it. Implementations SHOULD NOT automatically add the newly created
        /// parameter to the current collection.
        /// </summary>
        /// <returns>A new, unassociated parameter.</returns>
        IParameter Create();
    }
}