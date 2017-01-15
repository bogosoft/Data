namespace Bogosoft.Data
{
    /// <summary>
    /// Describes the intended results, and the possible effects on a data source, from the execution of a
    /// command. The values of this enumeration map directly to those values from
    /// <see cref="System.Data.CommandBehavior"/>.
    /// </summary>
    public enum CommandBehavior
    {
        /// <summary>
        /// The command may return multiple result sets and its execution may change
        /// the state of the data source.
        /// </summary>
        Default = 0x00,

        /// <summary>
        /// The command should return a single result set.
        /// </summary>
        SingleResult = 0x01,

        /// <summary>
        /// The command returns column information only.
        /// </summary>
        SchemaOnly = 0x02,

        /// <summary>
        /// The command returns column and primary key information only.
        /// </summary>
        KeyInfo = 0x04,

        /// <summary>
        /// The command should return a single row from the first result set, and its
        /// execution may change the state of the data source.
        /// </summary>
        SingleRow = 0x08,

        /// <summary>
        /// Indicates that results of command execution should use streams for large binary values.
        /// </summary>
        SequentialAccess = 0x10,

        /// <summary>
        /// Indicates that the data source connection associated with an executed command should
        /// close automatically when all results have been disposed of.
        /// </summary>
        CloseConnection = 0x20
    }
}