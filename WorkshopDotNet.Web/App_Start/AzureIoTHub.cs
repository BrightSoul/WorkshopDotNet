using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;
using WorkshopDotNet.Web.Hubs;
using Newtonsoft.Json;
using WorkshopDotNet.Modello.Entita;

namespace WorkshopDotNet.Web.App_Start
{
    public static class AzureIoTHub
    {

        static string connectionString = "HostName=workshopdotnet.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=TxMPZzlZEO+L2tucIUGxIXqyhTH9a+cI7PflW7EWiGY=";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        public static void IniziaConRicezioneMessaggi()
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            CancellationTokenSource cts = new CancellationTokenSource();

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition, cts.Token));
            }
            //Task.WaitAll(tasks.ToArray());

        }


        private static async Task ReceiveMessagesFromDeviceAsync(string partition, CancellationToken ct)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventHubReceiver.ReceiveAsync(TimeSpan.FromSeconds(2));
                if (eventData == null) continue;

                var deviceId = eventData.SystemProperties["iothub-connection-device-id"] as string;
                deviceId = deviceId.Substring("dispositivo".Length);
                

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Debug.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);
                var telemetria = JsonConvert.DeserializeObject<Telemetria>(data);
                telemetria.IdDispositivo = int.Parse(deviceId);

                TelemetriaHub.InviaMessaggio(telemetria);

            }
        }
    }
}