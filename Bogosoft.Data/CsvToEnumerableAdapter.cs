using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Bogosoft.Data
{
    /// <summary>
    /// An adapter to make comma-separated value data enumerable.
    /// </summary>
    /// <typeparam name="T">The type of the enumerable value.</typeparam>
    [Obsolete("Use IFlatFileReader.Select( ... ) instead of this class.")]
    public class CsvToEnumerableAdapter<T> : IEnumerable<T>
    {
        internal class Enumerator : IEnumerator<T>
        {
            char[] buffer;
            Converter<string[], T> fieldMapper;
            int length = 0;
            string line;
            StreamReader reader;

            public T Current => fieldMapper(GetFields(line).ToArray());

            object IEnumerator.Current => Current;

            internal Enumerator(Converter<string[], T> fieldMapper, StreamReader reader, int bufferSize)
            {
                buffer = new char[bufferSize];

                this.fieldMapper = fieldMapper;
                this.reader = reader;
            }

            public void Dispose()
            {
            }

            IEnumerable<string> GetFields(string data)
            {
                var quoted = false;

                char c = '\0', p;

                using (var enumerator = data.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        p = c;

                        c = enumerator.Current;

                        if (c == '"')
                        {
                            quoted = !quoted;

                            if (p == '"')
                            {
                                buffer[length++] = '"';
                            }

                            continue;
                        }

                        if (!quoted && c == ',')
                        {
                            yield return new string(buffer, 0, length);

                            length = 0;
                        }
                        else
                        {
                            buffer[length++] = c;
                        }
                    }

                    if (length > 0)
                    {
                        yield return new string(buffer, 0, length);

                        length = 0;
                    }
                }
            }

            public bool MoveNext() => !reader.EndOfStream && (line = reader.ReadLine()).Length > 0;

            public void Reset()
            {
            }
        }

        /// <summary>
        /// Get or set a value corresponding to the size that a buffer will be set to upon creation.
        /// </summary>
        protected int BufferSize;

        /// <summary>
        /// Get or set a strategy for mapping an array of strings (fields)
        /// to an object of the specified type.
        /// </summary>
        protected Converter<string[], T> FieldMapper;

        /// <summary>
        /// Get or set the source of the comma-separated value data.
        /// </summary>
        protected StreamReader Reader;

        /// <summary>
        /// Create a new instance of the <see cref="CsvToEnumerableAdapter{T}"/> class.
        /// </summary>
        /// <param name="fieldMapper">
        /// A strategy for mapping an array of strings (fields)
        /// to an object of the specified type.
        /// </param>
        /// <param name="reader">A source of comma-separated value data.</param>
        /// <param name="bufferSize">
        /// A value corresponding to the size that a buffer will be set to upon creation.
        /// </param>
        public CsvToEnumerableAdapter(
            Converter<string[], T> fieldMapper,
            StreamReader reader,
            int bufferSize = 2048
            )
        {
            BufferSize = bufferSize;
            FieldMapper = fieldMapper;
            Reader = reader;
        }

        /// <summary>
        /// Get a structure capable of enumerating objects of the specified type.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator() => new Enumerator(FieldMapper, Reader, BufferSize);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}