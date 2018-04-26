using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Bogosoft.Data
{
    class DataEnumerator<TConnection, TCommand, TReader, TEntity> : IEnumerator<TEntity>
        where TConnection : DbConnection
        where TCommand : DbCommand
        where TReader : DbDataReader
    {
        TCommand command;
        TConnection connection;
        Func<TReader, TEntity> mapper;
        TReader reader;

        public TEntity Current => mapper.Invoke(reader);

        object IEnumerator.Current => Current;

        internal DataEnumerator(TConnection connection, TCommand command, TReader reader, Func<TReader, TEntity> mapper)
        {
            this.command = command;
            this.connection = connection;
            this.mapper = mapper;
            this.reader = reader;
        }

        public void Dispose()
        {
            reader.Dispose();
            command.Dispose();
            connection.Dispose();
        }

        public bool MoveNext() => reader.Read();

        public void Reset()
        {
            reader.Dispose();

            reader = command.ExecuteReader() as TReader;
        }
    }
}