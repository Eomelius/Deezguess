using System;
using System.Net.Http;

namespace Deezer.Tools
{
    public class Client
    {

        public static string GetDataFromHttpClient(string url)
        {
            // HttpClient classe 
            HttpClient httpClient = new HttpClient();

            // appel de la méthode GetAsync  
            var response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
