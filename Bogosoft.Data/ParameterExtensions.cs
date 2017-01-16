using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Bogosoft.Data
{
    /// <summary>
    /// Extended functionality for the <see cref="IParameter"/> contract.
    /// </summary>
    public static class ParameterExtensions
    {
        /// <summary>
        /// Determine if the current parameter is capable of streaming raw binary data.
        /// </summary>
        /// <param name="parameter">The current <see cref="IParameter"/> implementation.</param>
        /// <returns>
        /// A value indicating whether or not the command associated with the current collection of
        /// parameters is capable of streaming raw binary data.
        /// </returns>
        public static bool CanStreamData(this IParameter parameter)
        {
            return 0 != (parameter.StreamingCapabilities & StreamingCapability.Raw);
        }

        /// <summary>
        /// Determine if the current parameter is capable of streaming text data.
        /// </summary>
        /// <param name="parameter">The current <see cref="IParameter"/> implementation.</param>
        /// <returns>
        /// A value indicating whether or not the current parameter can stream text data.
        /// </returns>
        public static bool CanStreamText(this IParameter parameter)
        {
            return 0 != (parameter.StreamingCapabilities & StreamingCapability.Text);
        }

        /// <summary>
        /// Determine if the current parameter is capable of streaming XML data.
        /// </summary>
        /// <param name="parameter">The current <see cref="IParameter"/> implementation.</param>
        /// <returns>
        /// A value indicating whether or not the current parameter can stream XML data.
        /// </returns>
        public static bool CanStreamXml(this IParameter parameter)
        {
            return 0 != (parameter.StreamingCapabilities & StreamingCapability.Xml);
        }

        /// <summary>
        /// Set the value of the current parameter to a streamable object. When reading, the
        /// <see cref="IParameter.Size"/> value will determine the number of bytes sent to
        /// the data source. If the underlying implementation does not allow the streaming of binary data,
        /// the given stream will first be converted to an array of bytes before being set as the
        /// value of the current parameter.
        /// </summary>
        /// <param name="parameter">The current <see cref="IParameter"/> implementation.</param>
        /// <param name="stream">A stream to set as the value of the current parameter.</param>
        /// <returns>The current parameter.</returns>
        public static IParameter Set(this IParameter parameter, Stream stream)
        {
            if (parameter.CanStreamData())
            {
                parameter.Value = stream;
            }
            else
            {
                using (var memory = new MemoryStream())
                {
                    var buffer = new byte[parameter.Size];

                    stream.Read(buffer, 0, buffer.Length);

                    memory.Write(buffer, 0, buffer.Length);

                    parameter.Value = memory.ToArray();
                }
            }

            return parameter;
        }

        /// <summary>
        /// Set the value of the current parameter to a <see cref="TextReader"/>. When reading, the
        /// <see cref="IParameter.Size"/> value will determine the number of chars sent to the data source.
        /// If the underlying implementation does not allow the streaming of text data, the given reader
        /// will read directly into the <see cref="IParameter.Value"/> property before being sent.
        /// </summary>
        /// <param name="parameter">The current <see cref="IParameter"/> implementation.</param>
        /// <param name="reader">A text reader.</param>
        /// <returns>The current parameter.</returns>
        public static IParameter Set(this IParameter parameter, TextReader reader)
        {
            if (parameter.CanStreamText())
            {
                parameter.Value = reader;
            }
            else
            {
                var buffer = new char[parameter.Size];

                reader.Read(buffer, 0, buffer.Length);

                parameter.Value = new string(buffer);
            }

            return parameter;
        }

        /// <summary>
        /// Set the value of the current parameter to a <see cref="XmlReader"/>. When reading, the
        /// <see cref="IParameter.Size"/> property will be ignored. If the actual implementation does
        /// not allow the streaming of XML data, the contents will first be serialized to a string before
        /// being assigned to the <see cref="IParameter.Value"/> property.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IParameter Set(this IParameter parameter, XmlReader reader)
        {
            if (parameter.CanStreamXml())
            {
                parameter.Value = reader;
            }
            else
            {
                parameter.Value = new XPathDocument(reader).CreateNavigator().OuterXml;
            }

            return parameter;
        }
    }
}