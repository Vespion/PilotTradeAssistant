﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using PTA.GalaxyDatabase.DataLayer;

#nullable disable

namespace PTA.GalaxyDatabase.DataLayer.Migrations
{
    [DbContext(typeof(GalaxyDbContext))]
    partial class GalaxyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("PTA.GalaxyDatabase.DataLayer.Models.Spatial.StarSystem", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<Point>("Position")
                        .IsRequired()
                        .HasColumnType("POINT")
                        .HasAnnotation("Sqlite:Srid", 4979);

                    b.HasKey("Id");

                    b.HasIndex("Position")
                        .IsUnique();

                    b.ToTable("StarSystems");
                });

            modelBuilder.Entity("PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars.StellarClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanBeScooped")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasComputedColumnSql("Class BETWEEN 0 AND 13");

                    b.Property<byte>("Class")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Luminosity")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("SubClass")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Class", "SubClass", "Luminosity");

                    b.ToTable("StellarClasses");
                });

            modelBuilder.Entity("PTA.GalaxyDatabase.DataLayer.Models.Spatial.StarSystem", b =>
                {
                    b.OwnsMany("PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars.Star", "Stars", b1 =>
                        {
                            b1.Property<long>("SystemId")
                                .HasColumnType("INTEGER");

                            b1.Property<long>("Id")
                                .HasColumnType("INTEGER");

                            b1.Property<int?>("ClassId")
                                .HasColumnType("INTEGER");

                            b1.Property<float>("DistanceFromMainStar")
                                .HasColumnType("REAL");

                            b1.HasKey("SystemId", "Id");

                            b1.HasIndex("ClassId");

                            b1.ToTable("Stars", (string)null);

                            b1.HasOne("PTA.GalaxyDatabase.DataLayer.Models.Spatial.Stars.StellarClass", "Class")
                                .WithMany()
                                .HasForeignKey("ClassId")
                                .OnDelete(DeleteBehavior.SetNull);

                            b1.WithOwner("System")
                                .HasForeignKey("SystemId");

                            b1.Navigation("Class");

                            b1.Navigation("System");
                        });

                    b.Navigation("Stars");
                });
#pragma warning restore 612, 618
        }
    }
}