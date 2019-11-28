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
        public Tone(Song song, Tone nextTone, Tone previousTone, Tone firstTone, Tone lastTone, int speakerIdToPlayOn, int delay)
        {
            this.song = song;
            this.nextTone = nextTone;
            this.previousTone = previousTone;
            this.firstTone = firstTone;
            this.lastTone = lastTone;
            this.speakerIdToPlayOn = speakerIdToPlayOn;
            this.delay = delay;
        }
    }
}
