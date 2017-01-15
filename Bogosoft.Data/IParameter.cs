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
        /// Get or set one or more flags on the current parameter.
        /// </summary>
        ParameterFlag Flags { get; set; }

        /// <summary>
        /// Get or set the name of the current parameter.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get or set the maximum number of digits used to represent the <see cref="Value"/>.
        /// </summary>
        int Precision { get; set; }

        /// <summary>
        /// Get or set the number of decimal places to which the <see cref="Value"/> is resolved.
        /// </summary>
        int Scale { get; set; }

        /// <summary>
        /// Get or set the maximum size, in bytes, that the value of the current
        /// parameter can be at the data source.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Get or set the value of the current parameter.
        /// </summary>
        object Value { get; set; }
    }
}