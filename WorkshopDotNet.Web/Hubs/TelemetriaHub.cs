using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Web.Hubs
{
    public class TelemetriaHub : Hub
    {
        public static void InviaMessaggio(Telemetria telemetria)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<TelemetriaHub>();
            var nomeGruppo = String.Concat("dispositivo", telemetria.IdDispositivo);
            context.Clients.Group(nomeGruppo).riceviMessaggio(telemetria);
        }

        public void SottoscriviRicezione(int idDispositivo)
        {
            Groups.Add(Context.ConnectionId, String.Concat("dispositivo", idDispositivo));
        }
    }
}