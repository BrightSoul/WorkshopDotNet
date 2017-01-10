using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopDotNet.Servizi
{
    public interface INotificaAllarmi
    {
        void NotificaTemperaturaTroppoAlta(double temperatura);
    }
}
