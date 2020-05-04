using System;
using CurrencyExchange;

namespace CurrencyExchangeApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var curController = new CurrencyExchangeController();

            Menu(curController);
        }

        private static void Menu(CurrencyExchangeController controller)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("[1] Список валют.\n[2] Узнать курс.\n[3] Вывести сохраненные запросы\n[4] Выход.");
                Console.WriteLine("Выберите действие, путем ввода его номера.");
               

                switch (Console.ReadLine())
                {
                    case "1":
                    {
                        controller.ShowAllCurrencies();
                        break;
                    } 
                    case "2":
                    {
                        controller.ShowRate();
                        break;
                    }
                    case "3":
                    {
                        controller.ShowSavedData();
                        break;
                    }
                    case "4":
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
    }
}
