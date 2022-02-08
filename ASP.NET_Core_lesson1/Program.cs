﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
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

            CancellationToken token = new CancellationToken();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            using (var file = new StreamWriter(File.Create("output.txt")))
            {
                for (int i = 0; i < 11; i++)
                {                
                    post = await Request(request + i.ToString(), httpClient, token);
                    await file.WriteLineAsync(post.ToString());                    
                }
            }            
            ConsoleKeyInfo input;
            while (true)
            {
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.Escape)
                {
                    cancelTokenSource.Cancel();
                    break;
                }
            }
            
        }
        private static async Task<ThePost> Request(string uri, HttpClient httpClient, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return null;
            }             
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
