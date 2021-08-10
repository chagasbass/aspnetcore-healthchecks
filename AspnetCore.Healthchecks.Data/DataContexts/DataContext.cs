using AspnetCore.Healthchecks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspnetCore.Healthchecks.Data.DataContexts
{
    public class DataContext : DbContext
    {
        public DbSet<Address> Address { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
