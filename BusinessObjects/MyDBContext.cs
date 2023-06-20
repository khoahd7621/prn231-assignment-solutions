using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class MyDBContext : IdentityDbContext<Customer>
    {
        public MyDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FlowerBouquet> FlowerBouquets { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetail>()
                .HasKey(r => new { r.OrderID, r.FlowerBouquetID });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Rose", Description = "This is Rose" },
                new Category { CategoryID = 2, CategoryName = "Peonies", Description = "This is Peonies" },
                new Category { CategoryID = 3, CategoryName = "Lily", Description = "This is Lily" },
                new Category { CategoryID = 4, CategoryName = "Carnation", Description = "This is Carnation" }
                );
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    SupplierID = 1,
                    SupplierName = "Flower Shop Ciaoflora",
                    SupplierAddress = "46 Nguyen Thai Binh, Ho Chi Minh",
                    Telephone = "0123456789"
                },
                new Supplier
                {
                    SupplierID = 2,
                    SupplierName = "Ant Flower",
                    SupplierAddress = "46 Nguyen Chanh Sat, Vung Tau",
                    Telephone = "0123456789"
                }
                );
            modelBuilder.Entity<FlowerBouquet>().HasData(
                new FlowerBouquet()
                {
                    FlowerBouquetID = 1,
                    FlowerBouquetName = "Red Rose",
                    Description = "This is Rose",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = 1,
                    SupplierID = 1
                },
                new FlowerBouquet()
                {
                    FlowerBouquetID = 2,
                    FlowerBouquetName = "Orchis",
                    Description = "This is Orchis",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = 1,
                    SupplierID = 1
                },
                new FlowerBouquet()
                {
                    FlowerBouquetID = 3,
                    FlowerBouquetName = "Sun Flower",
                    Description = "This is Sun Flower",
                    UnitPrice = 100000,
                    UnitsInStock = 100,
                    FlowerBouquetStatus = 1,
                    CategoryID = 1,
                    SupplierID = 1
                }
                );
        }
    }
}
