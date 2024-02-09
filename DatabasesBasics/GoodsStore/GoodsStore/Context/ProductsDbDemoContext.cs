using GoodsStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;

namespace GoodsStore.Context {
    internal class ProductsDbDemoContext : DbContext {
        public ProductsDbDemoContext() : base() {

        }


        public ProductsDbDemoContext(DbContextOptions<ProductsDbDemoContext> options) : base(options) { }


        public virtual DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var clientsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ProductsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true,
                ConnectTimeout = 30,
                Encrypt = true
            };

            optionsBuilder.UseSqlServer(clientsConnection.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>(entity => {
                entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0781EC7113");

                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.ProductCode).HasMaxLength(50);
            });
        }
    }
}
