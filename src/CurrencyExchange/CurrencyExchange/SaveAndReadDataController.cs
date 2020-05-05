using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange
{
    public class SaveAndReadDataController
    {
        /// <summary>
        /// Сохранение строки по заданному пути.
        /// </summary>
        /// <param name="path">Путь.</param>
        /// <param name="text">Текст.</param>
        public async Task SaveDateAsync(string path, string text)
        {
            path = path ?? throw new ArgumentNullException(nameof(path));
            text = text ?? throw new ArgumentNullException(nameof(text));

            using var sw = new StreamWriter(path, true, Encoding.UTF8);
            await sw.WriteLineAsync(text);
        }

        /// <summary>
        /// Вывод сохраненной информации на консоль.
        /// </summary>
        public void ReadData()
        {
            try
            {
                var sr = new StreamReader(Constants.PATH, Encoding.UTF8);
                Console.WriteLine(sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка.. Сообщение: {ex.Message}");
            }
        }
    }
}
