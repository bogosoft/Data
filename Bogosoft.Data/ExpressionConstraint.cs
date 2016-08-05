using System;

namespace Bogosoft.Data
{
    /// <summary>A constraint the directly uses a given expression.</summary>
    /// <typeparam name="T">The type of object graph targetted.</typeparam>
    public sealed class ExpressionConstraint<T> : IConstrain<T>
    {
        private Func<T, Boolean> expression;

        /// <summary>Create a new instance with a given expression.</summary>
        /// <param name="expression">A validation expression.</param>
        public ExpressionConstraint(Func<T, Boolean> expression)
        {
            this.expression = expression;
        }

        /// <summary>Validate an object graph against the current constraint.</summary>
        /// <param name="graph">An object graph to validate.</param>
        /// <returns>True if the give object graph passes the current constraint; false otherwise.</returns>
        public Boolean Validate(T graph)
        {
            return this.expression.Invoke(graph);
        }
    }
}