using AspnetCore.Healthchecks.Domain.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Healthchecks
{
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
               description: HealthNames.EXTERNALSERVICE_DESCRIPTION);
            }
            catch
            {
                return new HealthCheckResult(
                HealthStatus.Unhealthy,
                description: HealthNames.EXTERNALSERVICE_DESCRIPTION);
            }

            return new HealthCheckResult(
                HealthStatus.Healthy,
                description: HealthNames.EXTERNALSERVICE_DESCRIPTION);
        }
    }
}
