using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopDotNet.Modello.Contratti
{
    public interface INotificaAllarmi
    {
        void NotificaTemperaturaTroppoAlta(double temperatura);
    }
}
