﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Servizi;
using WorkshopDotNet.Web.Hubs;

namespace WorkshopDotNet.Web.Controllers
{
    public class TelemetriaController : ApiController
    {
        [ResponseType(typeof(Telemetria))]
        public async Task<IHttpActionResult> Post(Telemetria telemetria)
        {
            telemetria.DataSalvataggio = DateTime.UtcNow;
            using (var contesto = new Contesto())
            {
                contesto.Set<Telemetria>().Add(telemetria);
                await contesto.SaveChangesAsync();
            }

            TelemetriaHub.InviaMessaggio(telemetria);

            return Ok(telemetria);
        }
    }
}
