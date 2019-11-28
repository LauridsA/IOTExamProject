using MQTTClient;
using PiezoPlayer.Models;
using ReactFrontend.MQTTClient;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiezoPlayer
{
    public class PiezoPlayerClient : IObserver<MqttApplicationMessage>
    {
        static bool playing = false;
        static bool paused = false;
        static Dictionary<int, int> _speakerGPIOPortMap = new Dictionary<int, int>();
        private List<Message> messages { get; set; }

        public async Task SetupPlayer()
        {
            MQTTService client = new MQTTService();
            await client.SetupClient(12393, "farmer.cloudmqtt.com", "msxwryld", "7z4Ms3G5-kfD", "PiezoPlayer");
            await client.Subscribe("Song/#");
            Subscribe(client.GetMessageStream());
            PopulateSpeakerGPIOPorts();
        }

        private void PopulateSpeakerGPIOPorts()
        {
            _speakerGPIOPortMap.Add(1, 17);
            _speakerGPIOPortMap.Add(2, 18);
            _speakerGPIOPortMap.Add(3, 19);
        }

        public async Task Play()
        {
            var s = new Song();
            // (Song song, Tone nextTone, Tone previousTone, Tone firstTone, Tone lastTone, int speakerIdToPlayOn, int delay)
            var tone1 = new Tone(s, null, null, null, null, 1, 1000);
            var tone2 = new Tone(s, null, null, null, null, 1, 2000);
            var tone3 = new Tone(s, null, null, null, null, 1, 1500);
            var tone4 = new Tone(s, null, null, null, null, 1, 500);

            tone1.nextTone = tone2;
            tone1.firstTone = tone1;
            tone1.lastTone = tone4;

            tone2.firstTone = tone1;
            tone2.previousTone = tone1;
            tone2.nextTone = tone3;
            tone2.lastTone = tone4;

            tone3.firstTone = tone1;
            tone3.previousTone = tone2;
            tone3.nextTone = tone4;
            tone3.lastTone = tone4;

            tone4.firstTone = tone1;
            tone4.previousTone = tone3;
            tone4.nextTone = null;
            tone4.lastTone = tone4;

            s.firstTone = tone1;
            using (GpioController controller = new GpioController())
            {
                var tone = s.firstTone;
                controller.OpenPin(_speakerGPIOPortMap[1], PinMode.Output);
                controller.OpenPin(_speakerGPIOPortMap[2], PinMode.Output);
                controller.OpenPin(_speakerGPIOPortMap[3], PinMode.Output);
                Console.WriteLine($"GPIO pins enabled for use: {_speakerGPIOPortMap[1]}, {_speakerGPIOPortMap[2]}, {_speakerGPIOPortMap[3]}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    while (playing)
                    {
                        while (tone != null)
                        {
                            while (paused)
                                Thread.Sleep(1000);

                            Console.WriteLine($"Playing tone for {tone.delay} for ms on speaker with id {tone.speakerIdToPlayOn}");
                            controller.Write(_speakerGPIOPortMap[tone.speakerIdToPlayOn], PinValue.High);
                            Thread.Sleep(tone.delay);
                            controller.Write(_speakerGPIOPortMap[tone.speakerIdToPlayOn], PinValue.Low);
                            var dbl = tone.delay * 0.3;
                            Thread.Sleep(Convert.ToInt32(dbl));
                            tone = tone.nextTone;
                            if (tone == null)
                                tone = s.firstTone;
                        }
                    }
                }
            }
        }

        public async Task PlayLocal()
        {
            var s = new Song();
            // (Song song, Tone nextTone, Tone previousTone, Tone firstTone, Tone lastTone, int speakerIdToPlayOn, int delay)
            var tone1 = new Tone(s, null, null, null, null, 1, 1000);
            var tone2 = new Tone(s, null, null, null, null, 1, 4000);
            var tone3 = new Tone(s, null, null, null, null, 1, 3000);
            var tone4 = new Tone(s, null, null, null, null, 1, 2000);

            tone1.nextTone = tone2;
            tone1.firstTone = tone1;
            tone1.lastTone = tone4;

            tone2.firstTone = tone1;
            tone2.previousTone = tone1;
            tone2.nextTone = tone3;
            tone2.lastTone = tone4;

            tone3.firstTone = tone1;
            tone3.previousTone = tone2;
            tone3.nextTone = tone4;
            tone3.lastTone = tone4;

            tone4.firstTone = tone1;
            tone4.previousTone = tone3;
            tone4.nextTone = null;
            tone4.lastTone = tone4;

            s.firstTone = tone1;

            var tone = s.firstTone;

            Console.WriteLine($"GPIO pins enabled for use: {_speakerGPIOPortMap[1]}, {_speakerGPIOPortMap[2]}, {_speakerGPIOPortMap[3]}");

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
            {

            };

            while (true)
            {
                while (tone != null)
                {
                    while (paused)
                        Thread.Sleep(1000);

                    Console.WriteLine($"Playing tone for {tone.delay} for ms on speaker with id {tone.speakerIdToPlayOn}");
                    Thread.Sleep(tone.delay);
                    tone = tone.nextTone;
                    if (tone == null)
                        tone = s.firstTone;
                }
            }

        }


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
                    paused = true;
                }
                else if (payload == "unpause")
                {
                    paused = false;
                }
            }
        }
    }
}
