using System;
using CurrencyExchange;

namespace CurrencyExchangeApp
{
    public static class Program
    {
        static void Main()
        {
            Start();
        }

        private static void Start()
        {
            var curController = new CurrencyExchangeController();
            var saveAndReadController = new SaveAndReadDataController();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Меню:");
                Console.WriteLine("[1] Список валют.\n[2] Узнать курс.\n[3] Вывести сохраненные запросы\n[4] Выход.");
                Console.Write("Укажите действие (цифра): ");
                var userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case "1":
                    {
                        curController.ShowAllCurrencies();
                    }
                    break;

                    case "2":
                    {
                        curController.ShowRate();
                    }
                    break;

                    case "3":
                    {
                        saveAndReadController.ReadData();
                    }
                    break;

                    case "4":
                    {
                        Environment.Exit(0);
                    }
                    break;

                    default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некорректный ввод.");
                    }
                    break;
                }

                Console.WriteLine();
            }
        }
    }
}
