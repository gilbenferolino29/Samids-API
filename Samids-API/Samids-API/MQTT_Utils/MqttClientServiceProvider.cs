using Samids_API.Services.Interface;

namespace Samids_API.MQTT_Utils
{
    public class MqttClientServiceProvider
    {
        public readonly IMqttService MqttClientService;

        public MqttClientServiceProvider(IMqttService mqttClientService)
        {
            MqttClientService = mqttClientService;
        }
    }
}
