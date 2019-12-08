using ReactFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactFrontend.DataLayer
{
    public static class AlbumDB
    {
        public static Dictionary<int, Song> songs = new Dictionary<int, Song>()
        {
            {
                1, new Song()
                {
                    title = "The Imperial March",
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration(ms)
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 350),
                        new Tone(1, 440, 150),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 350),
                        new Tone(1, 440, 150),
                        new Tone(1, 440, 650),
                        //small wait, no tone
                        new Tone(1, 0, 150),
                        //next block
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 350),
                        new Tone(1, 440, 150),
                        new Tone(1, 440, 350),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //next block
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 300),
                        new Tone(1, 440, 150),
                        new Tone(1, 440, 400),
                        new Tone(1, 440, 200),
                        new Tone(1, 440, 200),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 250),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //next block
                        new Tone(1, 440, 250),
                        new Tone(1, 440, 400),
                        new Tone(1, 440, 200),
                        new Tone(1, 440, 200),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 250),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //last block
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 375),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 500),
                        new Tone(1, 440, 375),
                        new Tone(1, 440, 125),
                        new Tone(1, 440, 650)
                    }
                }
            },
            {
                2, new Song()
                {
                    title = "My Other Song",
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration, delaybeforeplaying
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1)
                    }
                }
            },
            {
                3, new Song()
                {
                    title = "Go Suck A D",
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration, delaybeforeplaying
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1),
                        new Tone(1, 440, 1)
                    }
                }
            },
        };

        public static Dictionary<int, Song> GetSongs()
        {
            return songs;
        }

    }
}
