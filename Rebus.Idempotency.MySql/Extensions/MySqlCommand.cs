using System.Data;
using MySqlConnector;

namespace Rebus.Idempotency.MySql.Extensions
{
    internal static class MySqlCommand
    {
        public static MySqlParameter CreateParameter(this MySqlConnector.MySqlCommand command, string name, DbType dbType, object value)
        {
            var parameter = command.CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}
