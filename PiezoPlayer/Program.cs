using System;
using System.Device.Gpio;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using MQTTClient;

namespace PiezoPlayer
{

    class Program
    {
        static GpioController GPIOController = new GpioController();
        static bool playing = false;
        static bool paused = false;
        static int speaker1High = 17;
        static int speaker2Middle = 18;
        static int speaker3Low = 19;

        public static void Main(string[] args)
        {
            PiezoPlayerClient pp = new PiezoPlayerClient();
            pp.SetupPlayer().Wait();
            pp.Play().Wait();
            while (true) ;
        }
    }
}

//using (GpioController controller = new GpioController())
//            {
//                controller.OpenPin(pin, PinMode.Output);
//                Console.WriteLine($"GPIO pin enabled for use: {pin}");

//                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs eventArgs) =>
//                {
//                    controller.Dispose();
//                };

//                while (true)
//                {
//                    while (playing)
//                    {
//                        while (paused)
//                            Thread.Sleep(1000);


//                    }
//                    Console.WriteLine($"Light for {lightTimeInMilliseconds}ms");
//                    controller.Write(pin, PinValue.High);
//                    Thread.Sleep(lightTimeInMilliseconds);
//                    Console.WriteLine($"Dim for {dimTimeInMilliseconds}ms");
//                    controller.Write(pin, PinValue.Low);
//                    Thread.Sleep(dimTimeInMilliseconds);

//                    if (temperature.IsAvailable)
//                    {
//                        Console.WriteLine($"The CPU temperature is {temperature.Temperature.Celsius}");
//                    }
//                }
//            }