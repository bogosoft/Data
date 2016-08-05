using System;

namespace Bogosoft.Data
{
    /// <summary>Provides a means of validating an object graph against a constraint.</summary>
    /// <typeparam name="T">The type of the object graph targetted.</typeparam>
    public interface IConstrain<T>
    {
        /// <summary>Validate an object graph against the current constraint.</summary>
        /// <param name="graph">An object graph to validate.</param>
        /// <returns>True if the give object graph passes the current constraint; false otherwise.</returns>
        Boolean Validate(T graph);
    }
}