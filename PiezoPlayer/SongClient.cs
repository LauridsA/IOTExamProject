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
            client = new RestClient(basepath);
        }

        public Song GetSongByTitle(string songTitle)
        {
            var request = new RestRequest($"/api/piezo/GetSongByTitle /{songTitle}");
            return client.Execute<Song>(request).Data;
        }

        public Song GetNextSong(int currentId)
        {
            var request = new RestRequest($"/api/piezo/GetNextSong/{currentId}");
            return client.Execute<Song>(request).Data;
        }


        public Song GetPreviousSong(int currentId)
        {
            var request = new RestRequest($"/api/piezo/GetPrevSong/{currentId}");
            return client.Execute<Song>(request).Data;
        }
    }
}
