using DAR.API.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DAR.API.Infrastructure.EntityConfiguration;

namespace DAR.API.Infrastructure
{
    public class DiagramContext : DbContext
    {
        public DbSet<HML> HMLs { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Transformation> TPHs { get; set; }
        public DbSet<Dependency> ARDs { get; set; }
        public DbSet<Reference> References { get; set; }

        public DiagramContext(DbContextOptions<DiagramContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HMLEntityTypeConfiguration());
            builder.ApplyConfiguration(new PropertyEntityTypeConfiguration());
            builder.ApplyConfiguration(new ARDEntityTypeConfiguration());
            builder.ApplyConfiguration(new TPHEntityTypeConfiguration());
            builder.ApplyConfiguration(new TypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new AttributeEntityTypeConfiguration());
            builder.ApplyConfiguration(new ReferenceEntityTypeConfiguration());
        }



    }

}