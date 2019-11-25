using System;
using System.Collections.Generic;
using System.Text;

namespace PiezoPlayer.Models
{
    public class Tone
    {
        public Song song;
        public Tone nextTone;
        public Tone previousTone;
        public Tone firstTone;
        public Tone lastTone;
        public int speakerIdToPlayOn;
        public int delay;
    }
}
