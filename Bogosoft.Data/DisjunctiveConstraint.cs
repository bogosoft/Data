using System;

namespace Bogosoft.Data
{
    internal sealed class DisjunctiveConstraint<T> : IConstrain<T>
    {
        private IConstrain<T> left;
        private IConstrain<T> right;

        internal DisjunctiveConstraint(IConstrain<T> left, IConstrain<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public Boolean Validate(T graph)
        {
            return this.left.Validate(graph) || this.right.Validate(graph);
        }
    }
}