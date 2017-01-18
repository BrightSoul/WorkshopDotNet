using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Servizi;

namespace WorkshopDotNet.Web.Controllers
{
    public class DispositiviController : Controller
    {
        // GET: Dispositivi
        public ActionResult Index()
        {
            // list all the devices in the DB


            using (Contesto contesto = new Contesto())
            {
                var elencoDispositivi = contesto.Set<Dispositivo>().ToList();
                //numerorisultati = contesto.Set<Telemetria>().Where(telemet => telemet.DataSalvataggio == telemetria.DataSalvataggio).Count();
                return View(elencoDispositivi);
            }

            
        }

        // GET: Dispositivi/Details/5
        public ActionResult Details(int? id)
        {

            if (!id.HasValue)
            {
                TempData["msg"] = "Non è stato indicato il dispositivo da visualizzare";
                return RedirectToAction(nameof(Index));
            }

            using (Contesto ctx = new Contesto())
            {
                Dispositivo disp = ctx.Set<Dispositivo>().Where(d => d.IdDispositivo == id).FirstOrDefault();
                if (disp == null)
                {
                    TempData["msg"] = "Il dispositivo che volevi visualizzare non esiste";
                    return RedirectToAction(nameof(Index));
                }

                return View(disp);
            }
        }

        // GET: Dispositivi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dispositivi/Create
        [HttpPost]
        public async Task<ActionResult> Create(Dispositivo dispositivo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (Contesto ctx = new Contesto())
                    {
                        ctx.Set<Dispositivo>().Add(dispositivo);
                        await ctx.SaveChangesAsync();
                    }

                    return RedirectToAction("Index");
                } else
                {
                    throw new InvalidOperationException("Attenzione, il modello non era valido");
                }
            }
            catch
            {
                return View(dispositivo);
            }
        }

        // GET: Dispositivi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["msg"] = "Non è stato indicato il dispositivo da modificare";
                return RedirectToAction(nameof(Index));
            }

            using (Contesto ctx = new Contesto())
            {
                Dispositivo disp = ctx.Set<Dispositivo>().Where(d => d.IdDispositivo == id).FirstOrDefault();
                if (disp == null) {
                    TempData["msg"] = "Il dispositivo che volevi modificare non esiste";
                    return RedirectToAction(nameof(Index));
                }

                return View(disp);

                //ctx.Set<Dispositivo>().Find(5, "B", 9045);
            }
        }

        // POST: Dispositivi/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, FormCollection formCollection)
        {
            if (!id.HasValue)
            {
                TempData["msg"] = "Non è stato indicato il dispositivo da modificare";
                return RedirectToAction(nameof(Index));
            }

            using (Contesto ctx = new Contesto())
            {
                Dispositivo disp = ctx.Set<Dispositivo>().Where(d => d.IdDispositivo == id).FirstOrDefault();
                if (disp == null)
                {
                    TempData["msg"] = "Il dispositivo che volevi modificare non esiste";
                    return RedirectToAction(nameof(Index));
                }

                TryUpdateModel(disp);

                await ctx.SaveChangesAsync();
            }
            TempData["msg-ok"] = "Aggiornamento eseguito correttamente";
            return RedirectToAction(nameof(Index));
        }

        // GET: Dispositivi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dispositivi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
