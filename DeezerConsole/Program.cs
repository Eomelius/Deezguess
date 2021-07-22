using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DeezerConsole
{
    class Program
    {

        public static string GetDataFromHttpClient(string url)
        {
            // HttpClient classe 
            HttpClient httpClient = new HttpClient();

            // appel de la méthode GetAsync  
            var response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }


        static void Main(string[] args)
        {
            Console.WriteLine(" demo accès api");

            string artistName = "Johnny";

            string url = "http://api.deezer.com/2.0/search/artist?q=";

            url += artistName;

            List<Artist> artists = new List<Artist>();

            List<Album> albums = new List<Album>();

            //  étape 1  :  connextion api 

            var result = GetDataFromHttpClient(url);


            // étape 2  : lecture du flux 

            dynamic dataArtists = JsonConvert.DeserializeObject<dynamic>(result);

            foreach (var item in dataArtists["data"])
            {
                //Console.WriteLine(item["id"]);
                //Console.WriteLine(item["name"]);
                Artist artist = new Artist(); // création d'un nouvel artist

                artist.Id = item["id"];    // définition des valeurs des propriètés d'un artist
                artist.Name = item["name"];

                artists.Add(artist); // ajout de l'artist à la liste 

            }

            // affichages des artistes

            foreach (var item in artists)
            {
                Console.WriteLine(item.Name);
            }
            // étape 3 : affichage des résultats 

            //Console.WriteLine(result);




            // album d'un artist 


            // comment est identifié un artiste?
            // ID 


            // premier artiste de la liste ?

            var id = artists[0].Id;

            var idLinq = artists.First().Id;

            // quelle query pour rechercher les albums d'un artiste?

            var queryAlbums = "https://api.deezer.com/2.0/artist/"+ id+ "/albums";



            //  étape 1  :  connextion api 

            var albumResult = GetDataFromHttpClient(queryAlbums);


            // étape 2  : lecture du flux 

            dynamic dataAlbums = JsonConvert.DeserializeObject<dynamic>(albumResult);

            foreach (var item in dataAlbums["data"])
            {
                // 1 = créer l'album 
                Album album = new Album();
                album.Id = item["id"];    // définition des valeurs des propriètés d'un artist
                album.Title = item["title"];
                album.CoverBig = item["cover_big"];
                // 2 = ajouter dans la liste d'albums 
                albums.Add(album);

                // 3 : liens! 
                // modifier la proprièté artist de l'album par la référence à l'artist 
                var artist = artists.First(a => a.Id == id);
                album.Artist = artist;

                //album.Artist = artists.Find(a => a.Id == id);
                // ajouter dans la liste des albums de l'artiste la référence de l'album 
                artist.Albums.Add(album);
            }



            foreach (var item in albums)
            {
                Console.WriteLine(item.Title);
            }




            Console.ReadKey(); 





        }
    }
}
