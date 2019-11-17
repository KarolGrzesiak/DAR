using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class ReferenceEntityTypeConfiguration : IEntityTypeConfiguration<Reference>
    {
        public void Configure(EntityTypeBuilder<Reference> builder)
        {
            builder.HasKey(r => new { r.Source, r.Destination, r.HMLId });
            builder.HasOne(r => r.Property).WithMany(p => p.References).HasForeignKey(r => new { r.Source, r.HMLId });
            builder.HasOne(r => r.Attribute).WithMany().HasForeignKey(r => new { r.Destination, r.HMLId });
        }
    }
}