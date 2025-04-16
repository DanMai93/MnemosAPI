using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MnemosAPI.Models;

namespace MnemosAPI.Data.Configurations
{
    public partial class EndCustomerConfiguration : IEntityTypeConfiguration<EndCustomer> // Changed class to partial
    {
        public void Configure(EntityTypeBuilder<EndCustomer> entity)
        {
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EndCustomer> entity);
    }
    
}
