using MQTTClient;
using PiezoPlayer.Models;
using ReactFrontend.MQTTClient;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiezoPlayer
{
    public class PiezoPlayerClient : IObserver<MqttApplicationMessage>
    {
        MQTTService client;
        SongClient sClient;
        static bool playing = false;
        static bool paused = false;
        static long v = 5;
        Song s = null;
        static Dictionary<int, int> _speakerGPIOPortMap = new Dictionary<int, int>();
        private List<Message> messages { get; set; }

        public async Task SetupPlayer()
        {
            client = new MQTTService();
            sClient = new SongClient("https://iot2.busk.cf");
            await client.SetupClient(12393, "broker.busk.cf", "piezo", "piezopass", "PiezoPlayer");
            await client.Subscribe("Song/#");
            await client.Subscribe("Track/#");
            await client.Subscribe("test/#"); //REMOVE FOR PROD
            Subscribe(client.GetMessageStream());
            PopulateSpeakerGPIOPorts();
            s = sClient.GetNextSong(100);
        }

        private void PopulateSpeakerGPIOPorts()
        {
            _speakerGPIOPortMap.Add(1, 17);
            _speakerGPIOPortMap.Add(2, 18);
            _speakerGPIOPortMap.Add(3, 19);
        }

        public async Task Play()
        {
            // (Song song, Tone nextTone, Tone previousTone, Tone firstTone, Tone lastTone, int speakerIdToPlayOn, int delay)

            bool gpio = true;

            if (gpio)
            {
                using (GpioController controller = new GpioController())
                {
                    controller.OpenPin(_speakerGPIOPortMap[1], PinMode.Output);
                    controller.OpenPin(_speakerGPIOPortMap[2], PinMode.Output);
                    controller.OpenPin(_speakerGPIOPortMap[3], PinMode.Output);
                    Console.WriteLine($"GPIO pins enabled for use: {_speakerGPIOPortMap[1]}, {_speakerGPIOPortMap[2]}, {_speakerGPIOPortMap[3]}");
                    controller.Write(_speakerGPIOPortMap[1], PinValue.Low);
                    controller.Write(_speakerGPIOPortMap[2], PinValue.Low);
                    controller.Write(_speakerGPIOPortMap[3], PinValue.Low);

                    Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                    {
                        controller.Dispose();
                    };

                    while (true)
                        while (playing)
                        {
                            if (s == null)
                                break;

                            foreach (var tone in s.nodes)
                            {
                                if (!playing)
                                {
                                    controller.Write(17, PinValue.Low);
                                    break;
                                }
                                while (paused)
                                    Thread.Sleep(1000);

                                PlayTone(controller, tone);
                            }
                        }
                }
            }
            else
            {
                Console.WriteLine($"GPIO pins enabled for use: {_speakerGPIOPortMap[1]}, {_speakerGPIOPortMap[2]}, {_speakerGPIOPortMap[3]}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    Console.WriteLine("keypresscancelled");
                };
                while (true)
                    while (playing)
                    {
                        foreach (var tone in s.nodes)
                        {
                            if (!playing)
                            {
                                Console.Write($"17 low");
                                break;
                            }
                            while (paused)
                                Thread.Sleep(1000);

                            PlayToneLocal(tone);

                        }
                    }
            }
        }

        //public async Task PlayLocal()
        //{
        //    var s = new Song();
        //    // (Song song, Tone nextTone, Tone previousTone, Tone firstTone, Tone lastTone, int speakerIdToPlayOn, int delay)
        //    var tone1 = new Tone(s, null, null, null, null, 1, 1000);
        //    var tone2 = new Tone(s, null, null, null, null, 1, 4000);
        //    var tone3 = new Tone(s, null, null, null, null, 1, 3000);
        //    var tone4 = new Tone(s, null, null, null, null, 1, 2000);

        //    tone1.nextTone = tone2;
        //    tone1.firstTone = tone1;
        //    tone1.lastTone = tone4;

        //    tone2.firstTone = tone1;
        //    tone2.previousTone = tone1;
        //    tone2.nextTone = tone3;
        //    tone2.lastTone = tone4;

        //    tone3.firstTone = tone1;
        //    tone3.previousTone = tone2;
        //    tone3.nextTone = tone4;
        //    tone3.lastTone = tone4;

        //    tone4.firstTone = tone1;
        //    tone4.previousTone = tone3;
        //    tone4.nextTone = null;
        //    tone4.lastTone = tone4;

        //    s.firstTone = tone1;

        //    var tone = s.firstTone;

        //    Console.WriteLine($"GPIO pins enabled for use: {_speakerGPIOPortMap[1]}, {_speakerGPIOPortMap[2]}, {_speakerGPIOPortMap[3]}");

        //    Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
        //    {

        //    };

        //    while (true)
        //    {
        //        while (tone != null)
        //        {
        //            while (paused)
        //                Thread.Sleep(1000);

        //            Console.WriteLine($"Playing tone for {tone.delay} for ms on speaker with id {tone.speakerIdToPlayOn}");
        //            Thread.Sleep(tone.delay);
        //            tone = tone.nextTone;
        //            if (tone == null)
        //                tone = s.firstTone;
        //        }
        //    }

        //}


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
            if (topic == "track")
            {
                if (payload == "next")
                {
                    // next track
                    if (s == null)
                        s = sClient.GetNextSong(1);
                    else
                    {
                        playing = false;
                        Thread.Sleep(1000);
                        s = sClient.GetNextSong(s.id);
                    }
                }
                else if (payload == "prev")
                {
                    // previous track
                    if (s == null)
                        s = sClient.GetPreviousSong(1);
                    else
                    {
                        playing = false;
                        Thread.Sleep(1000);
                        s = sClient.GetPreviousSong(s.id);
                    }
                }
                else
                {
                    playing = false;
                    Thread.Sleep(1000);
                    s = sClient.GetSongByTitle(payload);
                }

                // Get value.payload from backend (songname)
            }
            if (topic == "song")
            {
                if (payload == "play")
                {
                    playing = true;
                    client.Publish("status", "Playing");
                }
                else if (payload == "stop")
                {
                    playing = false;
                    client.Publish("status", "Stopped");
                }
                else if (payload == "pause")
                {
                    paused = true;
                    client.Publish("status", "Paused");

                }
                else if (payload == "unpause")
                {
                    paused = false;
                    client.Publish("status", "Unpaused");
                }
            }
            if (topic == "test/flicker")
            {
                if (payload == "start")
                {
                    Task.Run(() => Flicker());
                }
                else
                {
                    if (long.TryParse(payload, out v))
                        Console.WriteLine($"Flickering with a delay of {payload} hz - timespan {v} between on and off.");
                    else
                        Console.WriteLine($"Indvalid parse");

                }
            }
        }

        private async Task Flicker()
        {
            using (GpioController controller = new GpioController())
            {
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
                    controller.Write(17, PinValue.High);
                    Thread.Sleep(new TimeSpan(v));
                    controller.Write(17, PinValue.Low);
                    Thread.Sleep(new TimeSpan(v));
                }
            }
        }

        private void PlayTone(GpioController controller, Tone t)
        {
            Stopwatch youarebeingwatched = new Stopwatch();
            var sleeptimeafter = TimeSpan.FromMilliseconds(t.duration * 0.3);

            youarebeingwatched.Start();
            Console.WriteLine($"Playing delaying tone forbefore playing tone for {t.duration} ms. Will sleep for {t.frequency} ms. after {sleeptimeafter.TotalMilliseconds} sleep ms");
            if (t.frequency > 0)
                while (youarebeingwatched.ElapsedMilliseconds < t.duration)
                {
                    controller.Write(17, PinValue.High);
                    Thread.Sleep(t.frequency);
                    controller.Write(17, PinValue.Low);
                }
            controller.Write(17, PinValue.Low);
            Thread.Sleep(sleeptimeafter);
        }

        private void PlayToneLocal(Tone t)
        {
            Stopwatch youarebeingwatched = new Stopwatch();
            var sleeptimeafter = TimeSpan.FromMilliseconds(t.duration * 0.3);

            youarebeingwatched.Start();
            Console.WriteLine($"Playing delaying tone forbefore playing tone for {t.duration} ms. Will sleep for {t.frequency} ms. after {sleeptimeafter.TotalMilliseconds} sleep ms");
            if (t.frequency > 0)
                while (youarebeingwatched.ElapsedMilliseconds < t.duration)
                {
                    Thread.Sleep(t.frequency);
                }
            Thread.Sleep(sleeptimeafter);
        }

    }
}
