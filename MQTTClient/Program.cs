using System;
using System.Threading;
using System.Net.Mqtt;
using System.Threading.Tasks;
using System.Text;

namespace MQTTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            setup_template().Wait();
        }

        public static async Task setup_template()
        {
            MqttClientLocal localClient = new MqttClientLocal();
            var configuration = new MqttConfiguration()
            {
                Port = 12393,
                KeepAliveSecs = 0,
                WaitTimeoutSecs = 5,
                BufferSize = 128 * 1024,
                AllowWildcardsInTopicFilters = true,
                MaximumQualityOfService = MqttQualityOfService.AtLeastOnce
            };
            string topicSubscribe = "my/topic";
            string topicPost = "my/other/topic";
            var remoteClient = await MqttClient.CreateAsync("farmer.cloudmqtt.com", configuration);
            await remoteClient.ConnectAsync(new MqttClientCredentials(clientId: "MyCoolNewClient", "msxwryld", "7z4Ms3G5-kfD"));
            await remoteClient.SubscribeAsync(topicSubscribe, MqttQualityOfService.AtLeastOnce);
            localClient.Subscribe(remoteClient.MessageStream);
            string payloadToSend = "MY MESSAGE TO BE SENT";
            var mymessage = new MqttApplicationMessage(topicPost, Encoding.UTF8.GetBytes(payloadToSend));
            remoteClient.PublishAsync(mymessage, MqttQualityOfService.AtLeastOnce).Wait();
        }
    }
}
