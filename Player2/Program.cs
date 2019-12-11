using NAudio.Wave;
using System;

namespace Player2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var waveOut = new WaveOutEvent();
            var mp3Reader = new Mp3FileReader(@"untitled.mp3");
            //BufferedWaveProvider buffer = new BufferedWaveProvider(WaveFormat.)
            waveOut.Init(mp3Reader);
            waveOut.Play();

            Console.Read();
        }
    }
}
