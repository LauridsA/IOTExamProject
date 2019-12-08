using Newtonsoft.Json;
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
            var request = new RestRequest($"/api/piezo/GetSongByTitle/{songTitle}");
            var res = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Song>(res.Content);

            if (content != null)
                Console.WriteLine("SONGAPI: gotten by title " + content.title);
                        else
                Console.WriteLine("SONGAPI: SONG WAS NULL");
            return content;
        }

        public Song GetNextSong(int currentId)
        {
            var request = new RestRequest($"/api/piezo/GetNextSong/{currentId}");
            var res = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Song>(res.Content);

            if (content != null)
                Console.WriteLine("SONGAPI: gotten next " + content.title);
            else
                Console.WriteLine("SONGAPI: SONG WAS NULL");
            return content;
        }


        public Song GetPreviousSong(int currentId)
        {
            var request = new RestRequest($"/api/piezo/GetPrevSong/{currentId}");
            var res = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Song>(res.Content);

            if (content != null)
                Console.WriteLine("SONGAPI: gotten previous " + content.title);
                        else
                Console.WriteLine("SONGAPI: SONG WAS NULL");
            return content;
        }
    }
}
