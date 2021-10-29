namespace AspnetCore.Healthchecks.Domain.HealthModels
{
    /// <summary>
    /// Informações básicas de status de saúde
    /// </summary>
    public class HealthData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public HealthData() { }

    }
}
