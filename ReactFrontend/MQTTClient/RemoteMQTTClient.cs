using MQTTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mqtt;
using System.Net.Mqtt.Sdk.Bindings;
using System.Text;
using System.Threading.Tasks;

namespace ReactFrontend.MQTTClient
{
    public static class RemoteMQTTClient
    {
        private static IMqttClient remoteClient;
        private static MqttClientLocal localClient;

        public async static void SetupClient()
        {
            localClient = new MqttClientLocal();
            var configuration = new MqttConfiguration()
            {
                Port = 12393,
                KeepAliveSecs = 60,
                WaitTimeoutSecs = 5,
                BufferSize = 128 * 1024,
                AllowWildcardsInTopicFilters = true,
                MaximumQualityOfService = MqttQualityOfService.AtLeastOnce
            };
            string topicSubscribe = "song/#";
            string topicSubscribe2 = "status";
            remoteClient = await MqttClient.CreateAsync("localhost:12393", configuration);
            await remoteClient.ConnectAsync(new MqttClientCredentials(clientId: "Frontend", "front", "frontpass"));
            await remoteClient.SubscribeAsync(topicSubscribe, MqttQualityOfService.AtLeastOnce); //subscribe on cloud mqtt
            await remoteClient.SubscribeAsync(topicSubscribe2, MqttQualityOfService.AtLeastOnce); //subscribe on cloud mqtt
            localClient.Subscribe(remoteClient.MessageStream); // subscribe to the iobservable (the response message stream)
        }

        public static IMqttClient getClient ()
        {
            return remoteClient;
        }

        public static void postMessage(string topic, string message)
        {
            #region example
            //topic = "my/cool/topic";
            //message = "MY MESSAGE TO BE SENT";
            #endregion
            try
            {
                var mymessage = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(message));
                remoteClient.PublishAsync(mymessage, MqttQualityOfService.AtLeastOnce).Wait();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static List<Message> getMessages()
        {
            return localClient.getMessages();
        }

    }
}
