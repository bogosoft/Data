namespace Bogosoft.Data
{
    /// <summary>
    /// A set of messages related to data readers.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Get a message indicating that zero records were encountered where
        /// at least one record was expected.
        /// </summary>
        public const string NoRecords = "Sequence contains zero records.";

        /// <summary>
        /// Get a message indicating that the number of value extracts in a given
        /// sequence was less than the number of columns in a given sequence.
        /// </summary>
        public const string TooFewExtractors = "The number of given value extractors cannot be less thatn the number of given columns";

        /// <summary>
        /// Get a message indicating that the number of parsers in a given sequence was
        /// less than the number of columns in a given sequence.
        /// </summary>
        public const string TooFewParsers = "The number of given parsers cannot be less than the number of given columns";
    }
}