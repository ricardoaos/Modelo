using FiscalPrime.Backend.Infra.Data.Context;
using FiscalPrime.Backend.Infra.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Drivers.DevartPostgreSQL;
using Tnf.Runtime.Session;

namespace FiscalPrime.Backend.Infra.Data.PostgreSQL.Context
{
    public class PostgreSQLCrudDbContextFactory : IDesignTimeDbContextFactory<PostgreSQLCrudDbContext>
    {
        public PostgreSQLCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FiscalPrimeDbContext>();

            var configuration = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile($"appsettings.Development.json", false)
                                     .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            PostgreSqlLicense.Validate(databaseConfiguration.ConnectionString);

            builder.UsePostgreSql(databaseConfiguration.ConnectionString);

            return new PostgreSQLCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
