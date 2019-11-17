using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class PropertyAttributesEntityTypeConfiguration : IEntityTypeConfiguration<PropertyAttributes>
    {
        public void Configure(EntityTypeBuilder<PropertyAttributes> builder)
        {
            builder.HasKey(pa => new
            {
                pa.PropertyId,
                pa.AttributeId,
                pa.HMLId
            });

            // builder.HasOne(pa => pa.Property).WithMany(p => p.PropertyAttributes).HasForeignKey(pa => new { pa.PropertyId, pa.HMLId });
            // builder.HasOne(pa => pa.Attribute).WithMany(a => a.PropertyAttributes).HasForeignKey(pa => new { pa.AttributeId, pa.HMLId });
        }
    }
}