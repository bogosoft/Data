namespace Bogosoft.Data
{
    /// <summary>
    /// Represents the context within which the text of a command is interpreted.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// The text of the command is interpreted as a statement.
        /// </summary>
        Text = 0,

        /// <summary>
        /// The text of the associated command is interpreted as the name of a relation;
        /// i.e. table, view, etc.
        /// </summary>
        Relation,

        /// <summary>
        /// The text of the associated command is interpreted as the name of a stored routine.
        /// </summary>
        StoredProcedure
    }
}