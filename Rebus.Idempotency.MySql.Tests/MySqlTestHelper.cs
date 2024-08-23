using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace Rebus.Idempotency.MySql.Tests
{
    public static class MySqlTestHelper
    {
        const string TableDoesNotExist = "42S02";
        static readonly MySqlConnectionHelper MySqlConnectionHelper = new MySqlConnectionHelper(ConnectionString);
        public static string DatabaseName => $"rebus2_test";
        public static string ConnectionString => GetConnectionStringForDatabase(DatabaseName);
        public static MySqlConnectionHelper ConnectionHelper => MySqlConnectionHelper;

        public static async Task DropTableIfExists(string tableName)
        {
            using (var connection = await MySqlConnectionHelper.GetConnection())
            {
                using (var comand = connection.CreateCommand())
                {
                    comand.CommandText = $@"drop table if exists `{tableName}`;";

                    try
                    {
                        comand.ExecuteNonQuery();

                        Console.WriteLine("Dropped mysql table '{0}'", tableName);
                    }
                    catch (MySqlException exception) when (exception.SqlState == TableDoesNotExist)
                    {
                    }
                }

                connection.Complete();
            }
        }

        static string GetConnectionStringForDatabase(string databaseName)
        {
            var server = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost";
            return Environment.GetEnvironmentVariable("REBUS_MYSQL")
                ?? $"server={server}; database={databaseName}; user id=mysql; password=mysql;maximum pool size=30;";
        }
    }
}
