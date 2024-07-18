using System;
using MySqlConnector;


namespace Rebus.Idempotency.MySql
{
    /// <summary>
    /// Wraps an opened <see cref="MySqlConnector.MySqlConnection"/> and makes it easier to work with it
    /// </summary>
    public class MySqlConnection : IDisposable
    {
        readonly MySqlConnector.MySqlConnection _currentConnection;
        MySqlTransaction _currentTransaction;

        bool _disposed;

        /// <summary>
        /// Constructs the wrapper with the given connection and transaction
        /// </summary>
        public MySqlConnection(MySqlConnector.MySqlConnection currentConnection, MySqlTransaction currentTransaction)
        {
            _currentConnection = currentConnection ?? throw new ArgumentNullException(nameof(currentConnection));
            _currentTransaction = currentTransaction ?? throw new ArgumentNullException(nameof(currentTransaction));
        }

        /// <summary>
        /// Creates a new command, enlisting it in the current transaction
        /// </summary>
        public MySqlCommand CreateCommand()
        {
            var command = _currentConnection.CreateCommand();
            command.Transaction = _currentTransaction;
            return command;
        }

        /// <summary>
        /// Completes the transaction
        /// </summary>

        public void Complete()
        {
            if (_currentTransaction == null) return;
            using (_currentTransaction)
            {
                _currentTransaction.Commit();
                _currentTransaction = null;
            }
        }

        /// <summary>
        /// Rolls back the transaction if it hasn't been completed
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            try
            {
                try
                {
                    if (_currentTransaction == null) return;
                    using (_currentTransaction)
                    {
                        try
                        {
                            _currentTransaction.Rollback();
                        }
                        catch { }
                        _currentTransaction = null;
                    }
                }
                finally
                {
                    _currentConnection.Dispose();
                }
            }
            finally
            {
                _disposed = true;
            }
        }
    }
}
