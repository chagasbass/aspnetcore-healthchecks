namespace AspnetCore.Healthchecks.Domain.Configurations
{
    public static class HealthNames
    {
        public const string MEMORY_HEALTHCHECK = "APP Memory info";
        public const string SQLSERVER_HEALTHCHECK = "SqlServer Database";
        public const string EXTERNALSERVICE_HEALTHCHECK = "Address Service";
        public const string SELF_HEALTHCHECK = "Self";
        public const string MEMORY_DESCRIPTION = "Consumo de memória";
        public const string SQLSERVER_DESCRIPTION = "Banco de dados SqlServer";
        public const string EXTERNALSERVICE_DESCRIPTION = "Serviço Externo de cep";
        public const string SELF_DESCRIPTION = "Monitoramento próprio";
    }
}
