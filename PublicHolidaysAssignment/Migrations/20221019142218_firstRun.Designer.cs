﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHolidaysAssignment;

#nullable disable

namespace PublicHolidaysAssignment.Migrations
{
    [DbContext(typeof(HolidayDbContext))]
    [Migration("20221019142218_firstRun")]
    partial class firstRun
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PublicHolidaysAssignment.Models.CountryHolidays", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CountryId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("PublicHolidaysAssignment.Models.Date", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryHolidaysCountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DayOfTheWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryHolidaysCountryId");

                    b.ToTable("Date");
                });

            modelBuilder.Entity("PublicHolidaysAssignment.Models.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryHolidaysCountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryHolidaysCountryId");

                    b.ToTable("Name");
                });

            modelBuilder.Entity("PublicHolidaysAssignment.Models.Date", b =>
                {
                    b.HasOne("PublicHolidaysAssignment.Models.CountryHolidays", null)
                        .WithMany("Date")
                        .HasForeignKey("CountryHolidaysCountryId");
                });

            modelBuilder.Entity("PublicHolidaysAssignment.Models.Name", b =>
                {
                    b.HasOne("PublicHolidaysAssignment.Models.CountryHolidays", null)
                        .WithMany("Name")
                        .HasForeignKey("CountryHolidaysCountryId");
                });

            modelBuilder.Entity("PublicHolidaysAssignment.Models.CountryHolidays", b =>
                {
                    b.Navigation("Date");

                    b.Navigation("Name");
                });
#pragma warning restore 612, 618
        }
    }
}