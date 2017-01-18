namespace Bogosoft.Data
{
    /// <summary>
    /// Indicates the direction of data flow between this parameter and a data source. These values are
    /// a direct mapping to those in <see cref="System.Data.ParameterDirection"/>.
    /// </summary>
    public enum ParameterDirection
    {
        /// <summary>
        /// The associated parameter passes data to a data source.
        /// </summary>
        Input = 1,

        /// <summary>
        /// The associated parameter passes data back from a data source.
        /// </summary>
        Output = 2,

        /// <summary>
        /// The associated parameter passes data to and from a data source.
        /// </summary>
        InputOutput = 3,

        /// <summary>
        /// The associated parameter passes back the result of a stored procedure
        /// or function executed against a data source.
        /// </summary>
        ReturnValue = 6
    }
}