using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class HMLEntityTypeConfiguration : IEntityTypeConfiguration<HML>
    {
        public void Configure(EntityTypeBuilder<HML> builder)
        {
            builder.HasMany(h => h.Types).WithOne(t => t.HML).HasForeignKey(t => t.HMLId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.Attributes).WithOne(a => a.HML).HasForeignKey(a => a.HMLId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.TPH).WithOne(t => t.HML).HasForeignKey(t => t.HMLId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.ARD).WithOne(a => a.HML).HasForeignKey(a => a.HMLId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.Properties).WithOne(p => p.HML).HasForeignKey(p => p.HMLId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}