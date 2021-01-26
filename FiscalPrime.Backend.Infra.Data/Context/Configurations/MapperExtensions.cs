using FiscalPrime.Backend.Infra.Data.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace FiscalPrime.Backend.Infra.Data.Context.Configurations
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapperDependency(this IServiceCollection services)
        {
            // Configura o uso do AutoMappper
            return services.AddTnfAutoMapper(config =>
            {
                config.AddProfile<FiscalPrimeProfile>();
            });
        }
    }
}
