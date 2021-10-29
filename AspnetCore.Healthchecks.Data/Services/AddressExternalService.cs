using AspnetCore.Healthchecks.Domain.Configurations;
using AspnetCore.Healthchecks.Domain.Queries;
using AspnetCore.Healthchecks.Domain.Services;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Data.Services
{
    /// <summary>
    /// Implementação do Serviço externo de Address
    /// </summary>
    public class AddressExternalService : IAddressExternalService
    {
        private readonly BaseConfigurationOptions _baseConfigurationOptions;
        private readonly IHttpClientFactory _httpClient;

        public AddressExternalService(IHttpClientFactory httpClient,
                                      IOptionsMonitor<BaseConfigurationOptions> options)
        {
            _httpClient = httpClient;
            _baseConfigurationOptions = options.CurrentValue;
        }

        private HttpRequestMessage CreateRequest(string cep)
        {

            //viacep.com.br/ws/24130110/json/
            var cepWithoutMask = cep.Replace("-", string.Empty);
            var url = _baseConfigurationOptions.AddressService.Replace("my_cep", cepWithoutMask);

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        public async Task<AddressQuery> GetAddressByCepAsync(string cep)
        {
            var externalClient = _httpClient.CreateClient();

            var addressRequest = CreateRequest(cep);

            var addressQuery = new AddressQuery();

            var addressResponse = await externalClient.SendAsync(addressRequest);

            if (addressResponse.IsSuccessStatusCode)
            {
                var response = await addressResponse.Content.ReadAsStringAsync();
                addressQuery = JsonSerializer.Deserialize<AddressQuery>(response);
            }

            return addressQuery;
        }
    }
}

