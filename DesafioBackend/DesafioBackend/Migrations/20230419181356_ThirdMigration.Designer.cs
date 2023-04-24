﻿// <auto-generated />
using System;
using DesafioBackend.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioBackend.Migrations
{
    [DbContext(typeof(DesafioBackendContext))]
    [Migration("20230419181356_ThirdMigration")]
    partial class ThirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("DesafioBackend.DataCollection.DataCollectionPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IndicatorId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("IndicatorId");

                    b.ToTable("DataCollectionPoints", (string)null);
                });

            modelBuilder.Entity("DesafioBackend.Indicators.Indicator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("ResultType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Indicators", (string)null);
                });

            modelBuilder.Entity("DesafioBackend.DataCollection.DataCollectionPoint", b =>
                {
                    b.HasOne("DesafioBackend.Indicators.Indicator", "Indicator")
                        .WithMany("DataCollectionPoints")
                        .HasForeignKey("IndicatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Indicator");
                });

            modelBuilder.Entity("DesafioBackend.Indicators.Indicator", b =>
                {
                    b.Navigation("DataCollectionPoints");
                });
#pragma warning restore 612, 618
        }
    }
}