using AspnetCore.Healthchecks.Data.DataContexts;
using AspnetCore.Healthchecks.Domain.Entities;
using AspnetCore.Healthchecks.Domain.Repositories;
using System.Threading.Tasks;

namespace AspnetCore.Healthchecks.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveAddressAsync(Address address)
        {
            await _context.Address.AddAsync(address);
            await _context.SaveChangesAsync();
        }
    }
}
