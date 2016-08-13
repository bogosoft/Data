using System;

namespace Bogosoft.Data
{
    /// <summary>A mapper that directly uses a given expression.</summary>
    /// <typeparam name="TIn">The type of the input object.</typeparam>
    /// <typeparam name="TOut">The type of the output object.</typeparam>
    public sealed class ExpressionMapper<TIn, TOut> : IMap<TIn, TOut>
    {
        private Func<TIn, TOut> expression;

        /// <summary>
        /// Create a new <see cref="ExpressionMapper{TIn, TOut}"/> with a given expression.
        /// </summary>
        /// <param name="expression">An expression of type, <see cref="Func{TIn, TOut}"/>.</param>
        public ExpressionMapper(Func<TIn, TOut> expression)
        {
            this.expression = expression;
        }

        /// <summary>Map an input object to an output object.</summary>
        /// <param name="input">The object to map to an output object.</param>
        /// <returns>An instance of an object of the output type.</returns>
        public TOut Map(TIn input)
        {
            return this.expression.Invoke(input);
        }
    }
}