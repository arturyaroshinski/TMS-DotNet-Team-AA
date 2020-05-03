using CurrencyExchange.API;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class CurrencyExchangeController
    {
        private Currency[] _cache { get; set; }
        private DateTime LastUpdated { get; set; } = new DateTime();

        private HttpClient _httpClient = new HttpClient();

        private const string PATH = @"..\test.txt";

        // Возвращает массив объектов класса Currency.
        private async Task<Currency[]> GetAllCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync("https://www.nbrb.by/api/exrates/currencies");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Currency[]>(content);
        }

        // Обновляет кэш
        private void UpdateCache()
        {
            if (DateTime.Now - LastUpdated >= TimeSpan.FromDays(1))
            {
                _cache = GetAllCurrenciesAsync().GetAwaiter().GetResult();
            }
        }

        // Возвращает объект класса Rate по ID.
        private async Task<Rate> GetRateAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://www.nbrb.by/api/exrates/rates/ {id}");

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Rate>(content);
        }

        /// <summary>
        /// Вывод курса валюты на консоль по ID.
        /// </summary>
        public void ShowRate()
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
            var text = $"{rate.Date}: {rate.Cur_Abbreviation}. Курс по НБРБ - {rate.Cur_OfficialRate} за {rate.Cur_Scale} единиц валюты.";
            Console.WriteLine(text);

            Console.WriteLine("Нажмите Y, если хотите сохранить данные курса.");
            if (Console.ReadKey().Key.Equals(ConsoleKey.Y))
            {
                SaveAsync(PATH, text).GetAwaiter().GetResult();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("\nСохранение выполнено успешно.");
            }
        }
        /// <summary>
        /// Вывод на консоль всех валют.
        /// </summary>
        public void ShowAllCurrencies()
        {
            UpdateCache();
            foreach (var cur in _cache)
            {
                Console.WriteLine($"{cur.Cur_Name} : ID = {cur.Cur_ID}.");
            }
        }

        // Возвращает true, если валюта с таким id существует.
        private bool IdIsExist(int id)
        {
            UpdateCache();
            return _cache.Any(x => x.Cur_ID == id);
        }

        /// <summary>
        /// Сохранение строки по заданному пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <param name="text">Текст.</param>
        /// <returns></returns>
        public static async Task SaveAsync(string path, string text)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            text = text ?? throw new ArgumentNullException(nameof(text));

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                await sw.WriteLineAsync(text);
            }
        }

        public void ShowSavedData()
        {
            StreamReader sr = new StreamReader(PATH, Encoding.UTF8);
            Console.WriteLine(sr.ReadToEnd());
            
        }
    }
}
