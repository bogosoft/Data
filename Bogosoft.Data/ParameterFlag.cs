using System;

namespace Bogosoft.Data
{
    /// <summary>
    /// A set of flags for describing additional characteristics of parameters.
    /// </summary>
    [Flags]
    public enum ParameterFlag
    {
        /// <summary>
        /// Parameter is not flagged for any additional characteristics.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// The value of the parameter is defined as a fixed length type.
        /// </summary>
        FixedLength = 0x01,

        /// <summary>
        /// The value of the parameter is a Unicode-encoded text type.
        /// </summary>
        Unicode = 0x02
    }
}