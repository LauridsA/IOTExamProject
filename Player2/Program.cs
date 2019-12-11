using NAudio.Wave;
using NLayer.NAudioSupport;
using System;

namespace Player2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var waveOut = new WaveOutEvent();
            var file = @"untitled.mp3";
            var builder = new Mp3FileReader.FrameDecompressorBuilder(wf => new Mp3FrameDecompressor(wf));

            var mp3Reader = new Mp3FileReader(file, builder);
            //BufferedWaveProvider buffer = new BufferedWaveProvider(WaveFormat.)
            waveOut.Init(mp3Reader);
            waveOut.Play();

            Console.Read();
        }
    }
}
