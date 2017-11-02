using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    /// <summary>
    /// A configurable implementation of the <see cref="IFlatFileReader"/> contract that conforms
    /// to standard flat file formats by default. This class cannot be inherited.
    /// </summary>
    public sealed class StandardFlatFileReader : IFlatFileReader
    {
        char[] buffer;
        StreamReader reader;

        /// <summary>
        /// Get or set the character which identifies a field separator.
        /// </summary>
        public char FieldSeparator = ',';

        /// <summary>
        /// Get or set a strategy for determining whether a field
        /// is considered to be a null value.
        /// </summary>
        public Func<string, bool> NullIf = x => x.Length == 0;

        /// <summary>
        /// Get or set the character that identifies when a quoted sequence of characters begins.
        /// This character will be escaped if the character immediately after it is the same.
        /// </summary>
        public char QuoteChar = '"';

        /// <summary>
        /// Create a new instance of the <see cref="StandardFlatFileReader"/> class.
        /// </summary>
        /// <param name="reader">A text source.</param>
        /// <param name="bufferSize">
        /// A value corresponding to the size a buffer will be created with.
        /// </param>
        public StandardFlatFileReader(StreamReader reader, int bufferSize = 2048)
        {
            buffer = new char[bufferSize];

            this.reader = reader;
        }

        /// <summary>
        /// Get a data structure capable of enumerating over the current reader.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<string[]> GetEnumerator()
        {
            if (reader.EndOfStream)
            {
                yield break;
            }

            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                yield break;
            }

            var fields = line.GetFields(buffer, QuoteChar, FieldSeparator).ToArray();

            yield return fields;

            while (NextLine(fields))
            {
                yield return fields;
            }
        }

        /// <summary>
        /// Read the next line and parse it as fields into a given buffer.
        /// </summary>
        /// <param name="buffer">
        /// A buffer which will receive parsed fields.
        /// </param>
        /// <returns>
        /// A value indicating whether or not fields were parsed.
        /// </returns>
        public bool NextLine(string[] buffer)
        {
            if (reader.EndOfStream)
            {
                return false;
            }

            var i = 0;

            foreach(var field in reader.ReadLine().GetFields(this.buffer, QuoteChar, FieldSeparator))
            {
                buffer[i++] = NullIf.Invoke(field) ? null : field;
            }

            return true;
        }

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
        public async Task<bool> NextLineAsync(string[] buffer, CancellationToken token)
        {
            if (reader.EndOfStream)
            {
                return false;
            }

            var line = await reader.ReadLineAsync(token);

            var i = 0;

            foreach(var field in line.GetFields(this.buffer, QuoteChar, FieldSeparator))
            {
                buffer[i++] = NullIf.Invoke(field) ? null : field;
            }

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}