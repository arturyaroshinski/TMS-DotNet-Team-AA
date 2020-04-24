using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CurrencyExchange.API;
using Newtonsoft.Json;

namespace CurrencyExchangeApp
{
    public static class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private const string PATH = @"..\test.txt";

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
                        {
                            ShowAllCurrencies();
                        }
                        break;
                    case "2":
                        {
                            ShowRate().GetAwaiter().GetResult();
                            break;
                        }
                    case "3":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Некорректный ввод.");
                            break;
                        }
                }
            }
        }

        // Возвращает массив объектов класса Currency.
        private static async Task<Currency[]> GetAllCurrenciesAsync()
        {
            var response = await httpClient.GetAsync("https://www.nbrb.by/api/exrates/currencies");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Currency[]>(content);
        }

        // Вывод курса валюты на консоль по ID.
        public static async Task ShowRate()
        {
            int id = 298;
            bool flag = false;

            while (!flag)
            {
                Console.WriteLine();
                Console.WriteLine("Для получения курса введите ID валюты(Для просмотра списка валют введите 0).");

                var userInput = Console.ReadLine();

                if (int.TryParse(userInput, out id))
                {
                    if (id == 0)
                        ShowAllCurrencies();
                    else
                    {
                        if (IdIsExist(id))
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Валюты с ID {id} не существует.");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
            }

            Rate rate = GetRateAsync(id).GetAwaiter().GetResult();

            var text = $"{rate.Date}: {rate.Cur_Abbreviation}. Курс по НБРБ - {rate.Cur_OfficialRate}.";
            Console.WriteLine(text);

            Console.WriteLine("Нажмите Y, если хотите сохранить данные курса.");
            if (Console.ReadKey().Key.Equals(ConsoleKey.Y))
            {
                await SaveAsync(PATH, text);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nСохранение выполнено успешно.");
            }
        }

        // Возвращает объект класса Rate по ID.
        private static async Task<Rate> GetRateAsync(int id)
        {
            var response = await httpClient.GetAsync($"https://www.nbrb.by/api/exrates/rates/ {id}");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Rate>(content);
        }

        /// <summary>
        /// Вывод на консоль всех валют.
        /// </summary>
        public static void ShowAllCurrencies()
        {
            Currency[] currencies = GetAllCurrenciesAsync().GetAwaiter().GetResult();

            foreach (var cur in currencies)
            {
                Console.WriteLine($"{cur.Cur_Name} : ID = {cur.Cur_ID}.");
            }
        }

        // Возвращает true, если валюта с таким id существует.
        private static bool IdIsExist(int id)
        {
            var currencies = GetAllCurrenciesAsync().GetAwaiter().GetResult();
            return currencies.Any(x => x.Cur_ID == id);
        }

        // Сохранение строки text по пути path.
        public static async Task SaveAsync(string path, string text)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException();
            }

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                await sw.WriteLineAsync(text);
            }
        }
    }
}
