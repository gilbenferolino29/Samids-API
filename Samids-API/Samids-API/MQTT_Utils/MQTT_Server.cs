using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Net;

using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace Samids_API.MQTT_Utils
{
    public static class MQTT_Server
    {
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder
        //              .UseKestrel(o =>
        //              {
        //                  // This will allow MQTT connections based on HTTP WebSockets with URI "localhost:5000/mqtt" | Default HTTP pipeline
        //                  o.ListenAnyIP(5000);
        //                  // This will allow MQTT connections based on TCP port 1412. | MQTT pipeline
        //                  o.ListenAnyIP(1412, l => l.UseMqtt());
        //              })
        //            .UseStartup<Startup>();
        //        });
        
        public static Task Start_MqttServer()
        {
            /*
             * This sample starts a minimal ASP.NET Webserver including a hosted MQTT server.
             */

            var host = Host.CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseKestrel(
                            o =>
                            {
                                // This will allow MQTT connections based on TCP port 1412.
                                o.ListenAnyIP(1412, l => l.UseMqtt());

                                o.ListenAnyIP(7170); // Default HTTPS pipeline
                                o.ListenAnyIP(5043); // Default HTTP pipeline
                            });

                        webBuilder.UseStartup<Startup>()
                                  .UseUrls(urls: new String[] { "http://*:1412"});
                    });

            return host.RunConsoleAsync();
        }

        sealed class MqttController
        {
            //private readonly IMqttService service;
            public MqttController()
            {
                // Inject other services via constructor.
                //this.service = new IMqttService();
            }

            public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
            {
                Console.WriteLine($"Client '{eventArgs.ClientId}' connected.");
                return Task.CompletedTask;
            }


            public Task ValidateConnection(ValidatingConnectionEventArgs eventArgs)
            {
                Console.WriteLine($"Client '{eventArgs.ClientId}' wants to connect. Accepting!");
                return Task.CompletedTask;
            }

            public Task OnNewMessage(InterceptingPublishEventArgs eventArgs)
            {
                var payload = eventArgs.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(eventArgs.ApplicationMessage?.Payload);

                Console.WriteLine(
                    $"===================================================\n" +
                    $"TimeStamp: {DateTime.Now} -- \n" +
                    $"Message: ClientId = {eventArgs.ClientId}, \n" +
                    $"Topic = {eventArgs.ApplicationMessage?.Topic}, \n" +
                    $"Payload = {payload}, \n" +
                    $"QoS = {eventArgs.ApplicationMessage?.QualityOfServiceLevel}, \n" +
                    $"Retain-Flag = {eventArgs.ApplicationMessage?.Retain}"
                    );

                return Task.CompletedTask;

            }
        }

        sealed class Startup
        {
            public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, MqttController mqttController)
            {
                Console.WriteLine("Setting up :: Startup Configure ");

                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseEndpoints(
                    endpoints =>
                    {
                        //endpoints.MapControllers();
                        //Setup mqtt endpoints for websocket (localhost:{port}/mqtt}
                        //.NET CORE 3.1 Approach
                        endpoints.MapMqtt("mqtt/RFID");
                        //.NET 5 Approach

                        endpoints.MapConnectionHandler<MqttConnectionHandler>(
                            "mqtt/RFID",
                            httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
                                protocolList => protocolList.FirstOrDefault() ?? string.Empty);

                        
                    });

                app.UseMqttServer(
                    server =>
                    {
                        /*
                         * Attach event handlers etc. if required.
                         */
                        server.ValidatingConnectionAsync += mqttController.ValidateConnection;
                        server.ClientConnectedAsync += mqttController.OnClientConnected;

                        //server.InterceptingPublishAsync += args =>
                        //{
                        //    args.ApplicationMessage.Topic += "mqtt/RFID";
                        //    return Task.CompletedTask;
                        //};

                        server.InterceptingPublishAsync += mqttController.OnNewMessage;
                    });
            }

            public void ConfigureServices(IServiceCollection services)
            {
                Console.WriteLine("Setting up :: ConfigureServices ");
                services.AddCors();
                services.AddHostedMqttServer(
                    optionsBuilder =>
                    {
                        optionsBuilder.WithDefaultEndpoint(); // set endpoint to localhost
                        optionsBuilder.WithDefaultEndpointPort(1412); // port used will be 1312
                        optionsBuilder.WithDefaultEndpointBoundIPAddress(IPAddress.Parse("127.0.0.1"));

                    });

                services.AddConnections();
                services.AddMqttConnectionHandler();

                services.AddSingleton<MqttController>();

                //services.AddHostedMqttServerWithServices(options => {
                //    var s = options.ServiceProvider.GetRequiredService<MqttController>();
                //    s.ConfigureMqttServerOptions(options);
                //});
                //services.AddMqttConnectionHandler();
                //services.AddMqttWebSocketServerAdapter();
            }
        }
    }
}
