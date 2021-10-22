using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.Converters;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Healthchecks
{
    public class GarbageCollectorHealthcheck : IHealthCheck
    {
        private readonly GCInfoOptions _options;

        public GarbageCollectorHealthcheck(IOptionsMonitor<GCInfoOptions> options)
        {
            _options = options.CurrentValue;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var allocatedMemory = GC.GetTotalMemory(forceFullCollection: false);

            var gcInfo = GC.GetGCMemoryInfo();

            GCInfoOptions.MaxMemory = MemoryConverterExtensions.ConvertMemorySize(_options.Threshold);
            GCInfoOptions.AllocatedMemory = MemoryConverterExtensions.ConvertMemorySize(allocatedMemory);
            GCInfoOptions.TotalAvailableMemory = MemoryConverterExtensions.ConvertMemorySize(gcInfo.TotalAvailableMemoryBytes);
            GCInfoOptions.SetOperationalSystem();

            if (allocatedMemory > _options.Threshold)
            {
                return new HealthCheckResult(
                                              HealthStatus.Degraded,
                                              description: HealthNames.MEMORY_DESCRIPTION);
            }

            return new HealthCheckResult(
                                          HealthStatus.Healthy,
                                          description: HealthNames.MEMORY_DESCRIPTION);
        }
    }
}
