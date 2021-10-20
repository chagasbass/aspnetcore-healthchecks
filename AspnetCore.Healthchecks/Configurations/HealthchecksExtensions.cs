using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.Entities;
using AspnetCore.Healthchecks.Healthchecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AspnetCore.Healthchecks.Configurations
{
    public static class HealthchecksExtensions
    {
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            #region healthchecks customizados
            services.AddHealthChecks()
                    .AddGCInfoCheck(HealthNames.MEMORY_HEALTHCHECK)
                    .AddCheck<SqlServerHealthcheck>(HealthNames.SQLSERVER_HEALTHCHECK)
                    .AddCheck<AddressExternalServiceHealthcheck>(HealthNames.EXTERNALSERVICE_HEALTHCHECK)
                    .AddSelfCheck(HealthNames.SELF_HEALTHCHECK);

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
                         string result = GetHealthStatusData(report);

                         context.Response.ContentType = MediaTypeNames.Application.Json;

                         await context.Response.WriteAsync(result);

                     }
                 });
        }

        private static string GetHealthStatusData(HealthReport report)
        {
            var healthcheckInformation = new HealthInformation
            {
                Name = "Application HealthChecks",
                Version = "V1",
                Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var entries = report.Entries.ToList();

            foreach (var x in entries)
            {
                if (x.Key.Equals(HealthNames.MEMORY_HEALTHCHECK))
                {
                    healthcheckInformation.MemoryHealth = new HealthDataMemory()
                    {
                        Name = x.Key,
                        Description = x.Value.Description,
                        Status = x.Value.Status.ToString(),
                        AllocatedMemory = GCInfoOptions.AllocatedMemory,
                        TotalAvailableMemory = GCInfoOptions.TotalAvailableMemory,
                        MaxMemory = GCInfoOptions.MaxMemory
                    };
                }
                else
                {
                    healthcheckInformation.HealthDatas.Add(new HealthData
                    {
                        Name = x.Key,
                        Description = x.Value.Description,
                        Status = x.Value.Status.ToString()
                    });
                }

            }

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var result = JsonSerializer.Serialize(healthcheckInformation, serializeOptions);

            return result;
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
