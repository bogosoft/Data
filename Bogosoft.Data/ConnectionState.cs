using System;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents the current state of a database connection.
    /// </summary>
    [Flags]
    public enum ConnectionState
    {
        /// <summary>
        /// The database connection is closed.
        /// </summary>
        Closed = 0x00,

        /// <summary>
        /// The database connection is open.
        /// </summary>
        Open = 0x01,

        /// <summary>
        /// The database connection is in the process of connecting.
        /// </summary>
        Connecting = 0x02,

        /// <summary>
        /// The database connection is in the process of executing a command.
        /// </summary>
        Executing = 0x04,

        /// <summary>
        /// The database connection is in the process of fetching data from the data source.
        /// </summary>
        Fetching = 0x08,

        /// <summary>
        /// The database connection is broken.
        /// </summary>
        Broken = 0x10
    }
}