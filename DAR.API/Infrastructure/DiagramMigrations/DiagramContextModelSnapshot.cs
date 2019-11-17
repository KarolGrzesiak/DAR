﻿// <auto-generated />
using DAR.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAR.Infrastructure.DiagramMigrations
{
    [DbContext(typeof(DiagramContext))]
    partial class DiagramContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("DAR.API.Model.Attribute", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("HMLId");

                    b.Property<string>("Abbreviation");

                    b.Property<string>("CLB");

                    b.Property<string>("Class");

                    b.Property<string>("Comm");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id", "HMLId");

                    b.HasIndex("HMLId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("DAR.API.Model.Dependency", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("HMLId");

                    b.Property<string>("Destination");

                    b.Property<string>("Source");

                    b.HasKey("Id", "HMLId");

                    b.HasIndex("HMLId");

                    b.HasIndex("Destination", "HMLId");

                    b.HasIndex("Source", "HMLId");

                    b.ToTable("ARDs");
                });

            modelBuilder.Entity("DAR.API.Model.Domain", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ordered");

                    b.HasKey("Id");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("DAR.API.Model.HML", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("HMLs");
                });

            modelBuilder.Entity("DAR.API.Model.Property", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("HMLId");

                    b.HasKey("Id", "HMLId");

                    b.HasIndex("HMLId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("DAR.API.Model.Reference", b =>
                {
                    b.Property<string>("Source");

                    b.Property<string>("Destination");

                    b.Property<string>("HMLId");

                    b.HasKey("Source", "Destination", "HMLId");

                    b.HasIndex("Destination", "HMLId");

                    b.HasIndex("Source", "HMLId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("DAR.API.Model.Transformation", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("HMLId");

                    b.Property<string>("Destination");

                    b.Property<string>("Source");

                    b.HasKey("Id", "HMLId");

                    b.HasIndex("HMLId");

                    b.HasIndex("Destination", "HMLId");

                    b.HasIndex("Source", "HMLId");

                    b.ToTable("TPHs");
                });

            modelBuilder.Entity("DAR.API.Model.Type", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("HMLId");

                    b.Property<string>("Base");

                    b.Property<string>("Description");

                    b.Property<string>("DomainId");

                    b.Property<string>("Length");

                    b.Property<string>("Name");

                    b.Property<string>("Scale");

                    b.HasKey("Id", "HMLId");

                    b.HasIndex("DomainId");

                    b.HasIndex("HMLId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("DAR.API.Model.Value", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DomainId");

                    b.Property<string>("From");

                    b.Property<string>("Is");

                    b.Property<string>("Num");

                    b.Property<string>("To");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("DAR.API.Model.Attribute", b =>
                {
                    b.HasOne("DAR.API.Model.HML", "HML")
                        .WithMany("Attributes")
                        .HasForeignKey("HMLId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAR.API.Model.Dependency", b =>
                {
                    b.HasOne("DAR.API.Model.HML", "HML")
                        .WithMany("ARD")
                        .HasForeignKey("HMLId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAR.API.Model.Property", "DestinationProperty")
                        .WithMany()
                        .HasForeignKey("Destination", "HMLId");

                    b.HasOne("DAR.API.Model.Property", "SourceProperty")
                        .WithMany()
                        .HasForeignKey("Source", "HMLId");
                });

            modelBuilder.Entity("DAR.API.Model.Property", b =>
                {
                    b.HasOne("DAR.API.Model.HML", "HML")
                        .WithMany("Properties")
                        .HasForeignKey("HMLId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAR.API.Model.Reference", b =>
                {
                    b.HasOne("DAR.API.Model.Attribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("Destination", "HMLId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAR.API.Model.Property", "Property")
                        .WithMany("References")
                        .HasForeignKey("Source", "HMLId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAR.API.Model.Transformation", b =>
                {
                    b.HasOne("DAR.API.Model.HML", "HML")
                        .WithMany("TPH")
                        .HasForeignKey("HMLId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAR.API.Model.Property", "DestinationProperty")
                        .WithMany()
                        .HasForeignKey("Destination", "HMLId");

                    b.HasOne("DAR.API.Model.Property", "SourceProperty")
                        .WithMany()
                        .HasForeignKey("Source", "HMLId");
                });

            modelBuilder.Entity("DAR.API.Model.Type", b =>
                {
                    b.HasOne("DAR.API.Model.Domain", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId");

                    b.HasOne("DAR.API.Model.HML", "HML")
                        .WithMany("Types")
                        .HasForeignKey("HMLId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAR.API.Model.Value", b =>
                {
                    b.HasOne("DAR.API.Model.Domain")
                        .WithMany("Values")
                        .HasForeignKey("DomainId");
                });
#pragma warning restore 612, 618
        }
    }
}
