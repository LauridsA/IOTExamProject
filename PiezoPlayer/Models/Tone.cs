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
        public int delayBeforePlaying;
        public Tone(int speakerId, int frequency, int dur, int delaybeforeplaying)
        {
            this.speakerIdToPlayOn = speakerId;
            this.frequency = frequency;
            this.duration = dur;
            this.delayBeforePlaying = delaybeforeplaying;
        }
    }
}
