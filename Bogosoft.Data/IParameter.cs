namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a parameter for use with a data source command.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Get or set the direction of the current parameter.
        /// </summary>
        ParameterDirection Direction { get; set; }

        /// <summary>
        /// Get or set the name of the current parameter.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get or set the maximum number of digits used to represent the <see cref="Value"/>.
        /// </summary>
        byte Precision { get; set; }

        /// <summary>
        /// Get or set the number of decimal places to which the <see cref="Value"/> is resolved.
        /// </summary>
        byte Scale { get; set; }

        /// <summary>
        /// Get or set the maximum size, in bytes, that the value of the current
        /// parameter can be at the data source.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Get a value corresponding to the capability of the current command to stream data to the data source.
        /// </summary>
        StreamingCapability StreamingCapabilities { get; }

        /// <summary>
        /// Get or set the value of the current parameter.
        /// </summary>
        object Value { get; set; }
    }
}