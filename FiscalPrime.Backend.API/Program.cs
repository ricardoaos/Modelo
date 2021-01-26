using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace FiscalPrime.Backend.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.Title = typeof(Program).Namespace;

                await CreateHostBuilder(args)
                   .Build()
                   .RunAsync();
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
        .UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);

            configuration.Enrich.With<CustomEnricher>();
        });
    }

    public class CustomEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception != null)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("InnerExceptionMessage", logEvent.Exception.InnerException != null ? logEvent.Exception.InnerException.Message : ""));
            }
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", "Preencher aqui o usuário."));
        }
    }
}
