using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Entita;

namespace WorkshopDotNet.Servizi
{
    public class LeggiMessaggio
    {
        private readonly INotificaAllarmi notificaAllarmi;

        public LeggiMessaggio(INotificaAllarmi notificaAllarmi)
        {
            this.notificaAllarmi = notificaAllarmi;
        }

        public Telemetria Leggi(string messaggio)
        {
            Telemetria telemetria = JsonConvert.DeserializeObject<Telemetria>(messaggio);

            if (telemetria.Temperatura > 10) {

                notificaAllarmi.NotificaTemperaturaTroppoAlta(telemetria.Temperatura);
            }

            return telemetria;
        }
    }
}
