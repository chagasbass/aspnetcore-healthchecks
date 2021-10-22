using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.HealthModels;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AspnetCore.Healthchecks.Extensions
{
    public static class HealthReportExtensions
    {
        public static string AddHealthStatusData(this HealthReport report)
        {
            var healthcheckInformation = new HealthInformation
            {
                Name = "Application HealthChecks",
                Version = "V1",
                Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var entries = report.Entries.ToList();

            foreach (var entrie in entries)
            {
                if (entrie.Key.Equals(HealthNames.MEMORY_HEALTHCHECK))
                {
                    healthcheckInformation.ApplicationMemoryHealth = new HealthDataApplicationMemory()
                    {
                        Name = entrie.Key,
                        Description = entrie.Value.Description,
                        Status = entrie.Value.Status.ToString(),
                        AllocatedMemory = GCInfoOptions.AllocatedMemory,
                        TotalAvailableMemory = GCInfoOptions.TotalAvailableMemory,
                        MaxMemory = GCInfoOptions.MaxMemory,
                        OperationalSystem = GCInfoOptions.OperationalSystem,
                        OperationalSystemArchitecture = GCInfoOptions.OperationalSystemArchitecture,
                        ApplicationFramework = GCInfoOptions.ApplicationFramework
                    };
                }
                else
                {
                    healthcheckInformation.HealthDatas.Add(new HealthData
                    {
                        Name = entrie.Key,
                        Description = entrie.Value.Description,
                        Status = entrie.Value.Status.ToString()
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
    }
}
