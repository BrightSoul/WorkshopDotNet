using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopDotNet.Modello.Entita
{
    public class Telemetria
    {
        public double Temperatura { get; set; }
        public int Umidita { get; set; }
        public int IdTelemetria { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataSalvataggio { get; set; }
        public int IdDispositivo { get; set; }
        public virtual Dispositivo Dispositivo { get; set; }
    }
}