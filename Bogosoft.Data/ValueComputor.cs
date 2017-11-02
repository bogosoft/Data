using System.Data;

namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a template for a method capapble of computing a value from a given data record.
    /// </summary>
    /// <typeparam name="T">The type of the computed value.</typeparam>
    /// <param name="record">
    /// A data record from which a value may be computed.
    /// </param>
    /// <returns>A computed value.</returns>
    public delegate T ValueComputor<T>(IDataRecord record);
}