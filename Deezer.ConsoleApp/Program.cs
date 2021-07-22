using Deezer.Entities;
using Deezer.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Deezer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // INTRO
            //Déclaration des collections
            List<Artist> artists = new List<Artist>();
            List<Album> albums = new List<Album>();
            List<Track> tracks = new List<Track>();

            //Intro programme
            Console.WriteLine(" demo accès api");

            // ARTIST
            //Conception de l'URL
            string artistName = "Johnny";
            string url = "http://api.deezer.com/2.0/search/artist?q=";
            url += artistName;

            //Etape 1 : Connexion API
            var result = Client.GetDataFromHttpClient(url);

            //Etape 2 : Lecture du flux 
            dynamic dataArtists = JsonConvert.DeserializeObject<dynamic>(result);

            //Déclaration des ARTIST
            foreach (var item in dataArtists["data"])
            {
                //Console.WriteLine(item["id"]);
                //Console.WriteLine(item["name"]);

                //Création d'un nouvel artist
                Artist artist = new Artist();

                //Définition des variables de l'ARTIST
                artist.Id = item["id"];
                artist.Name = item["name"];

                //Ajout de l'ARTIST à la liste
                artists.Add(artist); // ajout de l'artist à la liste 
            }

            /*Affichage des artistes
            foreach (var item in artists)
            {
                Console.WriteLine(item.Name);
            }*/

            // étape 3 : affichage des résultats 
            //Console.WriteLine(result);

            // ALBUM
            //Récupère le premier artiste de la liste
            var id = artists[0].Id;

            //Recherche les albums des artites
            var queryAlbums = "https://api.deezer.com/2.0/artist/" + id + "/albums";

            //Etape 1 : Connexion API
            var albumResult = Client.GetDataFromHttpClient(queryAlbums);


            //Etape 2 : Lecture du flux 
            dynamic dataAlbums = JsonConvert.DeserializeObject<dynamic>(albumResult);

            foreach (var item in dataAlbums["data"])
            {
                //Création de l'album 
                Album album = new Album();
                album.Id = item["id"];    // définition des valeurs des propriètés d'un artist
                album.Title = item["title"];
                album.CoverBig = item["cover_big"];

                //Ajout dans la liste d'albums 
                albums.Add(album);

                //Ajout du lien avec l'artiste
                var artist = artists.First(a => a.Id == id);
               //album.Artist = artist;
                artist.Albums.Add(album);
            }

            foreach (var item in albums)
            {
                Console.WriteLine(item.Title);
            }

            // TRACK
            foreach (Album a in albums)
            {
                // Récupération des tracks de l'album a
                var queryTracks = "https://api.deezer.com/album/" + a.Id + "/tracks";
                var trackResult = Client.GetDataFromHttpClient(queryTracks);
                dynamic dataTracks = JsonConvert.DeserializeObject<dynamic>(trackResult);

                // Traduction des données des tracks en classe
                foreach (var item in dataTracks["data"])
                {
                    // Création du track
                    Track track = new Track();
                    track.Id = item["id"];
                    track.Title = item["title"];
                    track.Preview = item["preview"];

                    // Ajout dans la liste de tracks
                    tracks.Add(track);

                    // Ajout du lien avec l'album
                    //track.Album = a;
                    a.Tracks.Add(track);
                }
            }

            // SERIALISATION
            string jsonName = "D:/JeanPhilippeSmet.json";
            string jsonContent = JsonConvert.SerializeObject(artists[0]);
            File.WriteAllText(@jsonName, jsonContent);
        }
    }
}
