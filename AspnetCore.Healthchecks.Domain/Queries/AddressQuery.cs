using System.Text.Json.Serialization;

namespace AspnetCore.Healthchecks.Domain.Queries
{
    /// <summary>
    /// Classe que representa o retorno do serviço que será consumido
    /// </summary>
    public class AddressQuery
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }
        [JsonPropertyName("uf")]
        public string Uf { get; set; }
        [JsonPropertyName("ibge")]
        public string Ibge { get; set; }
        [JsonInclude]
        [JsonPropertyName("gia")]
        public string Gia { get; set; }
        [JsonPropertyName("ddd")]
        public string DDD { get; set; }
        [JsonPropertyName("siafi")]
        public string Siafi { get; set; }

        public AddressQuery()
        {

        }
    }
}
