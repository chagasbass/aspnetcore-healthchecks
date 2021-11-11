using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.HealthModels;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AspnetCore.Healthchecks.Extensions
{
    /// <summary>
    /// Extension da classe HealthcheckReport para recuperar as informações dos entries dos healtchecks
    /// da aplicação.
    /// </summary>
    public static class HealthReportExtensions
    {
        const string ApplicationName = "Application HealthChecks";
        public static string AddHealthStatusData(this HealthReport report)
        {
            var healthcheckInformation = new HealthInformation
            {
                Name = ApplicationName,
                Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            };

            var entries = report.Entries.ToList();

            entries.ForEach(entrie =>
            {
                if (entrie.Key.Equals(HealthNames.MemoryHealthcheck))
                {
                    healthcheckInformation.ApplicationMemoryHealth = new HealthDataApplicationMemory
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
            });

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var healthResult = JsonSerializer.Serialize(healthcheckInformation, serializeOptions);

            return healthResult;
        }
    }
}
