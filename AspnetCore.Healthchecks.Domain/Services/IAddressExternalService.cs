using AspnetCore.Healthchecks.Domain.Queries;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Domain.Services
{
    /// <summary>
    /// Interface do serviço externo de address
    /// </summary>
    public interface IAddressExternalService
    {
        Task<AddressQuery> GetAddressByCepAsync(string cep);
    }
}
