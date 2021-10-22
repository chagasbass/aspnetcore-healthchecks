namespace AspnetCore.Healthchecks.Domain.HealthModels
{
    public class HealthDataApplicationMemory : HealthData
    {
        public string AllocatedMemory { get; set; }
        public string TotalAvailableMemory { get; set; }
        public string MaxMemory { get; set; }
        public string OperationalSystem { get; set; }
        public string OperationalSystemArchitecture { get; set; }
        public string ApplicationFramework { get; set; }
    }
}
