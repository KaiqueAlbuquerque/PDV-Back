﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Business.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MeasuredUnit")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NoteDescription")
                        .HasColumnType("varchar(17)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Business.Model.ProductSale", b =>
                {
                    b.Property<Guid>("SaleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(65,2)");

                    b.HasKey("SaleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSales");
                });

            modelBuilder.Entity("Business.Model.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(65,2)");

                    b.Property<decimal>("Change")
                        .HasColumnType("decimal(65,2)");

                    b.Property<DateTime>("DateRegister")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FormOfPayment")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,2)");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Business.Model.ProductSale", b =>
                {
                    b.HasOne("Business.Model.Product", "Product")
                        .WithMany("ProductSales")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.HasOne("Business.Model.Sale", "Sale")
                        .WithMany("ProductSales")
                        .HasForeignKey("SaleId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}