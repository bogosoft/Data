﻿using System;
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
    public class TransactionEnlistedCommandFactory<TConnection, TCommand, TTransaction>
        : ICommandFactory<TCommand>, IDisposable
        where TCommand : DbCommand
        where TConnection : DbConnection
        where TTransaction : DbTransaction
    {
        static void DisposeOfTransaction(TTransaction transaction)
        {
            transaction?.Dispose();
        }

        static void DoNothing(TTransaction transaction)
        {
        }

        readonly bool commitOnDispose;
        readonly TConnection connection;
        readonly Action<TTransaction> disposer;
        bool initialized = false;
        TTransaction transaction;
        readonly Func<TConnection, TTransaction> transactions;

        /// <summary>
        /// Raised after a transaction has been committed.
        /// </summary>
        public event EventHandler TransactionCommitted;

        /// <summary>
        /// Raised after a transaction has been discarded, i.e. rolled back.
        /// </summary>
        public event EventHandler TransactionDiscarded;

        /// <summary>
        /// Raised after an attempt to commit or discard a transaction resulted in an exception.
        /// </summary>
        public event EventHandler<Exception> TransactionFaulted;

        /// <summary>
        /// Raised after the current provider, when so configured, starts a transaction.
        /// </summary>
        public event EventHandler TransactionStarted;

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
        public TransactionEnlistedCommandFactory(TConnection connection, bool commitOnDispose = true)
            : this(connection, null, commitOnDispose, DisposeOfTransaction)
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
        public TransactionEnlistedCommandFactory(TConnection connection, TTransaction transaction, bool commitOnDispose = true)
            : this(connection, c => transaction, commitOnDispose, DoNothing)
        {
        }

        TransactionEnlistedCommandFactory(
            TConnection connection,
            Func<TConnection, TTransaction> transactions,
            bool commitOnDispose,
            Action<TTransaction> disposer
            )
        {
            this.connection = connection;
            this.disposer = disposer;
            this.commitOnDispose = commitOnDispose;
            this.transactions = transactions ?? Begin;
        }

        TTransaction Begin(TConnection connection)
        {
            var transaction = connection.BeginTransaction();

            TransactionStarted?.Invoke(this, EventArgs.Empty);

            return transaction as TTransaction;
        }

        /// <summary>
        /// Create an executable database command.
        /// </summary>
        /// <param name="configure">A configuration strategy to be applied to a newly created command.</param>
        /// <returns>A newly generated, executable database command.</returns>
        public TCommand Create(Action<TCommand> configure)
        {
            if (!initialized)
            {
                transaction = transactions.Invoke(connection);

                initialized = true;
            }

            var command = connection.CreateCommand() as TCommand;

            configure.Invoke(command);

            command.Transaction = transaction;

            return command as TCommand;
        }

        /// <summary>
        /// Dispose of the current command provider and any disposable dependencies it manages.
        /// </summary>
        public void Dispose()
        {
            if (transaction is null)
            {
                return;
            }

            try
            {
                if (commitOnDispose)
                {
                    transaction.Commit();

                    TransactionCommitted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    transaction.Rollback();

                    TransactionDiscarded?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception e)
            {
                TransactionFaulted?.Invoke(this, e);
            }

            disposer.Invoke(transaction);
        }
    }
}