using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Healthchecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;

namespace AspnetCore.Healthchecks.Configurations
{
    /// <summary>
    /// Extension para HealthCheck do GarbageCollector
    /// </summary>
    public static class GCInfoHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddGCInfoCheck(
            this IHealthChecksBuilder builder,
            string name,
            HealthStatus? failureStatus = null,
            IEnumerable<string> tags = null,
            long? thresholdInBytes = null)
        {
            // Registrando o Healthcheck customizado de GCInfo
            builder.AddCheck<GarbageCollectorHealthcheck>(name, failureStatus ?? HealthStatus.Degraded, tags);

            //Configurando o valor de Threshold usando Options Pattern
            if (thresholdInBytes.HasValue)
            {
                builder.Services.Configure<GCInfoOptions>(name, options =>
                {
                    options.Threshold = thresholdInBytes.Value;
                });
            }

            return builder;
        }
    }
}
