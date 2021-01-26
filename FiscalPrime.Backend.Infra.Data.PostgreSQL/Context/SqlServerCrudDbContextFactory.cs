using FiscalPrime.Backend.Infra.Data.Context;
using FiscalPrime.Backend.Infra.Data.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace BasicCrud.Infra.SqlServer.Context
{
    public class SqlServerCrudDbContextFactory : IDesignTimeDbContextFactory<SQLServerCrudDbContext>
    {
        public SQLServerCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FiscalPrimeDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            builder.UseSqlServer(databaseConfiguration.ConnectionString);

            return new SQLServerCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
