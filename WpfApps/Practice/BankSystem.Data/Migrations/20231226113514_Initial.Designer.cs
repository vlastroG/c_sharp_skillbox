﻿// <auto-generated />
using BankSystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankSystem.Data.Migrations
{
    [DbContext(typeof(ClientsDbContext))]
    [Migration("20231226113514_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("BankSystem.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_passport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_patronymic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
