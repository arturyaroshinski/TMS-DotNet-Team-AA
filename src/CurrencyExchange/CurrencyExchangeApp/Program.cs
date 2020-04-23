using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.API;
using Newtonsoft.Json;

namespace CurrencyExchangeApp
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();


        static void Main(string[] args)
        {
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("[1] Список валют.\n[2] Узнать курс.\n[3] Выход.");
                Console.WriteLine("Выберите действие, путем ввода его номера.");

                switch (Console.ReadLine())
                {
                    case "1":
                        GetAllCurrenciesAsync().GetAwaiter().GetResult();
                        break;
                    case "2":
                        GetRateAsync().GetAwaiter().GetResult();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }
            }
        }

        // Вывод всех курсов на консоль.
        private static async Task GetAllCurrenciesAsync()
        {
            var response = await httpClient.GetAsync("https://www.nbrb.by/api/exrates/currencies");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            List<Currency> currencies = JsonConvert.DeserializeObject<List<Currency>>(content);
            foreach (var cur in currencies)
            {
                Console.WriteLine($"{cur.Cur_Name} : ID = {cur.Cur_ID}.");
            }
        }

        // Вывод курса валюты по ID
        private static async Task GetRateAsync()
        {
            // TODO: добавить проверку на существование такого id.
            int id = 298;
            bool flag = false;

            while (!flag)
            {
                Console.WriteLine();
                Console.WriteLine("Для получения курса введите ID валюты (Для просмотра списка валют введите 0).");

                var userInput = Console.ReadLine();

                if (userInput.Equals("0"))
                    await GetAllCurrenciesAsync();
                else
                {
                    id = int.Parse(userInput);
                    flag = true;
                }
            }

            var response = await httpClient.GetAsync($"https://www.nbrb.by/api/exrates/rates/ {id}");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            Rate rate = JsonConvert.DeserializeObject<Rate>(content);

            Console.WriteLine($"{rate.Date}: {rate.Cur_Abbreviation}. Курс по НБРБ - {rate.Cur_OfficialRate}.");
        }
    }
}
