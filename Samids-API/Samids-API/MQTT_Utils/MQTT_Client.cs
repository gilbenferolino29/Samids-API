using System.Text;
using System.Net;
using System.Text.Json;
using System.IO;
using System.Security.Authentication;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;

using Samids_API.Models;
using Microsoft.Extensions.Configuration;
using MQTTnet.AspNetCore;

namespace Samids_API.MQTT_Utils
{
    public static class MQTT_Client
    {
        private static readonly string clientId = "API_CLIENT";
        private static readonly string pubTopic = $"mqtt/API/{clientId}";
        private static readonly string subTopic = "mqtt/RFID/+";


        public static async Task StartClient()
        {
            var env = File.ReadAllText(@"./environment.json");
            var broker = JsonSerializer.Deserialize<Broker>(env);
            broker.DumpToConsole();

            /*
             * This sample creates a simple MQTT client and connects to a public broker using a WebSocket connection.
             * 
             * This is a modified version of the sample _Connect_Client_! See other sample for more details.
             */
            var mqttFactory = new MqttFactory();

            var mqttClient = mqttFactory.CreateMqttClient();
            {
                //MqttClientConnectResult response;
                var mqttClientOptions = new MqttClientOptionsBuilder()
                                            .WithTcpServer(broker?.HiveTest?.Url, broker?.HiveTest?.Port)
                                            .WithClientId(clientId)
                                            .WithCredentials(broker?.HiveTest?.Uname, broker?.HiveTest?.Pass)
                                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                                            .WithTls(
                                            o =>
                                            {
                                                // The used public broker sometimes has invalid certificates. This sample accepts all
                                                // certificates. This should not be used in live environments.
                                                o.CertificateValidationHandler = _ => true;

                                                // The default value is determined by the OS. Set manually to force version.
                                                o.SslProtocol = SslProtocols.Tls12;
                                            })
                                            .WithCleanSession()
                                            .Build();

                Console.WriteLine("Client Options build done...");

                // In MQTTv5 the response contains much more information.
                var timeout = new CancellationTokenSource(5000);

                _ = Task.Run(
                async () =>
                {
                    // User proper cancellation and no while(true).
                    while (!timeout.IsCancellationRequested)
                    {
                        try
                        {
                            //Console.WriteLine("I TRIED...... T_T");

                            // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                            if (!await mqttClient.TryPingAsync())
                            {

                                // Setup message handling before connecting so that queued messages
                                // are also handled properly. When there is no event handler attached all
                                // received messages get lost.
                                mqttClient.ApplicationMessageReceivedAsync += e =>
                                {
                                    String[] tokens = e.ApplicationMessage.Topic.Split('/');
                                    String tokClientID = tokens.Last();

                                    Console.WriteLine($"Received application message.");
                                    //e.DumpToConsole();
                                    var payload = e.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                                    Console.WriteLine(
                                    $"===================================================\n" +
                                    $"-- TimeStamp: {DateTime.Now} -- \n" +
                                    $"ClientID: {tokClientID}, \n" +
                                    $"Topic = {e.ApplicationMessage?.Topic}, \n" +
                                    $"Payload = {payload}, \n" +
                                    $"QoS = {e.ApplicationMessage?.QualityOfServiceLevel}, \n" +
                                    $"Retain-Flag = {e.ApplicationMessage?.Retain}\n"
                                    );

                                    RFID? json = JsonSerializer.Deserialize<RFID>(payload);

                                    Console.WriteLine(json._Id);

                                    //Student? respStudent = GetStudentByRfid()

                                    //payload.DumpToConsole();

                                    // publish method here

                                    return Task.CompletedTask;
                                };

                                var response = await mqttClient.ConnectAsync(mqttClientOptions, timeout.Token);
                                Console.WriteLine("The MQTT client is connected.");
                                //response.DumpToConsole();

                                // Subscribe to topics when session is clean etc.
                                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                                                                        .WithTopicFilter(
                                                                            f =>
                                                                            {
                                                                                f.WithTopic(subTopic);
                                                                            })
                                                                        .Build();

                                var subresp = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                                subresp.DumpToConsole();

                                Console.WriteLine("MQTT client subscribed to topic.");

                            }

                        }
                        catch (Exception e)
                        {
                            // Handle the exception properly (logging etc.).
                            Console.WriteLine("The MQTT client failed to connect.");
                            Console.WriteLine(e);
                            break;
                        }
                        finally
                        {
                            Console.WriteLine("Pinging server every 3 seconds if disconnected...");
                            // Check the connection state every 3 seconds and perform a reconnect if required.
                            await Task.Delay(TimeSpan.FromSeconds(3));
                        }
                    }

                });

                Console.WriteLine("The MQTT client is disconnected.");

                // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
                // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
                var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

                await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
            }
        }

    }

    


}
