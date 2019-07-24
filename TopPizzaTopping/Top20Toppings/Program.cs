using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Top20Toppings
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();

            var data = httpClient.GetAsync("http://files.olo.com/pizzas.json").Result.Content.ReadAsStringAsync().Result;

            var dataDeserialize = JsonConvert.DeserializeObject<List<ToppingDto>>(data);

            var toppingList = dataDeserialize.Select(x => string.Join(",", x.Toppings));

            Dictionary<string, int> toppings = new Dictionary<string, int>();

            foreach (var item in toppingList)
            {
                if (toppings.ContainsKey(item))
                {
                    toppings[item] += 1;
                }
                else
                {
                    toppings.Add(item, 1);
                }
            }

            var top20Topping = toppings.OrderByDescending(x => x.Value).Take(20);

            int rank = 1;

            Console.WriteLine("Rank" + "        " + "Count" + "         " + "Topping");

            foreach (var item in top20Topping)
            {
                Console.WriteLine("  " + rank++ + "         " + item.Value + "          " + item.Key);

                Console.WriteLine("===============================================");
            }

            Console.ReadLine();
        }
    }
}
