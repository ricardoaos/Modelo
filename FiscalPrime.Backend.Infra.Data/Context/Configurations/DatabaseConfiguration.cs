﻿using Microsoft.Extensions.Configuration;
using System;

namespace FiscalPrime.Backend.Infra.Data.Context.Configurations
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            DefaultSchema = configuration["DefaultSchema"];
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.SqlServer.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.SqlServer;
            }
            else if (DatabaseType.Sqlite.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.Sqlite;
            }
            else if (DatabaseType.Oracle.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.Oracle;

                if (string.IsNullOrWhiteSpace(DefaultSchema))
                    throw new NotSupportedException($"When Oracle is used you must have set DefaultSchema in configuration.");
            }
            else if (DatabaseType.PostgreSQL.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.PostgreSQL;
            }
            else
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");

            ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
        }

        public string DefaultSchema { get; }
        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        SqlServer,
        Sqlite,
        Oracle,
        PostgreSQL
    }
}
