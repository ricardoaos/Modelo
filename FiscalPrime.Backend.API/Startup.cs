using FiscalPrime.Backend.API.HostedServices;
using FiscalPrime.Backend.API.Middleware;
using FiscalPrime.Backend.Application;
using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Domain.Interfaces;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.Infra.Data.Context.Configurations;
using FiscalPrime.Backend.Infra.Data.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tnf.Configuration;

namespace FiscalPrime.Backend.API
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        RacConfiguration RacConfiguration { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
            RacConfiguration = new RacConfiguration(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()
                .AddTnfAspNetCore(options =>
                {
                    // Adiciona as configurações de localização da aplicação
                    options.UseDomainLocalization();

                    // Configuração global de como irá funcionar o Get utilizando o repositorio do Tnf
                    // O exemplo abaixo registra esse comportamento através de uma convenção:
                    // toda classe que implementar essas interfaces irão ter essa configuração definida
                    // quando for consultado um método que receba a interface IRequestDto do Tnf
                    options.Repository(repositoryConfig =>
                    {
                        repositoryConfig.Entity<IEntity>(entity =>
                            entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                    });

                    // Configura a connection string da aplicação
                    options.DefaultConnectionString(DatabaseConfiguration.ConnectionString);

                    //options.EnableDevartPostgreSQLDriver();

                    // Habita o suporte ao multitenancy
                    options.MultiTenancy(tenancy => tenancy.IsEnabled = true);
                })
                .AddTnfAspNetCoreSecurity(Configuration);

                services.AddSingleton(RacConfiguration);
                services.AddPostgreSQLDependency();
                services.AddHostedService<MigrationHostedService>();

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fiscal Prime API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                        Type = SecuritySchemeType.ApiKey
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization" }
                            },
                            new string[0]
                        },
                    });
                });

            services.AddSingleton(RacConfiguration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<SerialogResquestLogger>();

            app.UseFastReport();

            app.UseCors("AllowAll");

            app.UseTnfAspNetCore();

            app.UseRouting();

            app.UseTnfAspNetCoreSecurity();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Fiscal Prime v1");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}