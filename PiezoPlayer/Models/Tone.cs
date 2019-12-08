using System;
using System.Collections.Generic;
using System.Text;

namespace PiezoPlayer.Models
{
    public class Tone
    {
        public int frequency;
        public int duration;
        public Tone(int frequency, int dur)
        {
            this.frequency = frequency;
            this.duration = dur;
        }
    }
}
