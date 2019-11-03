using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class DomainEntityTypeConfiguration : IEntityTypeConfiguration<Domain>
    {
        public void Configure(EntityTypeBuilder<Domain> builder)
        {
            builder.HasMany(d => d.Values).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
