﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrainManagement.Data;

#nullable disable

namespace TrainManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TrainManagement.Models.Car", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CarNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("FreightEtsngName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FreightTotalWeightKg")
                        .HasColumnType("integer");

                    b.Property<string>("InvoiceNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastOperationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ParentStopId")
                        .HasColumnType("bigint");

                    b.Property<int>("PositionInTrain")
                        .HasColumnType("integer");

                    b.Property<DateTime>("WhenLastOperation")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParentStopId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("TrainManagement.Models.Stop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ParentTrainId")
                        .HasColumnType("bigint");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentTrainId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("TrainManagement.Models.Train", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FromStationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ToStationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TrainIndex")
                        .HasColumnType("integer");

                    b.Property<string>("TrainIndexCombined")
                        .HasColumnType("text");

                    b.Property<int>("TrainNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("TrainManagement.Models.Car", b =>
                {
                    b.HasOne("TrainManagement.Models.Stop", "ParentStop")
                        .WithMany("Cars")
                        .HasForeignKey("ParentStopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentStop");
                });

            modelBuilder.Entity("TrainManagement.Models.Stop", b =>
                {
                    b.HasOne("TrainManagement.Models.Train", "ParentTrain")
                        .WithMany("PassedStops")
                        .HasForeignKey("ParentTrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentTrain");
                });

            modelBuilder.Entity("TrainManagement.Models.Stop", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("TrainManagement.Models.Train", b =>
                {
                    b.Navigation("PassedStops");
                });
#pragma warning restore 612, 618
        }
    }
}
