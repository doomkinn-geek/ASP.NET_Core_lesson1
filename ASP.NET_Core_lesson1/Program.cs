using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ASP.NET_Core_lesson1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            ThePost post = null;
            string request = "https://jsonplaceholder.typicode.com/posts/";


            for (int i = 0; i < 11; i++)
            {
                using (var file = new TextWriter(FileAccess.ReadWrite))
                {
                    post = await Request(request + i.ToString(), httpClient);
                    file.WriteLine(post.ToString());
                }
            }            
        }
        private static async Task<ThePost> Request(string uri, HttpClient httpClient)
        {
            var httpResponse = await httpClient.GetAsync(uri);
            if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
            {
                var jsonString = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ThePost>(jsonString);
            }
            else
            {
                Console.WriteLine("ответ не может быть интерпретирован");
            }
            return null;
        }
    }
}
