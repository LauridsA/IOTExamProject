using System;
using System.Device.Gpio;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using Iot.Device.CpuTemperature;
using ReactFrontend.MQTTClient;

namespace PiezoPlayer
{

    class Program : IObserver<MqttApplicationMessage>
    {
        static GpioController GPIOController = new GpioController();
        static bool playing = false;
        static bool paused = false;
        static int speaker1High = 17;
        static int speaker2Middle = 18;
        static int speaker3Low = 19;

        static CpuTemperature temperature = new CpuTemperature();

        static void Main(string[] args)
        {
            Program.Subscribe(RemoteMQTTClient.GetMessageStream());
            var pin = 17;
            var lightTimeInMilliseconds = 1000;
            var dimTimeInMilliseconds = 200;

            Console.WriteLine($"Let's blink an LED!");
            using (GpioController controller = new GpioController())
            {
                controller.OpenPin(pin, PinMode.Output);
                Console.WriteLine($"GPIO pin enabled for use: {pin}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
                {
                    controller.Dispose();
                };

                while (true)
                {
                    while (playing)
                    {
                        while (paused)
                            Thread.Sleep(1000);


                    }
                    Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.High);
                    Thread.Sleep(lightTimeInMilliseconds);
                    Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
                    controller.Write(pin, PinValue.Low);
                    Thread.Sleep(dimTimeInMilliseconds);

                    if (temperature.IsAvailable)
                    {
                        Console.WriteLine($"The CPU temperature is {temperature.Temperature.Celsius}");
                    }
                }
            }
        }
        public virtual void Subscribe(IObservable<MqttApplicationMessage> mqttApplicationMessage)
        {
            mqttApplicationMessage.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(MqttApplicationMessage value)
        {
            if (value.Topic == "song/track")
            {
                // Get value.payload from backend (songname)
            }
            if (value.Topic == "song/next")
            {
                // Get the next song
            }
            if (value.Topic == "song/previous")
            {
                // Get the previous song
            }
            if (value.Topic == "song/play")
            {
                if (Encoding.UTF8.GetString(value.Payload) == "start")
                {
                    playing = true;
                }
                else if (Encoding.UTF8.GetString(value.Payload) == "stop")
                {
                    playing = false;
                }
                else if (Encoding.UTF8.GetString(value.Payload) == "pause")
                {
                    playing = false;
                    paused = true;
                }
                else if (Encoding.UTF8.GetString(value.Payload) == "unpause")
                {
                    playing = true;
                    paused = false;
                }
            }
        }
    }
}
