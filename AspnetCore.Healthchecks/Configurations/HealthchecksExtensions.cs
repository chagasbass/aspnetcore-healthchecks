using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Extensions;
using AspnetCore.Healthchecks.Healthchecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

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
                         string result = report.AddHealthStatusData();

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
