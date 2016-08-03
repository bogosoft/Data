using System;

namespace Bogosoft.Data
{
    internal sealed class NegatedConstraint<T> : IConstrain<T>
    {
        private IConstrain<T> constraint;

        internal NegatedConstraint(IConstrain<T> constraint)
        {
            this.constraint = constraint;
        }

        public Boolean Validate(T graph)
        {
            return false == this.constraint.Validate(graph);
        }
    }
}