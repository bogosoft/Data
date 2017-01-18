using DataTable = System.Data.DataTable;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IBulkWrite"/> contract.
    /// </summary>
    public static class BulkWriteExtensions
    {
        /// <summary>
        /// Bulk write the contents of a <see cref="DataTable"/> to the data source.
        /// </summary>
        /// <param name="writer">The current <see cref="IBulkWrite"/> implementation.</param>
        /// <param name="table">A data table.</param>
        /// <returns>A task which represents the asynchronous operation.</returns>
        public static async Task WriteAsync(this IBulkWrite writer, DataTable table)
        {
            await writer.WriteAsync(table, CancellationToken.None);
        }

        /// <summary>
        /// Bulk write the contents of a data reader to the data source.
        /// </summary>
        /// <param name="writer">The current <see cref="IBulkWrite"/> implementation.</param>
        /// <param name="reader">A data reader.</param>
        /// <returns>A task which represents the asynchronous operation.</returns>
        public static async Task WriteAsync(this IBulkWrite writer, IReader reader)
        {
            await writer.WriteAsync(reader, CancellationToken.None);
        }
    }
}