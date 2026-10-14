using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using projekt_bazy_danych.Entities;
using projekt_bazy_danych.Common.Providers;

namespace projekt_bazy_danych.Common
{
    public class Datacontext : DbContext
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
                try
                {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Błąd połączenia z bazą danych : {ex.Message}");
                }
            }
        }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klient>()
                .HasMany(k => k.Id_zamowienia)
                .WithOne(z => z.Klient)
                .HasForeignKey(z => z.KlientId);

            modelBuilder.Entity<Zamowienie>()
                .HasOne(z => z.Pojazd)
                .WithOne()
                .HasForeignKey<Zamowienie>(z => z.Vin_pojazdu);

            modelBuilder.Entity<Klient>().HasData(DataSeedingProvider.GenerateKlient());

            modelBuilder.Entity<Pojazd>().HasData(DataSeedingProvider.GeneratePojazd());

            modelBuilder.Entity<Zamowienie>().HasData(DataSeedingProvider.GenerateZamowienia());
        }
    }
}