using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            // New Code:
            client.BaseAddress = new Uri("http://localhost:53087/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var users = await GetUsersAsync("user");

            foreach (var user in users)
            {
                Console.WriteLine(string.Format("{0} {1} {2} {3}", user.FirstName, user.LastName, user.EmailAddress, user.NumberOfSiblings));
            }

            Console.ReadLine();
        }

        static async Task<IEnumerable<User>> GetUsersAsync(string path)
        {
            IEnumerable<User> users = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<User>>();
            }
            return users;
        }

        class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string NumberOfSiblings { get; set; }
        }
    }
}