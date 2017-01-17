using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Migrations;

namespace WorkshopDotNet.Modello.Servizi
{
    public class Contesto : DbContext
    {
        static Contesto()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Contesto>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contesto, Configuration>());
        }

        public Contesto() : this("Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=WorkshopDotNet;Integrated Security=True")
        {

        }
        public Contesto(string stringaconnessione) : base(stringaconnessione)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Telemetria>()
                .ToTable("telemetria")
                .HasKey(telemetria => telemetria.IdTelemetria);

            modelBuilder
                .Entity<Dispositivo>()
                .ToTable("dispositivo")
                .HasKey(dispositivo => dispositivo.IdDispositivo);

            // Not Nullable
            modelBuilder
                .Entity<Dispositivo>()
                .HasMany(dispositivo => dispositivo.Telemetria)
                .WithRequired(telemetria => telemetria.Dispositivo)
                .HasForeignKey(telemetria => telemetria.IdDispositivo);

            /*
            modelBuilder
                .Entity<Dispositivo>()
                .Property(d => d.Descrizione)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            */
           /* modelBuilder
                .Entity<Telemetria>()
                .HasRequired(telemetria => telemetria.Dispositivo)
                .WithMany(dispositivo => dispositivo.Telemetria)
                .HasForeignKey(telemetria => telemetria.IdDispositivo); */
            

        }

        public System.Data.Entity.DbSet<WorkshopDotNet.Modello.Entita.Dispositivo> Dispositivoes { get; set; }
    }
}
