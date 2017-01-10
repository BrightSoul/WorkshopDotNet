using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Servizi
{
    public class LeggiMessaggio
    {
        private readonly ILettoreTelemetria[] lettoriTelemetria;

        public LeggiMessaggio(params ILettoreTelemetria[] lettoriTelemetria)
        {
            this.lettoriTelemetria = lettoriTelemetria;
        }

        public Telemetria Leggi(string messaggio)
        {
            Telemetria telemetria = JsonConvert.DeserializeObject<Telemetria>(messaggio);
            foreach (var lettoreTelemetria in lettoriTelemetria)
            {
                lettoreTelemetria.Leggi(telemetria);
            }
            return telemetria;
        }
    }
}
