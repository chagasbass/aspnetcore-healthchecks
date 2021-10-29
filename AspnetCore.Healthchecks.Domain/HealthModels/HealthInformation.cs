using System.Collections.Generic;

namespace AspnetCore.Healthchecks.Domain.HealthModels
{
    public class HealthInformation
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public List<HealthData> HealthDatas { get; set; }
        public HealthDataApplicationMemory ApplicationMemoryHealth { get; set; }

        public HealthInformation()
        {
            HealthDatas = new List<HealthData>();
        }
    }
}
