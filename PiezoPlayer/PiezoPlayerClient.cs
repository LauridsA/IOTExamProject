﻿using MQTTClient;
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
        static bool playing = false;
        static bool paused = false;
        static long v = 5;
        Song s = null;
        static Dictionary<int, int> _speakerGPIOPortMap = new Dictionary<int, int>();
        private List<Message> messages { get; set; }

        public async Task SetupPlayer()
        {
            MQTTService client = new MQTTService();
            await client.SetupClient(12393, "broker.busk.cf", "piezo", "piezopass", "PiezoPlayer");
            await client.Subscribe("Song/#");
            await client.Subscribe("test/#"); //REMOVE FOR PROD
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
            s = new Song
                (
                    new List<Tone>()
                    { //speaker id, frequency in hz, duration, delay before playing
                        new Tone(1, 440, 500, 0),
                        new Tone(1, 440, 500, 20),
                        new Tone(1, 440, 500, 20),
                        new Tone(1, 349, 600, 20),
                        new Tone(1, 523, 150, 20),
                        new Tone(1, 440, 500, 20),
                        new Tone(1, 349, 350, 20),
                        new Tone(1, 523, 150, 20),
                        new Tone(1, 440, 650, 20)
                    }
                );
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

                    Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                    {
                        controller.Dispose();
                    };

                    while (true)
                        while (playing)
                        {
                            foreach (var tone in s.tones)
                            {
                                if (!playing)
                                {
                                    controller.Write(_speakerGPIOPortMap[tone.speakerIdToPlayOn], PinValue.Low);
                                    break;
                                }
                                while (paused)
                                    Thread.Sleep(1000);

                                Console.WriteLine($"Playing delaying tone for {tone.delayBeforePlaying} before playing tone for {tone.duration} ms on speaker with id {tone.speakerIdToPlayOn}");
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
                        foreach (var tone in s.tones)
                        {
                            if (!playing)
                            {
                                Console.Write($"{_speakerGPIOPortMap[tone.speakerIdToPlayOn]} low");
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
                }
                else if (payload == "prev")
                {
                    // previous track
                }
                else
                {
                    //Look up payload as song in API
                }

                // Get value.payload from backend (songname)
            }
            if (topic == "song")
            {
                if (payload == "play")
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

        private TimeSpan HzToTimespan(int hz)
        {
            return TimeSpan.FromMilliseconds((1/hz)*1000);
        }

        private void PlayTone(GpioController controller, Tone t)
        {
            Stopwatch faggot = new Stopwatch();
            TimeSpan ts = TimeSpan.FromMilliseconds(t.duration);

            faggot.Start();

            while (faggot.ElapsedMilliseconds < ts.Milliseconds)
            {
                controller.Write(17, PinValue.High);
                Thread.Sleep(HzToTimespan(t.frequency));
                controller.Write(17, PinValue.Low);
            }
            Thread.Sleep(TimeSpan.FromMilliseconds(t.duration / 0.3));
        }

        private void PlayToneLocal(Tone t)
        {
            Stopwatch faggot = new Stopwatch();
            TimeSpan ts = TimeSpan.FromMilliseconds(t.duration);

            faggot.Start();
            while (faggot.ElapsedMilliseconds < ts.Milliseconds)
            {
                Console.WriteLine("Beep");
                Thread.Sleep(HzToTimespan(t.frequency));
                Console.WriteLine("Beep");
            }
            Thread.Sleep(TimeSpan.FromMilliseconds(t.duration / 0.3));
        }

    }
}
