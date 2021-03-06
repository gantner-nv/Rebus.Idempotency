﻿using System;
using System.Collections.Generic;

namespace Rebus.Idempotency.MySql.Extensions
{
    internal static class MySqlConnection
    {
        public static List<string> GetTableNames(this global::Rebus.Idempotency.MySql.MySqlConnection connection)
        {
            var tableNames = new List<string>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from information_schema.tables where table_schema not in ('pg_catalog', 'information_schema', 'ServiceBus')";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader["table_name"].ToString());
                    }
                }
            }

            return tableNames;
        }
    }
}
