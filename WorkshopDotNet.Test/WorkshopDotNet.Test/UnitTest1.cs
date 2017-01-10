using System;
using WorkshopDotNet.Servizi;
using WorkshopDotNet.Entita;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopDotNet.Test.Mocks;
using NSubstitute;

namespace WorkshopDotNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LeggiMessaggioDeveLeggereCorrettamenteValoriDiTemperaturaEUmidita()
        {
            //arrange
            string messaggio = "{\"temperatura\": 4.5,\"umidita\": 75 }";
            MockNotificaAllarmi notificaAllarmi = new MockNotificaAllarmi();
            var leggiMessaggio = new LeggiMessaggio(notificaAllarmi);

            //act
            Telemetria telemetria = leggiMessaggio.Leggi(messaggio);

            //assert
            Assert.AreEqual(4.5, telemetria.Temperatura);
            Assert.AreEqual(75, telemetria.Umidita);

        }

        [TestMethod]
        public void SeLaTemperaturaMaggioreDi10GradiNotificaAllarme() {
            //arrange
            string messaggio = "{\"temperatura\": 10.5,\"umidita\": 75 }";
            //MockNotificaAllarmi notificaAllarmi = new MockNotificaAllarmi();
            var notificaAllarmi = Substitute.For<INotificaAllarmi>();

            var leggiMessaggio = new LeggiMessaggio(notificaAllarmi);

            //act
            Telemetria telemetria = leggiMessaggio.Leggi(messaggio);

            //assert
            //Assert.AreEqual(true, notificaAllarmi.TemperaturaTroppoAltaNotificata);
        }
    }
}
