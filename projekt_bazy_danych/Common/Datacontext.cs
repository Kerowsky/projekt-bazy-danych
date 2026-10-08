using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common
{
    class Datacontext : DbContext
    {
        private readonly string connectionString;

        #region DataSets
        public DbSet<Fabryka> Fabryki { get; set; }

        public DbSet<Klient> Klienci { get; set; }

        public DbSet<Zamowienie> Zamowienia { get; set; }

        public DbSet<Pojazd> Pojazdy { get; set; }
        #endregion

        public Datacontext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            this.connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public Datacontext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usunięto duplikat relacji Klient-Zamówienie, zostawiono poprawną wersję
            modelBuilder.Entity<Klient>()
                .HasMany(k => k.Id_zamowienia)
                .WithOne(z => z.Klient);

            modelBuilder.Entity<Zamowienie>()
                .HasOne(z => z.Pojazd)
                .WithOne()
                .HasForeignKey<Zamowienie>(z => z.Vin_pojazdu);
        }
    }
}