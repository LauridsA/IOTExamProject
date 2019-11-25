using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace MQTTClient
{
    public class MQTTService
    {
        private static IMqttClient client;

        public MQTTService()
        {
            
        }

        public async Task<IMqttClient> SetupClient(int port, string basepath, string username, string password, string clientname)
        {
            var configuration = new MqttConfiguration()
            {
                Port = port,
                KeepAliveSecs = 0,
                WaitTimeoutSecs = 5,
                BufferSize = 128 * 1024,
                AllowWildcardsInTopicFilters = true,
                MaximumQualityOfService = MqttQualityOfService.AtLeastOnce
            };
            var rpc = await MqttClient.CreateAsync(basepath, configuration);
            await rpc.ConnectAsync(new MqttClientCredentials(clientname, username, password));
            client = rpc;
            return rpc;
        }

        public IObservable<MqttApplicationMessage> GetMessageStream()
        {
            return client.MessageStream;
        }

        public async Task Subscribe(string topic)
        {
            await client.SubscribeAsync(topic.ToLower(), MqttQualityOfService.AtLeastOnce);
            await client.SubscribeAsync(topic, MqttQualityOfService.AtLeastOnce);
        }

        public async Task Publish(string topic, string message)
        {
            await client.PublishAsync(new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message)), MqttQualityOfService.AtLeastOnce);
        }
    }
}
