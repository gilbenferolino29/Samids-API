using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

using Samids_API.Services;
using Samids_API.Services.Interfaces;

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
                                // This will allow MQTT connections based on TCP port 1883.
                                o.ListenAnyIP(1883, l => l.UseMqtt());

                                // This will allow MQTT connections based on HTTP WebSockets with URI "localhost:5000/mqtt"
                                // See code below for URI configuration.
                                o.ListenAnyIP(3000); // Default HTTP pipeline
                            });

                        webBuilder.UseStartup<Startup>();
                    });

            return host.RunConsoleAsync();
        }

        sealed class MqttController
        {
            public MqttController()
            {
                // Inject other services via constructor.
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
        }

        sealed class Startup
        {
            public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, MqttController mqttController)
            {
                app.UseRouting();

                app.UseEndpoints(
                    endpoints =>
                    {
                        //endpoints.MapControllers();
                        //Setup mqtt endpoints for websocket (localhost:{port}/mqtt}
                        //.NET CORE 3.1 Approach
                        //endpoints.MapMqtt("/mqtt");
                        //.NET 5 Approach

                        endpoints.MapConnectionHandler<MqttConnectionHandler>(
                            "/mqtt",
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
                    });
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddCors();
                services.AddHostedMqttServer(
                    optionsBuilder =>
                    {
                        optionsBuilder.WithDefaultEndpoint(); // set endpoint to localhost
                        optionsBuilder.WithDefaultEndpointPort(1412); // port used will be 1312

                    });

                services.AddMqttConnectionHandler();
                services.AddConnections();

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
