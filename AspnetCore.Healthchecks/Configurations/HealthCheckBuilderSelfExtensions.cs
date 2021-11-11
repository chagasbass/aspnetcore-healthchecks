using AspnetCore.Healthchecks.Healthchecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;

namespace AspnetCore.Healthchecks.Configurations
{
    /// <summary>
    /// Extensão para verificar a saúde da própria aplicação.
    /// </summary>
    public static class HealthCheckBuilderSelfExtensions
    {
        public static IHealthChecksBuilder AddSelfCheck(this IHealthChecksBuilder builder, string name, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
        {
            // Registrando o healthcheck customizado SelfHealthCheck
            builder.AddCheck<SelfHealthCheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

            return builder;
        }
    }
}
