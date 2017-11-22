namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a template for a method capable of parsing a string of data into an object.
    /// </summary>
    /// <param name="data">String data to parse.</param>
    /// <returns>A parsed object.</returns>
    public delegate object Parser(string data);
}