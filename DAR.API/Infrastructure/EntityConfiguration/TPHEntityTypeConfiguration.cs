using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class TPHEntityTypeConfiguration : IEntityTypeConfiguration<Transformation>
    {
        public void Configure(EntityTypeBuilder<Transformation> builder)
        {

            builder.HasKey(t => new { t.Id, t.HMLId });
            builder.HasOne(t => t.SourceProperty).WithMany().HasForeignKey(t => new { t.Source, t.HMLId });
            builder.HasOne(t => t.DestinationProperty).WithMany().HasForeignKey(t => new { t.Destination, t.HMLId });
        }
    }
}