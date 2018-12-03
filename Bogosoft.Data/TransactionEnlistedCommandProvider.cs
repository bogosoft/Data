using System;
using System.Data;
using System.Data.Common;

namespace Bogosoft.Data
{
    /// <summary>
    /// A command provider that automatically enlists generated commands in a transaction.
    /// </summary>
    /// <typeparam name="TConnection">The type of a database connection.</typeparam>
    /// <typeparam name="TCommand">The type of any commands that are generated.</typeparam>
    /// <typeparam name="TTransaction">The type of a transaction into which commands will be enlisted.</typeparam>
    public sealed class TransactionEnlistedCommandProvider<TConnection, TCommand, TTransaction>
        : ICommandProvider<TCommand>, IDisposable
        where TCommand : DbCommand
        where TConnection : DbConnection
        where TTransaction : DbTransaction
    {
        static TTransaction Begin(TConnection connection)
        {
            return connection.BeginTransaction() as TTransaction;
        }

        static void DisposeOfTransaction(TTransaction transaction)
        {
            transaction?.Dispose();
        }

        static void DoNothing(TTransaction transaction)
        {
        }

        readonly bool commitOnDispose;
        TConnection connection;
        readonly Action<TTransaction> disposer;
        bool initialized = false;
        TTransaction transaction;
        Func<TConnection, TTransaction> transactions;

        /// <summary>
        /// Create a new transaction-enlisted command provider. A transaction will be automatically generated,
        /// committed and disposed of by this instance.
        /// </summary>
        /// <param name="connection">
        /// An established database connection against which a transaction will be started and database commands will be generated.
        /// This connection will NOT be disposed of when this instance is disposed of.
        /// </param>
        /// <param name="commitOnDispose">
        /// A value indicating whether or not a transaction started against the given connection
        /// should be committed when this instance is disposed of.
        /// </param>
        public TransactionEnlistedCommandProvider(TConnection connection, bool commitOnDispose = true)
            : this(connection, Begin, commitOnDispose, DisposeOfTransaction)
        {
        }

        /// <summary>
        /// Create a new transaction-enlisted command provider.
        /// </summary>
        /// <param name="connection">
        /// An established database connection against which database commands will be generated.
        /// This connection will NOT be disposed of when this instance is disposed of.
        /// </param>
        /// <param name="transaction">
        /// An established database transaction into which newly generated commands will be enlisted.
        /// This transaction will NOT be disposed of when this instance is disposed of.
        /// </param>
        /// <param name="commitOnDispose">
        /// A value indicating whether or not a transaction started against the given connection
        /// should be committed when this instance is disposed of.
        /// </param>
        public TransactionEnlistedCommandProvider(TConnection connection, TTransaction transaction, bool commitOnDispose = true)
            : this(connection, c => transaction, commitOnDispose, DoNothing)
        {
        }

        TransactionEnlistedCommandProvider(
            TConnection connection,
            Func<TConnection, TTransaction> transactor,
            bool commitOnDispose,
            Action<TTransaction> disposer
            )
        {
            this.connection = connection;
            this.disposer = disposer;
            this.commitOnDispose = commitOnDispose;
            this.transactions = transactor;
        }

        /// <summary>
        /// Create an executable database command.
        /// </summary>
        /// <param name="commandText">The text of the newly generated command.</param>
        /// <param name="commandType">The type of the newly generated command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        public TCommand Create(string commandText, CommandType commandType)
        {
            if (!initialized)
            {
                transaction = transactions.Invoke(connection);

                initialized = true;
            }

            var command = connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = transaction;

            return command as TCommand;
        }

        /// <summary>
        /// Dispose of the current command provider and any disposable dependencies it manages.
        /// </summary>
        public void Dispose()
        {
            if (commitOnDispose)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            disposer.Invoke(transaction);
        }
    }
}