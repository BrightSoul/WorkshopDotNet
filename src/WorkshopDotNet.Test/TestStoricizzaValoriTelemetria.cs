using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopDotNet.Servizi.LettoriMessaggio;
using WorkshopDotNet.Modello.Entita;
using WorkshopDotNet.Modello.Servizi;
using System.Linq;

namespace WorkshopDotNet.Test
{
    [TestClass]
    public class TestStoricizzaValoriTelemetria
    {
        [TestMethod]
        public void StoricizzaValoriTelemetriaDeveAggiungereCorrettamenteGliOggettiTelemetria()
        {
            //arrange

            DateTime datasalvataggio = DateTime.Now;
            datasalvataggio = new DateTime(datasalvataggio.Year,datasalvataggio.Month,datasalvataggio.Day,datasalvataggio.Hour,datasalvataggio.Minute,datasalvataggio.Second);
            Dispositivo dispositivo = new Dispositivo();
            dispositivo.Descrizione = "Dispositivo di test";

            Telemetria telemetria = new Telemetria();
            telemetria.DataSalvataggio = datasalvataggio;
            telemetria.DataEvento = DateTime.Now.AddSeconds(-30);
            telemetria.Dispositivo = dispositivo;
            StoricizzaValoriTelemetria storicizzavaloritelemetria = new StoricizzaValoriTelemetria();

            //act
            storicizzavaloritelemetria.Leggi(telemetria);

            //assert
            int numerorisultati;
            using (Contesto contesto = new Contesto())
            {
                numerorisultati = contesto.Set<Telemetria>().Where(telemet => telemet.DataSalvataggio == telemetria.DataSalvataggio).Count();
            }
            Assert.AreEqual(1, numerorisultati);
        }
    }
}
