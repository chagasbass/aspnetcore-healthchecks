using AspnetCore.Healthchecks.Domain.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Healthchecks
{
    /// <summary>
    /// Healthcheck customizado para monitoramento da própria aplicação
    /// </summary>
    public class SelfHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HealthCheckResult(
                HealthStatus.Healthy,
                description: HealthNames.SelfDescription));
        }
    }
}
