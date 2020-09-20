using KontoVerwaltungV4.Konto;
using KontoVerwaltungV4.Transaktionen;
using Microsoft.EntityFrameworkCore;

namespace KontoVerwaltungV4.Database
{
    public class BankingContext : DbContext
    {
        public DbSet<Kunde.Kunde> KundenSet { get; set; }

        //TODO:EFCore unterstützt in jetziger version noch kein Table-per-Concrete-Type Zuordnung deswegen werden alle Tabellen einzeln benötigt
        public DbSet<Konto.Konto> KontoSet { get; set; }
        public DbSet<GiroKonto> GiroKontoSet { get; set; }
        public DbSet<TagesgeldKonto> TagesgeldKontoSet { get; set; }
        public DbSet<FestgeldKonto> FestgeldKontoSet { get; set; }
        public DbSet<SparKonto> SparKontoSet { get; set; }


        public DbSet<Transaktion> TransaktionsSet { get; set; }

        /// <summary>
        ///     Database Config in Runtime
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=KontoDB.db");
            options.UseLazyLoadingProxies();
        }

        /// <summary>
        ///     Database CreationModeling Settings
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Konto.Konto>()
                .HasDiscriminator<string>("konto_type");
        }
    }
}