using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class HMLEntityTypeConfiguration : IEntityTypeConfiguration<HML>
    {
        public void Configure(EntityTypeBuilder<HML> builder)
        {
            builder.HasMany(h => h.Types).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.Attributes).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.TPH).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.ARD).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.Properties).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}