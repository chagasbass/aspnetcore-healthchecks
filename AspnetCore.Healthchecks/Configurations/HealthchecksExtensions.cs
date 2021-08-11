using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.Entities;
using AspnetCore.Healthchecks.Healthchecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mime;
using System.Text.Json;

namespace AspnetCore.Healthchecks.Configurations
{
    public static class HealthchecksExtensions
    {
        const string MEMORY_HEALTHCHECK = "Memory info";
        const string SQLSERVER_HEALTHCHECK = "SqlServer Database";
        const string EXTERNALSERVICE_HEALTHCHECK = "Address Service";

        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            #region healthchecks customizados
            services.AddHealthChecks()
                    .AddGCInfoCheck(MEMORY_HEALTHCHECK)
                    .AddCheck<SqlServerHealthcheck>(SQLSERVER_HEALTHCHECK)
                    .AddCheck<AddressExternalServiceHealthcheck>(EXTERNALSERVICE_HEALTHCHECK);
            #endregion

            #region healthcheckUI
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(60);
                setup.MaximumHistoryEntriesPerEndpoint(50);
            }).AddInMemoryStorage();
            #endregion

            return services;
        }

        public static void UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/app-status");
            app.UseHealthChecks("/app-status-json",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                Sucesso = true,
                                Mensagem = "Status da aplicação",
                                Data = new HealthcheckInformation()
                                {
                                    Name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                                    Version = "V1",
                                    Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Status = report.Status.ToString(),
                                    AllocatedMemory = GCInfoOptions.AllocatedMemory,
                                    TotalAvailableMemory = GCInfoOptions.TotalAvailableMemory,
                                    MaxMemory = GCInfoOptions.MaxMemory
                                }
                            });

                        GCInfoOptions.DisposeGcInfoOptions();

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
        }

        public static void UserHealthCheckUi(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
                //options.AddCustomStylesheet("dotnet.css"); add customização na dashboard
            });
        }
    }
}
