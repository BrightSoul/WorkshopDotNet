using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopDotNet.Servizi;

namespace WorkshopDotNet.Test.Mocks
{
    internal class MockNotificaAllarmi : INotificaAllarmi
    {
        public MockNotificaAllarmi()
        {
            TemperaturaTroppoAltaNotificata = false;
        }

        public bool TemperaturaTroppoAltaNotificata { get; private set; }

        public void NotificaTemperaturaTroppoAlta(double temperatura)
        {
            TemperaturaTroppoAltaNotificata = true;
        }
    }
}
