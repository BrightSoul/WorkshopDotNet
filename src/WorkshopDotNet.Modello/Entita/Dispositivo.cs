using System;
using System.Collections.Generic;

namespace WorkshopDotNet.Modello.Entita
{
    public class Dispositivo
    {
        public int IdDispositivo { get; set; }
        public string Descrizione { get; set; }
        public DateTime? DataInstallazione { get; set; }
        public ICollection<Telemetria> Telemetria { get; set; }
    }
}