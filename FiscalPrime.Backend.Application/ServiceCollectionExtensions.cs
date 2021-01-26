using FiscalPrime.Backend.Application.Interfaces;
using FiscalPrime.Backend.Application.Services;
using FiscalPrime.Backend.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace FiscalPrime.Backend.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
        {
            // Dependencia do projeto BasicCrud.Domain
            services.AddDomainDependency();

            // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
            // descomente a linha abaixo:
            // services.AddTnfDefaultConventionalRegistrations();

            // Registro dos serviços
            services.AddTransient<IEmpresaAppService, EmpresaAppService>();
            services.AddTransient<IDominioExternoAppService, DominioExternoAppService>();

            return services;
        }
    }
}
