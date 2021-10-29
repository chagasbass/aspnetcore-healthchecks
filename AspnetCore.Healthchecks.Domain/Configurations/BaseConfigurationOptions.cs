namespace AspnetCore.Healthchecks.Domain.Configurations
{
    /// <summary>
    /// Configurações base da aplicação
    /// </summary>
    public class BaseConfigurationOptions
    {
        public const string BaseConfig = "BaseConfig";
        public string AddressService { get; set; }
        public string DatabaseConn { get; set; }
    }
}
