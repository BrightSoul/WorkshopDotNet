using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Modello.Servizi
{
    public class Contesto:DbContext
    {
        static Contesto()
        {
            //Database.SetInitializer<Contesto>(new MigrateDatabaseToLatestVersion<Contesto>)
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

           /* modelBuilder
                .Entity<Telemetria>()
                .HasRequired(telemetria => telemetria.Dispositivo)
                .WithMany(dispositivo => dispositivo.Telemetria)
                .HasForeignKey(telemetria => telemetria.IdDispositivo); */
        }
    }
}
