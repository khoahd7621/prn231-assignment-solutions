﻿// <auto-generated />
using System;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Rose",
                            Description = "This is Rose"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryName = "Peonies",
                            Description = "This is Peonies"
                        },
                        new
                        {
                            CategoryID = 3,
                            CategoryName = "Lily",
                            Description = "This is Lily"
                        },
                        new
                        {
                            CategoryID = 4,
                            CategoryName = "Carnation",
                            Description = "This is Carnation"
                        });
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerID = 1,
                            City = "Ho Chi Minh",
                            Country = "VietNam",
                            CustomerName = "Hoang Dang Khoa",
                            DateOfBirth = new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7513),
                            Email = "khoa.hoangdang@gmail.com",
                            Password = "123456",
                            Role = "Customer"
                        },
                        new
                        {
                            CustomerID = 2,
                            City = "Vung Tau",
                            Country = "VietNam",
                            CustomerName = "Ho Hai Nam",
                            DateOfBirth = new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7524),
                            Email = "nam.hohai@gmail.com",
                            Password = "123456",
                            Role = "Customer"
                        },
                        new
                        {
                            CustomerID = 3,
                            City = "Binh Duong",
                            Country = "VietNam",
                            CustomerName = "Thai Thanh Phat",
                            DateOfBirth = new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7525),
                            Email = "phat.thaithanh@gmail.com",
                            Password = "123456",
                            Role = "Customer"
                        });
                });

            modelBuilder.Entity("BusinessObjects.FlowerBouquet", b =>
                {
                    b.Property<int>("FlowerBouquetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlowerBouquetID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("FlowerBouquetName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("FlowerBouquetStatus")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("FlowerBouquetID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SupplierID");

                    b.ToTable("FlowerBouquets");

                    b.HasData(
                        new
                        {
                            FlowerBouquetID = 1,
                            CategoryID = 1,
                            Description = "This is Rose",
                            FlowerBouquetName = "Red Rose",
                            FlowerBouquetStatus = 1,
                            SupplierID = 1,
                            UnitPrice = 100000,
                            UnitsInStock = 100
                        },
                        new
                        {
                            FlowerBouquetID = 2,
                            CategoryID = 1,
                            Description = "This is Orchis",
                            FlowerBouquetName = "Orchis",
                            FlowerBouquetStatus = 1,
                            SupplierID = 1,
                            UnitPrice = 100000,
                            UnitsInStock = 100
                        },
                        new
                        {
                            FlowerBouquetID = 3,
                            CategoryID = 1,
                            Description = "This is Sun Flower",
                            FlowerBouquetName = "Sun Flower",
                            FlowerBouquetStatus = 1,
                            SupplierID = 1,
                            UnitPrice = 100000,
                            UnitsInStock = 100
                        });
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Freight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderID = 1,
                            CustomerID = 1,
                            Freight = "COD",
                            OrderDate = new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7541),
                            OrderStatus = 1,
                            ShippedDate = new DateTime(2023, 5, 25, 16, 40, 49, 508, DateTimeKind.Local).AddTicks(7542),
                            Total = 300000
                        });
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("FlowerBouquetID")
                        .HasColumnType("int");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderID", "FlowerBouquetID");

                    b.HasIndex("FlowerBouquetID");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderID = 1,
                            FlowerBouquetID = 1,
                            Discount = 0,
                            Quantity = 1,
                            UnitPrice = 100000
                        },
                        new
                        {
                            OrderID = 1,
                            FlowerBouquetID = 2,
                            Discount = 0,
                            Quantity = 2,
                            UnitPrice = 100000
                        });
                });

            modelBuilder.Entity("BusinessObjects.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierID"), 1L, 1);

                    b.Property<string>("SupplierAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");

                    b.HasData(
                        new
                        {
                            SupplierID = 1,
                            SupplierAddress = "46 Nguyen Thai Binh, Ho Chi Minh",
                            SupplierName = "Flower Shop Ciaoflora",
                            Telephone = "0123456789"
                        },
                        new
                        {
                            SupplierID = 2,
                            SupplierAddress = "46 Nguyen Chanh Sat, Vung Tau",
                            SupplierName = "Ant Flower",
                            Telephone = "0123456789"
                        });
                });

            modelBuilder.Entity("BusinessObjects.FlowerBouquet", b =>
                {
                    b.HasOne("BusinessObjects.Category", "Category")
                        .WithMany("FlowerBouquets")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Supplier", "Supplier")
                        .WithMany("FlowerBouquets")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.HasOne("BusinessObjects.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.FlowerBouquet", "FlowerBouquet")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FlowerBouquetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlowerBouquet");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Navigation("FlowerBouquets");
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.FlowerBouquet", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.Supplier", b =>
                {
                    b.Navigation("FlowerBouquets");
                });
#pragma warning restore 612, 618
        }
    }
}
