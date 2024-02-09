using GoodsStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;

namespace GoodsStore.Context {
    internal class ClientsDbDemoContext : DbContext {
        public ClientsDbDemoContext() : base() {

        }

        public ClientsDbDemoContext(DbContextOptions<ClientsDbDemoContext> options) : base(options) { }


        public virtual DbSet<Client> Clients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var clientsConnection = new SqlConnectionStringBuilder() {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = Path.GetFullPath(@"..\..\..\ClientsDbDemo.mdf"),
                IntegratedSecurity = true,
                Pooling = true,
                ConnectTimeout = 30,
                Encrypt = true
            };

            optionsBuilder.UseSqlServer(clientsConnection.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Client>(entity => {
                entity.HasKey(e => e.Email).HasName("PK__Clients__A9D105357036B24E");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(50);
                entity.Property(e => e.Patronymic).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(50);
            });
        }
    }
}
