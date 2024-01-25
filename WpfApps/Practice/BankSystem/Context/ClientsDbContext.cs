using BankSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Context {
    internal class ClientsDbContext : DbContext {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<BankAccountGeneral> BankAccountsGeneral { get; set; }

        public DbSet<BankAccountDeposit> BankAccountsDeposit { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=clients.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Client>().Property("_name");
            modelBuilder.Entity<Client>().Property("_surname");
            modelBuilder.Entity<Client>().Property("_patronymic");
            modelBuilder.Entity<Client>().Property("_passport");
            modelBuilder.Entity<Client>().Property("_phone");
            modelBuilder.Entity<Client>().Property("_lastChangeTime");
            modelBuilder.Entity<Client>().Property("_lastChangeData");
            modelBuilder.Entity<Client>().Property("_lastChangeDescription");
            modelBuilder.Entity<Client>().Property("_lastChangeBy");

            modelBuilder.Entity<BankAccountGeneral>().UseTpcMappingStrategy().ToTable("BankAccountsGeneral");
            modelBuilder.Entity<BankAccountGeneral>().HasKey(e => e.Number);
            modelBuilder.Entity<BankAccountDeposit>().UseTpcMappingStrategy().ToTable("BankAccountsDeposit");

            //modelBuilder.Entity<Client>()
            //    .HasOne(e => e.BankAccountDeposit)
            //    .WithOne(e => e.ClientWithDepositAccount)
            //    .HasForeignKey<BankAccountDeposit>(e => e.ClientWithDepositAccountId);
            //modelBuilder.Entity<Client>()
            //    .HasOne(e => e.BankAccountGeneral)
            //    .WithOne(e => e.ClientWithGeneralAccount)
            //    .HasForeignKey<BankAccountGeneral>(e => e.ClientWithGeneralAccountId);

        }
    }
}
