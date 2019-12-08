using ReactFrontend.DataLayer;
using ReactFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactFrontend.Services
{
    public class AlbumService
    {
        public AlbumService()
        {
            //empty lul
        }

        public Song getPrevSong(int key)
        {
            var songs = AlbumDB.GetSongs();
            if (key - 1 < 1)
            {
                return songs[songs.Count];
            }
            return songs[key-1];
        }

        public Song getSong(int key)
        {
            var songs = AlbumDB.GetSongs();
            if (key+1 > songs.Count || key-1 < 1)
            {
                return songs[1];
            }
            return songs[key];
        }

        public Song getNextSong(int key)
        {
            var songs = AlbumDB.GetSongs();
            if (key + 1 > songs.Count)
            {
                return songs[1];
            }
            return songs[key+1];
        }

        public Song getSongFromTitle(string title)
        {
            var songs = AlbumDB.GetSongs();
            var song = songs.Values.FirstOrDefault((list) => list.title == title);
            return song;
        }
    }
}
