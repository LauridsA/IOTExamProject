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
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 350),
                        new Tone(1, 5, 150),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 350),
                        new Tone(1, 5, 150),
                        new Tone(1, 5, 650),
                        //small wait, no tone
                        new Tone(1, 0, 150),
                        //next block
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 350),
                        new Tone(1, 5, 150),
                        new Tone(1, 5, 350),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //next block
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 300),
                        new Tone(1, 5, 150),
                        new Tone(1, 5, 400),
                        new Tone(1, 5, 200),
                        new Tone(1, 5, 200),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 250),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //next block
                        new Tone(1, 5, 250),
                        new Tone(1, 5, 400),
                        new Tone(1, 5, 200),
                        new Tone(1, 5, 200),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 250),
                        //next small wait, no tone
                        new Tone(1, 0, 250),
                        //last block
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 375),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 500),
                        new Tone(1, 5, 375),
                        new Tone(1, 5, 125),
                        new Tone(1, 5, 650)
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
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1)
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
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1),
                        new Tone(1, 5, 1)
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
