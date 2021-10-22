namespace AspnetCore.Healthchecks.Domain.HealthModels
{
    public class HealthData
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public HealthData() { }

    }
}
