using CurrencyExchange.API;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class CurrencyExchangeController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        
        private Currency[] Cache { get; set; }

        private DateTime LastUpdated { get; set; } = new DateTime();




        /// <summary>
        /// Вывод курса валюты на консоль по ID.
        /// </summary>
        public void ShowRate()
        {
            // TODO: refactor it!!!


            // TODO: Delete magic number
            int id = 298;
            bool flag = false;


            // TODO: To another method 1
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



            // TODO: To another method 2
            Rate rate = GetRateAsync(id).GetAwaiter().GetResult();
            var text = $"{rate.Date:D}: {rate.Cur_Abbreviation}. Курс по НБРБ - {rate.Cur_OfficialRate} за {rate.Cur_Scale} единиц валюты.";
            Console.WriteLine(text);



            // TODO: To another method 3
            // TODO: Exception (User input)
            Console.WriteLine("Нажмите Y, если хотите сохранить данные курса.");
            if (Console.ReadKey().Key.Equals(ConsoleKey.Y))
            {
                var saveData = new SaveAndReadDataController();
                saveData.SaveDateAsync(Constants.PATH, text).GetAwaiter().GetResult();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("\nСохранение выполнено успешно.");
            }
        }





        /// <summary>
        /// Проверить существует ли Id.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Возвращает true, если валюта с таким id существует.</returns>
        public bool IdIsExist(int id)
        {
            UpdateCache();
            return Cache.Any(x => x.Cur_ID == id);
        }

        /// <summary>
        /// Вывод на консоль всех валют.
        /// </summary>
        public void ShowAllCurrencies()
        {
            UpdateCache();
            var emptyRow = new string('=', 52);

            // TODO: Generate header
            Console.WriteLine(emptyRow);
            foreach (var cur in Cache)
            {
                // TODO: https://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c

                Console.WriteLine("| {0,5} | {1,40} |", cur.Cur_ID, cur.Cur_Name);
                Console.WriteLine(emptyRow);
            }
        }

        // Обновляет кэш
        private void UpdateCache()
        {
            try
            {
                if (DateTime.Now - LastUpdated >= TimeSpan.FromDays(1))
                {
                    Cache = GetAllCurrenciesAsync().GetAwaiter().GetResult();
                    LastUpdated = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Constants.EXCEPTION_TEXT}{ex.Message}");
            }
        }

        // Возвращает массив объектов класса Currency.
        private async Task<Currency[]> GetAllCurrenciesAsync()
        {
            // TODO: Exception
            var response = await _httpClient.GetAsync(Constants.CURRENCIES);

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Currency[]>(content);
        }

        // Возвращает объект класса Rate по ID.
        private async Task<Rate> GetRateAsync(int id)
        {
            // TODO: Exception
            var request = $"{Constants.RATES}{id}";
            var response = await _httpClient.GetAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Rate>(content);
        }
    }
}
