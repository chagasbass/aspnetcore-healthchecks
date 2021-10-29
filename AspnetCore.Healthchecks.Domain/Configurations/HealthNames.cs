using System.Collections.Generic;

namespace AspnetCore.Healthchecks.Domain.Configurations
{
    /// <summary>
    /// Classe que contém as informações de Resource
    /// </summary>
    public static class HealthNames
    {
        public const string MemoryHealthcheck = "APP Memory info";
        public const string SqlServerHealthcheck = "SqlServer Database";
        public const string ExternalServiceHealthcheck = "Address Service";
        public const string SelfHealthcheck = "Self";
        public const string MemoryDescription = "Consumo de memória permitido";
        public const string MemoryDescriptionError = "Consumo de memória elevado";
        public const string SqlServerDescription = "Banco de dados SqlServer OK";
        public const string SqlServerDescriptionError = "Banco de dados SqlServer com erros";
        public const string ExternalServiceDescription = "Serviço Externo de cep OK";
        public const string ExternalServiceDescriptionError = "Serviço Externo de cep com erros";
        public const string SelfDescription = "Monitoramento próprio";
        public const string SelfDescriptionError = "Monitoramento próprio com erros";

        public static List<string> MemoryTags = new List<string>() { "memory", "process" };
        public static List<string> DatabaseTags = new List<string>() { "database", "sqlServer" };
        public static List<string> ExternalServicesTags = new List<string>() { "services", "http Calls" };
        public static List<string> SelfTags = new List<string>() { "self", "monitoring" };
    }
}
