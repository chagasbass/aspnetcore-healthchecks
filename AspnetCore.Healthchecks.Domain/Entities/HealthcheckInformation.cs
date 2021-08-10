namespace AspnetCore.Healthchecks.Domain.Entities
{
    public class HealthcheckInformation
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }

        public HealthcheckInformation() { }

    }
}
