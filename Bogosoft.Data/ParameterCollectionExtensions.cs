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
        /// Have the current collection create a new parameter, add it to the collection,
        /// and return it.
        /// </summary>
        /// <param name="collection">The current <see cref="IParameterCollection"/> implementation.</param>
        /// <param name="name">The name to be assigned to the newly created parameter.</param>
        /// <param name="flags">
        /// A value corresponding to one or more flags to set on the newly created parameter.
        /// </param>
        /// <returns>The newly created parameter.</returns>
        public static IParameter Add(
            this IParameterCollection collection,
            string name,
            ParameterFlag flags = ParameterFlag.None
            )
        {
            var parameter = collection.Create();

            parameter.Flags = flags;

            parameter.Name = name;

            return collection.Add(parameter);
        }

        /// <summary>
        /// Have the current collection create a new parameter, add it to the collection,
        /// and return it.
        /// </summary>
        /// <param name="collection">The current <see cref="IParameterCollection"/> implementation.</param>
        /// <param name="name">The name to be assigned to the newly created parameter.</param>
        /// <param name="size">
        /// A value corresponding to the maximum length, in bytes, that the value can have at the data source.
        /// </param>
        /// <param name="flags">
        /// A value corresponding to one or more flags to set on the newly created parameter.
        /// </param>
        /// <returns>The newly created parameter.</returns>
        public static IParameter Add(
            this IParameterCollection collection,
            string name,
            int size,
            ParameterFlag flags = ParameterFlag.None
            )
        {
            var parameter = collection.Create();

            parameter.Flags = flags;

            parameter.Name = name;

            return collection.Add(parameter);
        }

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