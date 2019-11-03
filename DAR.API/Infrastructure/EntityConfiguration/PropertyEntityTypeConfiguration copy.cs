using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasMany(p => p.References).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
