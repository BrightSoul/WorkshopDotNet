using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkshopDotNet.Modello.Entita
{
    public class Dispositivo
    {
        public int IdDispositivo { get; set; }
        [Required, StringLength(100, MinimumLength = 10)]
        public string Descrizione { get; set; }
        public DateTime? DataInstallazione { get; set; }
        public ICollection<Telemetria> Telemetria { get; set; }
    }
}