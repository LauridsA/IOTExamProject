using System;
using System.Collections.Generic;
using System.Text;

namespace PiezoPlayer.Models
{
    public class Tone
    {
        public int speakerIdToPlayOn;
        public int frequency;
        public int duration;
        public Tone(int speakerId, int frequency, int dur)
        {
            this.speakerIdToPlayOn = speakerId;
            this.frequency = frequency;
            this.duration = dur;
        }
    }
}
