namespace AspnetCore.Healthchecks.Domain.Entities
{
    public class HealthDataMemory : HealthData
    {
        public string AllocatedMemory { get; set; }
        public string TotalAvailableMemory { get; set; }
        public string MaxMemory { get; set; }
    }
}
