using Bogosoft.Collections.Async;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a result from a command executed against a data source.
    /// </summary>
    public interface IResult : ITraversable<IRow>
    {
        /// <summary>
        /// Get a value corresponding to the number of fields contained in the current result.
        /// </summary>
        int FieldCount { get; }

        /// <summary>
        /// Get a value indicating whether or not the current result contains rows.
        /// </summary>
        bool HasRows { get; }

        /// <summary>
        /// Get a value indicating the number of records affected by the portion of an executed command
        /// represented by the current result.
        /// </summary>
        ulong RecordsAffected { get; }
    }
}