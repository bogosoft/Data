using System;

namespace Bogosoft.Data
{
    /// <summary>A generically-typed range of values.</summary>
    /// <typeparam name="T">The type of the constrained values.</typeparam>
    public class Range<T>
    {
        /// <summary>
        /// Get or set whether the boundary values of the current <see cref="Range{T}"/> are inclusive.
        /// </summary>
        public Boolean Inclusive { get; set; }

        /// <summary>Get or set the maximum value of the current <see cref="Range{T}"/>.</summary>
        public T Max { get; set; }

        /// <summary>Get or set the minimum value of the current <see cref="Range{T}"/>.</summary>
        public T Min { get; set; }
    }
}