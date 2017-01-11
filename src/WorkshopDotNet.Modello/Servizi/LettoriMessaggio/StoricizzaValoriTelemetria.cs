using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Servizi;

namespace WorkshopDotNet.Servizi.LettoriMessaggio
{
    public class StoricizzaValoriTelemetria : ILettoreTelemetria
    {
        public void Leggi(Telemetria telemetria)
        {
            using(Contesto contesto = new Contesto())
            {
                contesto.Set<Telemetria>().Add(telemetria);
                contesto.SaveChanges();
            }


            //throw new NotImplementedException();
        }
    }
}
