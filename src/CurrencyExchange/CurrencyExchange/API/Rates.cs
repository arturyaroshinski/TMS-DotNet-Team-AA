using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API
{
    public class Rate
    {
        /// <summary>
        /// Внутренний код.
        /// </summary>
        [Key]
        public int Cur_ID { get; set; }

        /// <summary>
        /// Дата, на которую запрашивается курс.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Буквенный код.
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Количество единиц иностранной валюты.
        /// </summary>
        public int Cur_Scale { get; set; }

        /// <summary>
        /// Наименование валюты на русском языке во множественном, либо в единственном числе, в зависимости от количества единиц.
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Курс.
        /// </summary>
        public decimal? Cur_OfficialRate { get; set; }
    }

    public class RateShort
    {
        /// <summary>
        /// Внутренний код.
        /// </summary>
        public int Cur_ID { get; set; }

        /// <summary>
        /// Дата, на которую запрашивается курс.
        /// </summary>
        [Key]
        public System.DateTime Date { get; set; }

        /// <summary>
        /// Курс.
        /// </summary>
        public decimal? Cur_OfficialRate { get; set; }
    }

}