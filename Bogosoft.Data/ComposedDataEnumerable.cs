using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// An enumerable data reader whose principal operations of connecting to a data source, generating executable
    /// commands and mapping rows in a data reader to objects of a specified type are accomplished by delegates.
    /// </summary>
    /// <typeparam name="TConnection">The type of the database connection.</typeparam>
    /// <typeparam name="TCommand">The type of the database command to be executed.</typeparam>
    /// <typeparam name="TReader">The type of the data reader.</typeparam>
    /// <typeparam name="TEntity">The type of the objects in the sequence.</typeparam>
    public sealed class ComposedDataEnumerable<TConnection, TCommand, TReader, TEntity> : IEnumerable<TEntity>
        where TConnection : DbConnection
        where TCommand : DbCommand
        where TReader : DbDataReader
    {
        Func<TConnection, TCommand> commandProvider;
        Func<TConnection> connector;
        Func<TReader, TEntity> mapper;

        /// <summary>
        /// Create a new instance of the composed data enumerable class.
        /// </summary>
        /// <param name="connector">A delegate responsible for creating a data source connection.</param>
        /// <param name="commandProvider">
        /// A delegate responsible for generating an executable command against a given connection.
        /// </param>
        /// <param name="mapper">
        /// A strategy for mapping values from each row of a data reader to objects of the specified type.
        /// </param>
        public ComposedDataEnumerable(
            Func<TConnection> connector,
            Func<TConnection, TCommand> commandProvider,
            Func<TReader, TEntity> mapper
            )
        {
            this.commandProvider = commandProvider;
            this.connector = connector;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get an object capable of enumerating over the current sequence.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            TCommand command = null;
            TConnection connection = null;
            TReader reader = null;

            try
            {
                connection = connector.Invoke();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command = commandProvider.Invoke(connection);

                reader = command.ExecuteReader() as TReader;
            }
            catch (Exception)
            {
                reader?.Dispose();
                command?.Dispose();
                connection?.Dispose();

                throw;
            }

            return new DataEnumerator<TConnection, TCommand, TReader, TEntity>(
                connection,
                command,
                reader,
                mapper
                );
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}