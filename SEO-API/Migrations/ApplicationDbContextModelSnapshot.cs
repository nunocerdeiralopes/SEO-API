﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEO_API.Data;

namespace SEO_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SEO_API.Models.RecurringKeyword", b =>
                {
                    b.Property<int>("RecurringKeyworId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryDomain")
                        .IsRequired();

                    b.Property<string>("Query")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("RecurringKeyworId");

                    b.ToTable("RecurringKeyword");
                });

            modelBuilder.Entity("SEO_API.Models.RecurringKeywordPosition", b =>
                {
                    b.Property<int>("RecurringKeywordPositionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Positions")
                        .IsRequired();

                    b.Property<int>("RecurringKeyworId");

                    b.HasKey("RecurringKeywordPositionId");

                    b.ToTable("RecurringKeywordPosition");
                });
#pragma warning restore 612, 618
        }
    }
}
