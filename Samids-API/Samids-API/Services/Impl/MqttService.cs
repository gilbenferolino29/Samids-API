using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;


using MQTTnet;
using MQTTnet.Client;

using Samids_API.Services.Interface;
using Samids_API.Services.Impl;
using Samids_API.Models;
using Samids_API.Data;

using System.Text.Json;
using System.Text;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using NumSharp;
using NumsharpOpencvSharpConvertor;
using System.Diagnostics;
using NumSharp.Generic;


namespace Samids_API.Services
{
    public class MqttService : IMqttService
    {
        private readonly IStudentService _studentService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IMqttClient mqttClient;
        private readonly MqttFactory mqttFactory = new MqttFactory();
        private readonly MqttClientOptions options;
        private readonly ILogger<MqttService> _logger;

        public static readonly string clientId = "API_CLIENT";
        private static readonly string pubTopic = $"mqtt/API/{clientId}";
        private static readonly string subTopic = "mqtt/RFID/+";

        public MqttService(MqttClientOptions options, ILogger<MqttService> logger
            , IServiceScopeFactory serviceScopeFactory
            //, IStudentService studentService
            )
        {
            this.options = options;
            mqttClient = mqttFactory.CreateMqttClient();
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            //_studentService = studentService;
            ConfigureMqttClient();
        }

        private void ConfigureMqttClient()
        {
            mqttClient.ConnectedAsync += HandleConnectedAsync;
            //mqttClient.DisconnectedAsync += HandleDisconnectedAsync;
            mqttClient.ApplicationMessageReceivedAsync += HandleApplicationMessageReceivedAsync;
        }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            //throw new System.NotImplementedException();
            String[] tokens = eventArgs.ApplicationMessage.Topic.Split('/');
            String tokClientID = tokens.Last();

            Console.WriteLine($"Received application message.");
            //e.DumpToConsole();
            var payload = eventArgs.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);

            Console.WriteLine(
            $"===================================================\n" +
            $"-- TimeStamp: {DateTime.Now} -- \n" +
            $"ClientID: {tokClientID}, \n" +
            $"Topic = {eventArgs.ApplicationMessage?.Topic}, \n" +
            $"Payload = {payload}, \n" +
            $"QoS = {eventArgs.ApplicationMessage?.QualityOfServiceLevel}, \n" +
            $"Retain-Flag = {eventArgs.ApplicationMessage?.Retain}\n"
            );

            RFID? json = JsonSerializer.Deserialize<RFID>(payload);

            Console.WriteLine(json);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var myScopedService = scope.ServiceProvider.GetRequiredService<IStudentService>();

                CRUDReturn crudtudent = await myScopedService!.GetStudentByRfid(json!._Id);

                if (crudtudent.success || crudtudent != null)
                {
                    Console.WriteLine("BOANGGGGGG");

                    //NDArray bytearr = np.array(json._Img?.Buffer, dtype: np.uint8);

                    //Debug.WriteLine($">> Test:::\n {bytearr.shape}");
                    //for (int i = 0; i < bytearr.shape[0]; i++)
                    //{
                    //    Console.WriteLine((char)bytearr[i]);
                    //}
                    //Mat img = Cv2.ImDecode(bytearr.ToMat(), ImreadModes.Unchanged);

                    //Cv2.ImShow("test", img);




                    //Cv2.WaitKey(0);
                    //Cv2.DestroyAllWindows();
                }
                else
                {
                    Console.WriteLine("Json failed");
                }
            }

            


            
            

                //Console.WriteLine(respStudent.Rfid);

            //payload.DumpToConsole();

            // publish method here

            //return Task.CompletedTask; 
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            var mqttSubscribeOptions = mqttFactory
                                        .CreateSubscribeOptionsBuilder()
                                        .WithTopicFilter(
                                            f =>
                                            {
                                                f.WithTopic(subTopic);
                                            })
                                        .Build();

            _logger.LogInformation("connected");
            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {

            _logger.LogInformation("HandleDisconnected");
            #region Reconnect_Using_Event :https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
            /*
            * This sample shows how to reconnect when the connection was dropped.
            * This approach uses one of the events from the client.
            * This approach has a risk of dead locks! Consider using the timer approach (see sample).
            * The following reconnection code "Reconnect_Using_Timer" is recommended
           */
            if (eventArgs.ClientWasConnected)
            {
                // Use the current options as the new options.
                await mqttClient.ConnectAsync(this.options);
            }
            #endregion
            await Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var timeout = new CancellationTokenSource(5000);

            #region Reconnect_Using_Timer:https://github.com/dotnet/MQTTnet/blob/master/Samples/Client/Client_Connection_Samples.cs
            /* 
             * This sample shows how to reconnect when the connection was dropped.
             * This approach uses a custom Task/Thread which will monitor the connection status.
            * This is the recommended way but requires more custom code!
           */
            _ = Task.Run(
           async () =>
           {
               // // User proper cancellation and no while(true).
               while (!timeout.IsCancellationRequested)
               {
                   try
                   {
                       // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                       if (!await mqttClient.TryPingAsync())
                       {
                           await mqttClient.ConnectAsync(this.options, timeout.Token);

                           // Subscribe to topics when session is clean etc.
                           _logger.LogInformation("The MQTT client is connected.");
                       }
                   }
                   catch (Exception ex)
                   {
                       // Handle the exception properly (logging etc.).
                       _logger.LogError(ex, "The MQTT client failed to connect.");
                   }
                   finally
                   {
                       Console.WriteLine("Pinging server every 5 seconds if disconnected...");
                       // Check the connection state every 5 seconds and perform a reconnect if required.
                       await Task.Delay(TimeSpan.FromSeconds(5));
                   }
               }
           });
            #endregion

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                var disconnectOption = new MqttClientDisconnectOptions
                {
                    Reason = MqttClientDisconnectReason.ImplementationSpecificError,
                    ReasonString = "Calling _DisconnectAsync_ to send a DISCONNECT packet before closing the connection."
                };
                await mqttClient.DisconnectAsync(disconnectOption, cancellationToken);
            }
            await mqttClient.DisconnectAsync();
        }
    }
}
