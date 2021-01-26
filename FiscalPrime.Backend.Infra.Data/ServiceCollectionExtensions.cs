using FiscalPrime.Backend.Domain.Interfaces.Repositories;
using FiscalPrime.Backend.Infra.Data.Context.Configurations;
using FiscalPrime.Backend.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FiscalPrime.Backend.Infra.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services)
        {
            services
                .AddTnfEntityFrameworkCore()    // Configura o uso do EntityFrameworkCore registrando os contextos que serão usados pela aplicação
                .AddMapperDependency();         // Configura o uso do AutoMappper

            // Registro dos repositórios
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IDominioExternoRepository, DominioExternoRepository>();


            return services;
        }
    }
}
