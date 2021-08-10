using AspnetCore.Healthchecks.Domain.Queries;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Domain.Services
{
    public interface IAddressExternalService
    {
        Task<AddressQuery> GetAddressByCepAsync(string cep);
    }
}
