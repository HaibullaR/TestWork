using Newtonsoft.Json;
using Server.Entity;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mysplitURL = new Uri("https://localhost:44384/Service/MySplit?text=Hello World");
            var createURL = new Uri("https://localhost:44384/Service/Create");
            var getPersonsURL = new Uri("https://localhost:44384/Service/Get");

            using (var client = new HttpClient())
            {
                for (int i = 0; i < 10; i++)
                {
                    var response = await client.GetAsync(mysplitURL);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"MySplit response: {responseBody}");
                    if (i == 4)
                        mysplitURL = new Uri("https://localhost:44384/Service/MySplit?tex = wad");
                }

                var person = new PersonCreateModel
                {
                    Iin = "123456789102",
                    Birthday = DateTime.Now,
                    FirstName = "Макар",
                    LastName = "Макаров",
                    MiddleName = "Ильясович"
                };
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    string iin = "";
                    var json = JsonConvert.SerializeObject(person);
                    var response = await client.PostAsync(createURL, new StringContent(json, Encoding.UTF8, "application/json"));
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Create person response: {responseBody}");

                    for (int j = 0; j < 12; j++)
                        iin += random.Next(1, 9).ToString();
                    person.Iin = iin;

                    if (i == 2)
                    {
                        person.Iin = "123456789102";
                    }
                    else if (i == 4)
                    {
                        person.FirstName = null;
                    }
                    else if (i == 7)
                    {
                        person.FirstName = "Андрей";
                    }
                }


                var res = await client.GetAsync(getPersonsURL);
                var resBody = await res.Content.ReadAsStringAsync();
                Console.WriteLine($"Get response: {resBody}");
            }
        }
    }
}
