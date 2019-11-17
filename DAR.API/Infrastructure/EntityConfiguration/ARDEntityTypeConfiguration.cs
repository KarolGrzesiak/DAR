using DAR.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAR.API.Infrastructure.EntityConfiguration
{
    public class ARDEntityTypeConfiguration : IEntityTypeConfiguration<Dependency>
    {
        public void Configure(EntityTypeBuilder<Dependency> builder)
        {
            builder.HasKey(d => new { d.Id, d.HMLId });

            builder.HasOne(d => d.SourceProperty).WithMany().HasForeignKey(d => new { d.Source, d.HMLId });
            builder.HasOne(d => d.DestinationProperty).WithMany().HasForeignKey(d => new { d.Destination, d.HMLId });
        }
    }
}