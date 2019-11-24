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

            whatever().Wait();
        }

        public static async Task whatever()
        {
            Client gg = new Client();
            var configuration = new MqttConfiguration()
            {
                Port = 12393,
                KeepAliveSecs = 0,
                WaitTimeoutSecs = 5,
                BufferSize = 128 * 1024,
                AllowWildcardsInTopicFilters = true
            };
            var client = await MqttClient.CreateAsync("farmer.cloudmqtt.com", configuration);
            var sessionState = await client.ConnectAsync(new MqttClientCredentials(clientId: "MyCoolNewClient", "msxwryld", "7z4Ms3G5-kfD"));
            await client.SubscribeAsync("foo/bar", MqttQualityOfService.AtLeastOnce);
            await client.SubscribeAsync("foo/bare", MqttQualityOfService.ExactlyOnce);

            Console.WriteLine("subbed to something");

            gg.Subscribe(client.MessageStream);


            for (int i = 1; i < 15; i++)
            {
            }

            while (true)
            {
                var input = Console.ReadLine();
                var mymessage = new MqttApplicationMessage("foo/bar", Encoding.UTF8.GetBytes(input));
                client.PublishAsync(mymessage, MqttQualityOfService.AtLeastOnce).Wait();

                if (input == "faggot")
                    return;
            }
            Console.Read();
        }
    }
}
