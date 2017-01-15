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
        /// Have the current collection create a parameter according to its own internal algorithm
        /// and return it. Implementations SHOULD NOT automatically add the newly created
        /// parameter to the current collection.
        /// </summary>
        /// <returns>A new, unassociated parameter.</returns>
        IParameter Create();
    }
}