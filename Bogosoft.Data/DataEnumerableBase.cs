using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// A partial implementation of an enumerable data reader which handles the connection, command and reader
    /// construction as well as automatic disposal.
    /// </summary>
    /// <typeparam name="TConnection">The type of the database connection.</typeparam>
    /// <typeparam name="TCommand">The type of the database command to be executed.</typeparam>
    /// <typeparam name="TReader">The type of the data reader.</typeparam>
    /// <typeparam name="TEntity">The type of the objects in the sequence.</typeparam>
    public abstract class DataEnumerableBase<TConnection, TCommand, TReader, TEntity> : IEnumerable<TEntity>
        where TCommand : DbCommand
        where TConnection : DbConnection
        where TReader : DbDataReader
    {
        /// <summary>
        /// When overridden in a derived class, builds a command off of a given database connection.
        /// </summary>
        /// <param name="connection">A database connection that can be used to build a new command.</param>
        /// <returns>An executable database command.</returns>
        protected abstract TCommand BuildCommand(TConnection connection);

        /// <summary>
        /// Get a new database connection.
        /// </summary>
        /// <returns>A new database connection.</returns>
        protected abstract TConnection Connect();

        /// <summary>
        /// Get an object capable of enumerating over the current sequence.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            TCommand command = null;
            TConnection connection = null;

            try
            {
                connection = Connect();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                command = BuildCommand(connection);
            }
            catch (Exception)
            {
                command?.Dispose();
                connection?.Dispose();
            }

            return new DataEnumerator<TConnection, TCommand, TReader, TEntity>(
                connection,
                command,
                command.ExecuteReader() as TReader,
                Map
                );
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Convert the current row in a given data reader into an object of the entity type.
        /// </summary>
        /// <param name="reader">A database reader.</param>
        /// <returns>An object of the entity type.</returns>
        protected abstract TEntity Map(TReader reader);
    }
}