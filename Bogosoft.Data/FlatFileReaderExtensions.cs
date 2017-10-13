using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IFlatFileReader"/> contract.
    /// </summary>
    public static class FlatFileReaderExtensions
    {
        /// <summary>
        /// Read the next line and parse it as fields into a given buffer.
        /// </summary>
        /// <param name="reader">The current <see cref="IFlatFileReader"/> implementation.</param>
        /// <param name="buffer">
        /// A buffer which will receive parsed fields.
        /// </param>
        /// <returns>
        /// A value indicating whether or not fields were parsed.
        /// </returns>
        public static Task<bool> NextLineAsync(this IFlatFileReader reader, string[] buffer)
        {
            return reader.NextLineAsync(buffer, CancellationToken.None);
        }
    }
}