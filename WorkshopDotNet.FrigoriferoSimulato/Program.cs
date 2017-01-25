using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.FrigoriferoSimulato
{
    class Program
    {


        //HostName=workshopdotnet.azure-devices.net;DeviceId=dispositivo1;SharedAccessKey=mEhidUmQuG3dI0SVj2hveNgsZrPjsFYwazJ6HvT95GI=

        static DeviceClient deviceClient;
        static string iotHubUri = "workshopdotnet.azure-devices.net";
        static string deviceId = "dispositivo1";
        static string deviceKey = "mEhidUmQuG3dI0SVj2hveNgsZrPjsFYwazJ6HvT95GI=";


        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey));

            SendDeviceToCloudMessagesAsync();

            Console.ReadLine();
        }


        private static async void SendDeviceToCloudMessagesAsync()
        {
            double temperatura = 4; // °C
            int umidita = 70; // %
            Random rand = new Random();

            while (true)
            {
                double temperaturaAttuale = temperatura + rand.NextDouble() * 2 - 1;
                int umiditaAttuale = umidita + rand.Next(0, 20) - 10;

                var telemetria = new Telemetria
                {
                     Temperatura = temperaturaAttuale,
                     Umidita = umiditaAttuale,
                     DataEvento = DateTime.UtcNow
                };
                var messageString = JsonConvert.SerializeObject(telemetria);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();
            }
        }


    }
}
