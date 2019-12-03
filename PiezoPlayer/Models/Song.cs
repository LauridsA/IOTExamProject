using System;
using System.Collections.Generic;
using System.Text;

namespace PiezoPlayer.Models
{
    public class Song
    {
        public List<Tone> tones;

        public Song(List<Tone> tunes)
        {
            tones = tunes;
        }
    }
}
