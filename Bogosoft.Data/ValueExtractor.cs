namespace Bogosoft.Data
{
    /// <summary>
    /// Represents a template for any method capable of extracting a value from an object of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the object from which a value will be extracted.</typeparam>
    /// <param name="object">An object from which to extract a value.</param>
    /// <returns>
    /// An object corresponding to the value extracted from the given object.
    /// </returns>
    public delegate object ValueExtractor<T>(T @object);
}