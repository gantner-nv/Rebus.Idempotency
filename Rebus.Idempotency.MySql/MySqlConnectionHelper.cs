using System.Data;
using System.Threading.Tasks;

namespace Rebus.Idempotency.MySql
{
    public class MySqlConnectionHelper
    {
        readonly string _connectionString;

        public MySqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets a fresh, open and ready-to-use connection wrapper
        /// </summary>
        public async Task<MySqlConnection> GetConnection()
        {
            var connection = new MySqlConnector.MySqlConnection(_connectionString);

            await connection.OpenAsync();

            var currentTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            return new MySqlConnection(connection, currentTransaction);
        }
    }
}
