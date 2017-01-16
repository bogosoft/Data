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

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="bool"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddBoolenParameter(
            this IParameterCollection parameters,
            string name,
            bool value
            )
        {
            var parameter = parameters.AddBooleanParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing an array of <see cref="byte"/> as its value.
        /// The size of the parameter will be set equal to the size of the given array.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns></returns>
        public static IParameter AddByteArrayParameter(
            this IParameterCollection parameters,
            string name,
            byte[] value
            )
        {
            var parameter = parameters.AddByteArrayParameter(name, value.Length);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing an array of <see cref="byte"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <param name="size">
        /// A value corresponding to the maximum size, in bytes, that the value of the current
        /// parameter can be at the data source.
        /// </param>
        /// <returns>A parameter.</returns>
        public static IParameter AddByteArrayParameter(
            this IParameterCollection parameters,
            string name,
            byte[] value,
            int size
            )
        {
            var parameter = parameters.AddByteArrayParameter(name, size);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="byte"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddByteParameter(
            this IParameterCollection parameters,
            string name,
            byte value
            )
        {
            var parameter = parameters.AddByteParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="System.Data.DataTable"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="table">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDataTableParameter(
            this IParameterCollection parameters,
            string name,
            System.Data.DataTable table
            )
        {
            var parameter = parameters.AddDataTableParameter(name);

            parameter.Value = table;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="DateTimeOffset"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDateTimeOffsetParameter(
            this IParameterCollection parameters,
            string name,
            DateTimeOffset value
            )
        {
            var parameter = parameters.AddDateTimeOffsetParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="DateTime"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDateTimeParameter(
            this IParameterCollection parameters,
            string name,
            DateTime value
            )
        {
            var parameter = parameters.AddDateTimeParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="decimal"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDecimalParameter(
            this IParameterCollection parameters,
            string name,
            decimal value
            )
        {
            var parameter = parameters.AddDecimalParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="decimal"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDecimalParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale
            )
        {
            var parameter = parameters.AddDecimalParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="decimal"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDecimalParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale,
            decimal value
            )
        {
            var parameter = parameters.AddDecimalParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="double"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDoubleParameter(
            this IParameterCollection parameters,
            string name,
            double value
            )
        {
            var parameter = parameters.AddDoubleParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="double"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDoubleParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale
            )
        {
            var parameter = parameters.AddDoubleParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="double"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddDoubleParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale,
            double value
            )
        {
            var parameter = parameters.AddDoubleParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="float"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddFloatParameter(
            this IParameterCollection parameters,
            string name,
            float value
            )
        {
            var parameter = parameters.AddFloatParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="float"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <returns>A parameter.</returns>
        public static IParameter AddFloatParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale
            )
        {
            var parameter = parameters.AddFloatParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a fixed length <see cref="string"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <param name="size">
        /// A value corresponding to the maximum length that the value can have at the data source.
        /// </param>
        /// <returns></returns>
        public static IParameter AddFixedLengthStringParameter(
            this IParameterCollection parameters,
            string name,
            string value,
            int size
            )
        {
            var parameter = parameters.AddFixedLengthStringParameter(name, size);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="float"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="precision">
        /// A value corresponding to the maximum number of digits used to represent the value.
        /// </param>
        /// <param name="scale">
        /// A value corresponding to the number of decimal places to which the value is resolved.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddFloatParameter(
            this IParameterCollection parameters,
            string name,
            byte precision,
            byte scale,
            float value
            )
        {
            var parameter = parameters.AddFloatParameter(name);

            parameter.Precision = precision;
            parameter.Scale = scale;
            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="Guid"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddGuidParameter(
            this IParameterCollection parameters,
            string name,
            Guid value
            )
        {
            var parameter = parameters.AddGuidParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="short"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddInt16Parameter(
            this IParameterCollection parameters,
            string name,
            short value
            )
        {
            var parameter = parameters.AddInt16Parameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="int"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddInt32Parameter(
            this IParameterCollection parameters,
            string name,
            int value
            )
        {
            var parameter = parameters.AddInt32Parameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="long"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddInt64Parameter(
            this IParameterCollection parameters,
            string name,
            long value
            )
        {
            var parameter = parameters.AddInt64Parameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="object"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddObjectParameter(
            this IParameterCollection parameters,
            string name,
            object value
            )
        {
            var parameter = parameters.AddObjectParameter(name);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="string"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <param name="size">
        /// A value corresponding to the maximum length that the value can have at the data source.
        /// </param>
        /// <returns>A parameter.</returns>
        public static IParameter AddStringParameter(
            this IParameterCollection parameters,
            string name,
            string value,
            int size
            )
        {
            var parameter = parameters.AddStringParameter(name, size);

            parameter.Value = value;

            return parameter;
        }

        /// <summary>
        /// Create and return a parameter capable of storing a <see cref="TimeSpan"/> as its value.
        /// </summary>
        /// <param name="parameters">
        /// The current <see cref="IParameterCollection"/> implementation.
        /// </param>
        /// <param name="name">
        /// A value corresponding to the name to be given to the newly created parameter.
        /// </param>
        /// <param name="value">The initial value of the newly created parameter.</param>
        /// <returns>A parameter.</returns>
        public static IParameter AddTimeSpanParameter(
            this IParameterCollection parameters,
            string name,
            TimeSpan value
            )
        {
            var parameter = parameters.AddTimeSpanParameter(name);

            parameter.Value = value;

            return parameter;
        }
    }
}