using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Servizi;

namespace WorkshopDotNet.WebAPI.Controllers
{
    public class DispositiviController : ApiController
    {
        private readonly Contesto contesto;
        public DispositiviController()
        {
            this.contesto = new Contesto();
        }

        // GET: api/Dispositivi
        [EnableQuery]
        public IQueryable<Dispositivo> Get()
        {
            return contesto.Set<Dispositivo>();
        }

        // GET: api/Dispositivi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dispositivi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dispositivi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dispositivi/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            contesto.Dispose();
            base.Dispose(disposing);
        }
    }
}
