using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Servizi
{
    public interface ILettoreTelemetria
    {
        void Leggi(Telemetria telemetria);
    }
}
