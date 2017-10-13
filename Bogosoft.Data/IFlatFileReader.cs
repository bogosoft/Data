using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a template for a type capable of iterating over the lines of a flat file.
    /// </summary>
    public interface IFlatFileReader : IEnumerable<string[]>
    {
        /// <summary>
        /// Read the next line and parse it as fields into a given buffer.
        /// </summary>
        /// <param name="buffer">
        /// A buffer which will receive parsed fields.
        /// </param>
        /// <returns>
        /// A value indicating whether or not fields were parsed.
        /// </returns>
        bool NextLine(string[] buffer);

        /// <summary>
        /// Read the next line and parse it as fields into a given buffer.
        /// </summary>
        /// <param name="buffer">
        /// A buffer which will receive parsed fields.
        /// </param>
        /// <param name="token">A cancellation instruction.</param>
        /// <returns>
        /// A value indicating whether or not fields were parsed.
        /// </returns>
        Task<bool> NextLineAsync(string[] buffer, CancellationToken token);
    }
}