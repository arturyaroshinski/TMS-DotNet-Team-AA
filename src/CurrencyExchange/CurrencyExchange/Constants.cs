namespace CurrencyExchange
{
    /// <summary>
    /// Класс с константами.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public const string PATH = @"..\test.txt";

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public const string EXCEPTION_TEXT = "Возникла ошибка.. Сообщение: ";

        /// <summary>
        /// Сслыка на API списка валют.
        /// </summary>
        public const string CURRENCIES = "https://www.nbrb.by/api/exrates/currencies";

        /// <summary>
        /// Ссылка на API списка курсов валют.
        /// </summary>
        public const string RATES = "https://www.nbrb.by/api/exrates/rates/";
    }
}
