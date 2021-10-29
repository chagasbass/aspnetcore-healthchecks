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
    /// <summary>
    /// Extensão para configuração de healthchecks
    /// </summary>
    public static class HealthchecksExtensions
    {

        /// <summary>
        /// Efetua a configuração dos healthchecks customizados e da UI da dashboard que será usada
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            #region healthchecks customizados
            services.AddHealthChecks()
                    .AddGCInfoCheck(HealthNames.MemoryHealthcheck, default, HealthNames.MemoryTags)
                    .AddCheck<SqlServerHealthcheck>(HealthNames.SqlServerHealthcheck, default, HealthNames.DatabaseTags)
                    .AddCheck<AddressExternalServiceHealthcheck>(HealthNames.ExternalServiceHealthcheck, default, HealthNames.ExternalServicesTags)
                    .AddSelfCheck(HealthNames.SelfHealthcheck, default, HealthNames.SelfTags);

            #endregion

            #region healthcheckUI
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(20);
                setup.MaximumHistoryEntriesPerEndpoint(50);
            }).AddInMemoryStorage();
            #endregion

            return services;
        }

        /// <summary>
        /// Extensão que para customizar o as informações dos healthchecks e retornar um json customizado
        /// em um determinado endpoint
        /// </summary>
        /// <param name="app"></param>
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

        /// <summary>
        /// Configuração do middlelware do healthcheck UI
        /// </summary>
        /// <param name="app"></param>
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
