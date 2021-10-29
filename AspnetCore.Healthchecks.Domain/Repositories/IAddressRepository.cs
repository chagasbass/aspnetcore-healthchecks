using AspnetCore.Healthchecks.Domain.Entities;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Domain.Repositories
{
    /// <summary>
    /// Interface de repositório para Address
    /// </summary>
    public interface IAddressRepository
    {
        Task SaveAddressAsync(Address address);
    }
}
