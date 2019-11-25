using MQTTClient;
using ReactFrontend.MQTTClient;
using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace PiezoPlayer
{
    public class PiezoPlayerClient : IObserver<MqttApplicationMessage>
    {
        static bool playing = false;
        static bool paused = false;
        public async void SetupPlayer()
        {
            MQTTService client = new MQTTService();
            await client.SetupClient(12393, "farmer.cloudmqtt.com", "msxwryld", "7z4Ms3G5-kfD", "PiezoPlayer");
            await client.Subscribe("Song/#");
            Subscribe(client.GetMessageStream());
        }

        public async Task Play()
        {
            while (true)
            {

            }
        }
        private List<Message> messages { get; set; }
        public virtual void Subscribe(IObservable<MqttApplicationMessage> mqttApplicationMessage)
        {
            mqttApplicationMessage.Subscribe(this);
        }

        public void OnCompleted()
        {
            Console.WriteLine("I'm done mom, bathroom!!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Exception Occured. Exception Type: {error.GetType().ToString()} \n Exception message: {error.Message.ToString()}");
        }

        public void OnNext(MqttApplicationMessage value)
        {
            var topic = value.Topic.ToLower();
            var payload = Encoding.UTF8.GetString(value.Payload).ToLower();
            Console.WriteLine($"Topic: {value.Topic}. {Environment.NewLine}Message: {payload}");
            if (topic == "song/track")
            {
                // Get value.payload from backend (songname)
            }
            if (topic == "song/next")
            {
                // Get the next song
            }
            if (topic == "song/previous")
            {
                // Get the previous song
            }
            if (topic == "song/play")
            {
                if (payload == "start")
                {
                    playing = true;
                }
                else if (payload == "stop")
                {
                    playing = false;
                }
                else if (payload == "pause")
                {
                    playing = false;
                    paused = true;
                }
                else if (payload == "unpause")
                {
                    playing = true;
                    paused = false;
                }
            }
        }
    }
}
