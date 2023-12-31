﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce.Data;

namespace e_commerce.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221206195947_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("e_commerce.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductCategory")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Cost = 500.0,
                            Description = "plastic chair",
                            Name = "Plastic chair",
                            ProductCategory = 1
                        },
                        new
                        {
                            ProductId = 2,
                            Cost = 5000.0,
                            Description = "Office chair",
                            Name = "Office chair",
                            ProductCategory = 1
                        },
                        new
                        {
                            ProductId = 3,
                            Cost = 50000.0,
                            Description = "Laptop",
                            Name = "Hp Laptop",
                            ProductCategory = 0
                        },
                        new
                        {
                            ProductId = 4,
                            Cost = 5000.0,
                            Description = "Utensils",
                            Name = "Pressure cooker",
                            ProductCategory = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
