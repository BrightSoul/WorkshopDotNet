using System;
using WorkshopDotNet.Servizi;
using WorkshopDotNet.Modello.Entita;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkshopDotNet.Modello.Contratti;
using WorkshopDotNet.Servizi.LettoriMessaggio;

namespace WorkshopDotNet.Test
{
    [TestClass]
    public class TestLetturaMessaggi
    {
        [TestMethod]
        public void LeggiMessaggioDeveLeggereCorrettamenteValoriDiTemperaturaEUmidita()
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

        [TestMethod]
        public void LeggiMessaggioDeveRichiamareIMetodiLeggiDeiSuoiLettori()
        {
            //arrange
            var lettore1 = Substitute.For<ILettoreTelemetria>();
            var lettore2 = Substitute.For<ILettoreTelemetria>();
            string messaggio = "{\"temperatura\": 4.5,\"umidita\": 75 }";
            var leggiMessaggio = new LeggiMessaggio(
                    lettore1,
                    lettore2
                );

            //act
            var telemetria = leggiMessaggio.Leggi(messaggio);

            //assert
            lettore1.Received(1).Leggi(telemetria);
            lettore2.Received(1).Leggi(telemetria);
        }

        [TestMethod]
        public void SeLaTemperaturaMaggioreDi10GradiNotificaAllarme() {
            //arrange
            var telemetria = new Telemetria { Temperatura = 10.5 };
            var notificaAllarmi = Substitute.For<INotificaAllarmi>();
            var leggiMessaggio = new VerificaAllarmiTemperatura(notificaAllarmi);

            //act
            leggiMessaggio.Leggi(telemetria);

            //assert
            notificaAllarmi.Received().NotificaTemperaturaTroppoAlta(10.5);
        }
    }
}
