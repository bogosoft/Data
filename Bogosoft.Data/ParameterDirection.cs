namespace Bogosoft.Data
{
    /// <summary>
    /// Indicates that direction of data flow between this parameter and a data source.
    /// </summary>
    public enum ParameterDirection
    {
        /// <summary>
        /// The associated parameter passes data to a data source.
        /// </summary>
        Input = 0,

        /// <summary>
        /// The associated parameter passes data to and from a data source.
        /// </summary>
        InputOutput,

        /// <summary>
        /// The associated parameter passes data back from a data source.
        /// </summary>
        Output,

        /// <summary>
        /// The associated parameter passes back the result of a stored procedure
        /// or function executed against a data source.
        /// </summary>
        ReturnValue
    }
}