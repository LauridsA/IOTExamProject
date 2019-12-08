using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactFrontend.Models
{
    public class Tone
    {
        public int speakerIdToPlayOn;
        public int frequency;
        public int duration;

        public Tone(int speaker, int frequency, int dur)
        {
            this.speakerIdToPlayOn = speaker;
            this.frequency = frequency;
            this.duration = dur;
        }
    }
}
