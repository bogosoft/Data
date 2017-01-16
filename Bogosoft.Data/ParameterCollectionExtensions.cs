using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IParameterCollection"/> contract.
    /// </summary>
    public static class ParameterCollectionExtensions
    {
        /// <summary>
        /// Add a collection of parameters to the current parameter collection.
        /// </summary>
        /// <param name="collection">The current <see cref="IParameterCollection"/> implementation.</param>
        /// <param name="parameters">Zero or more parameters to add as a collection.</param>
        public static void Add(this IParameterCollection collection, IEnumerable<IParameter> parameters)
        {
            foreach(var p in parameters)
            {
                collection.Add(p);
            }
        }
    }
}