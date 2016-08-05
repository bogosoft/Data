using System;

namespace Bogosoft.Data
{
    /// <summary>Extensions for the Bogosoft.Data.IConstrain interface.</summary>
    public static class ConstrainExtensions
    {
        /// <summary>Add a conjunctive (AND) constraint to the current constraint.</summary>
        /// <typeparam name="T">The type of the object graph targetted.</typeparam>
        /// <param name="left">The current constraint.</param>
        /// <param name="right">A constraint to serve as the right-hand side of the operation.</param>
        /// <returns>
        /// A conjunctive constraint consisting of the current constraint as the left-hand side
        /// of the operation and an additional constraint as the right-hand side.
        /// </returns>
        public static IConstrain<T> And<T>(this IConstrain<T> left, IConstrain<T> right)
        {
            return new ConjunctiveConstraint<T>(left, right);
        }

        /// <summary>Add a conjunctive (AND) constraint to the current constraint.</summary>
        /// <typeparam name="T">The type of the object graph targetted.</typeparam>
        /// <param name="constraint">The current constraint.</param>
        /// <param name="expression">
        /// A constraint (in the form of an expression) to serve as the right-hand
        /// side of the operation as an expression.
        /// </param>
        /// <returns>
        /// A conjunctive constraint consisting of the current constraint as the left-hand side
        /// of the operation and an additional constraint as the right-hand side.
        /// </returns>
        public static IConstrain<T> And<T>(this IConstrain<T> constraint, Func<T, Boolean> expression)
        {
            return new ConjunctiveConstraint<T>(constraint, new ExpressionConstraint<T>(expression));
        }

        /// <summary>Negate the current constraint.</summary>
        /// <typeparam name="T">The type of the object graph targetted.</typeparam>
        /// <param name="constraint">The current constraint.</param>
        /// <returns>A negation of the current constraint.</returns>
        public static IConstrain<T> Negate<T>(this IConstrain<T> constraint)
        {
            return new NegatedConstraint<T>(constraint);
        }

        /// <summary>Add a disjunctive (OR) constraint to the current constraint.</summary>
        /// <typeparam name="T">The type of the object graph targetted.</typeparam>
        /// <param name="left">The current constraint.</param>
        /// <param name="right">A constraint to serve as the right-hand side of the operation.</param>
        /// <returns>
        /// A disjunctive constraint consisting of the current constraint as the left-hand side
        /// of the operation and an additional constraint as the right-hand side.
        /// </returns>
        public static IConstrain<T> Or<T>(this IConstrain<T> left, IConstrain<T> right)
        {
            return new DisjunctiveConstraint<T>(left, right);
        }

        /// <summary>Add a disjunctive (OR) constraint to the current constraint.</summary>
        /// <typeparam name="T">The type of the object graph targetted.</typeparam>
        /// <param name="constraint">The current constraint.</param>
        /// <param name="expression">
        /// A constraint (in the form of an expression) to serve as the right-hand
        /// side of the operation as an expression.
        /// </param>
        /// <returns>
        /// A disjunctive constraint consisting of the current constraint as the left-hand side
        /// of the operation and an additional constraint as the right-hand side.
        /// </returns>
        public static IConstrain<T> Or<T>(this IConstrain<T> constraint, Func<T, Boolean> expression)
        {
            return new DisjunctiveConstraint<T>(constraint, new ExpressionConstraint<T>(expression));
        }
    }
}