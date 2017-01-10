using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Contratti;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Servizi.LettoriMessaggio
{
    public class VerificaAllarmiTemperatura : ILettoreTelemetria
    {
        private readonly INotificaAllarmi notificaAllarmi;

        public VerificaAllarmiTemperatura(INotificaAllarmi notificaAllarmi)
        {
            this.notificaAllarmi = notificaAllarmi;
        }
        public void Leggi(Telemetria telemetria)
        {
            if (telemetria.Temperatura > 10)
            {
                notificaAllarmi.NotificaTemperaturaTroppoAlta(telemetria.Temperatura);
            }
        }

    }
}
