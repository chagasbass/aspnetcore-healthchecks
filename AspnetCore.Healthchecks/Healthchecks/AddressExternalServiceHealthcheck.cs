using AspnetCore.Healthchecks.Domain.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Healthchecks
{
    /// <summary>
    /// Healthcheck customizado para o serviço de endereço
    /// </summary>
    public class AddressExternalServiceHealthcheck : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly BaseConfigurationOptions _baseConfigurationOptions;

        private const string CEP = "24040110";

        public AddressExternalServiceHealthcheck(IHttpClientFactory clientFactory,
                                             IOptionsMonitor<BaseConfigurationOptions> options)
        {
            _clientFactory = clientFactory;
            _baseConfigurationOptions = options.CurrentValue;
        }

        /// <summary>
        /// Checa a saúde dos serviços externos
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var url = _baseConfigurationOptions.AddressService.Replace("my_cep", CEP);
            using var client = _clientFactory.CreateClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return new HealthCheckResult(
               HealthStatus.Unhealthy,
               description: HealthNames.ExternalServiceDescription);
            }
            catch
            {
                return new HealthCheckResult(
                HealthStatus.Unhealthy,
                description: HealthNames.ExternalServiceDescription);
            }

            return new HealthCheckResult(
                HealthStatus.Healthy,
                description: HealthNames.ExternalServiceDescription);
        }
    }
}
