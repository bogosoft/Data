using System;

namespace Bogosoft.Data
{
    /// <summary>
    /// Describes the capabilities of streaming types of data to and from a data source.
    /// </summary>
    [Flags]
    public enum StreamingCapability
    {
        /// <summary>
        /// Data source is not capable of streaming any form of data.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Data source can stream raw binary data.
        /// </summary>
        Raw = 0x01,

        /// <summary>
        /// Data source can stream text data.
        /// </summary>
        Text = 0x02,

        /// <summary>
        /// Data source can stream XML data.
        /// </summary>
        Xml = 0x04,

        /// <summary>
        /// Data source can stream any form of data.
        /// </summary>
        All = 0x07
    }
}