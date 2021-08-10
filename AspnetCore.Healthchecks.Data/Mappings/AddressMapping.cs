using AspnetCore.Healthchecks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspnetCore.Healthchecks.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("ADDRESS");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CEP)
                  .HasMaxLength(8)
                  .IsRequired();

            builder.Property(e => e.Street)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.District)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.City)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.State)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
