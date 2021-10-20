using System.Collections.Generic;

namespace AspnetCore.Healthchecks.Domain.Entities
{
    public class HealthInformation
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public List<HealthData> HealthDatas { get; set; }
        public HealthDataMemory MemoryHealth { get; set; }

        public HealthInformation()
        {
            HealthDatas = new List<HealthData>();
        }
    }
}
