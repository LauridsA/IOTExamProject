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
                    id = 1,
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration(ms)
                        new Tone(3, 500),
                        new Tone(3, 500),
                        new Tone(3, 500),
                        new Tone(3, 350),
                        new Tone(3, 150),
                        new Tone(3, 500),
                        new Tone(3, 350),
                        new Tone(3, 150),
                        new Tone(3, 650),
                        //small wait, no tone
                        new Tone(0, 150),
                        //next block
                        new Tone(3, 500),
                        new Tone(3, 500),
                        new Tone(3, 500),
                        new Tone(3, 350),
                        new Tone(3, 150),
                        new Tone(3, 350),
                        //next small wait, no tone
                        new Tone(0, 250),
                        //next block
                        new Tone(3, 500),
                        new Tone(3, 300),
                        new Tone(3, 150),
                        new Tone(3, 400),
                        new Tone(3, 200),
                        new Tone(3, 200),
                        new Tone(3, 125),
                        new Tone(3, 125),
                        new Tone(3, 250),
                        //next small wait, no tone
                        new Tone(0, 250),
                        //next block
                        new Tone(3, 250),
                        new Tone(3, 400),
                        new Tone(3, 200),
                        new Tone(3, 200),
                        new Tone(3, 125),
                        new Tone(3, 125),
                        new Tone(3, 250),
                        //next small wait, no tone
                        new Tone(0, 250),
                        //last block
                        new Tone(3, 125),
                        new Tone(3, 500),
                        new Tone(3, 375),
                        new Tone(3, 125),
                        new Tone(3, 500),
                        new Tone(3, 375),
                        new Tone(3, 125),
                        new Tone(3, 650)
                    }
                }
            },
            {
                2, new Song()
                {
                    title = "My Other Song",
                    id = 2,
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration, delaybeforeplaying
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1)
                    }
                }
            },
            {
                3, new Song()
                {
                    title = "Go Suck A D",
                    id = 3,
                    nodes = new List<Tone>()
                    {
                        //speaker, frequency, duration, delaybeforeplaying
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1),
                        new Tone(3, 1)
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
