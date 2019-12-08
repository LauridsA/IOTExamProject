using PiezoPlayer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiezoPlayer
{
    public class SongClient
    {
        RestClient client;

        public SongClient(string basepath)
        {
            client = new RestClient(basepath+"/api/piezo/");
        }

        public Song GetSongByTitle(string songTitle)
        {
            var request = new RestRequest($"GetSongByTitle/{songTitle}");
            return client.Execute<Song>(request).Data;
        }

        public Song GetNextSong(int currentId)
        {
            var request = new RestRequest($"GetNextSong/{currentId}");
            return client.Execute<Song>(request).Data;
        }


        public Song GetPreviousSong(int currentId)
        {
            var request = new RestRequest($"GetPrevSong/{currentId}");
            return client.Execute<Song>(request).Data;
        }
    }
}
