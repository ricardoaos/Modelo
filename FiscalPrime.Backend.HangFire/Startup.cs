using FiscalPrime.Backend.Application;
using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Domain.Interfaces;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.Infra.Data.Context.Configurations;
using FiscalPrime.Backend.Infra.Data.PostgreSQL;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Tnf.Configuration;

namespace FiscalPrime.Backend.HangFire
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        public IConfiguration Configuration { get; }
        RacConfiguration RacConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
            RacConfiguration = new RacConfiguration(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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

            services.AddPostgreSQLDependency();

            services.AddSingleton(RacConfiguration);

            services
               .AddResponseCompression()
               .AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fiscal Prime - HangFire", Version = "v1" });
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

            var automatizadorConfig = Configuration.GetSection("AutomatizadorConfig")
                     .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            services.AddSingleton(automatizadorConfig);

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSQL")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseTnfAspNetCore();

            app.UseRouting();

            app.UseTnfAspNetCoreSecurity();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo JWT Api");
            });

            app.UseHangfireServer();
            app.UseAuthentication();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
        }
    }
}
