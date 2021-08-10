using AspnetCore.Healthchecks.Domain.Entities;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task SaveAddressAsync(Address address);
    }
}
