using BasicCrud.Infra.SqlServer.Context;
using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Infra.Data.Context;
using FiscalPrime.Backend.Infra.Data.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FiscalPrime.Backend.Infra.Data.PostgreSQL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgreSQLDependency(this IServiceCollection services)
        {
            services
                .AddInfraDependency()
                //.AddTnfDbContext<FiscalPrimeDbContext, PostgreSQLCrudDbContext>((config) =>
                .AddTnfDbContext<FiscalPrimeDbContext, SQLServerCrudDbContext>((config) =>
                {
                    if (Constants.IsDevelopment())
                    {
                        config.DbContextOptions.EnableSensitiveDataLogging();
                        config.UseLoggerFactory();
                    }

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });

            return services;
        }
    }
}
