using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    static class StreamReaderExtensions
    {
        internal static Task<string> ReadLineAsync(this StreamReader reader, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return reader.ReadLineAsync();
        }
    }
}