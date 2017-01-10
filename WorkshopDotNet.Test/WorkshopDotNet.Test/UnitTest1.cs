using System;
using WorkshopDotNet.Servizi;
using WorkshopDotNet.Entita;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkshopDotNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            string messaggio = "{\"temperatura\": 4.5,\"umidita\": 75 }";
            var leggiMessaggio = new LeggiMessaggio();

            //act
            Telemetria telemetria = leggiMessaggio.Leggi(messaggio);

            //assert
            Assert.AreEqual(4.5, telemetria.Temperatura);
            Assert.AreEqual(75, telemetria.Umidita);

        }
    }
}
