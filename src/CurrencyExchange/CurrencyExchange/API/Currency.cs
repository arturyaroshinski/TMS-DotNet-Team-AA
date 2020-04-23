using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API
{
    public class Currency
    {
        /// <summary>
        /// Внутренний код.
        /// </summary>
        [Key]
        public int Cur_ID { get; set; }

        /// <summary>
        /// Родительский код.
        /// </summary>
        public Nullable<int> Cur_ParentID { get; set; }

        /// <summary>
        /// Цифровой код.
        /// </summary>
        public string Cur_Code { get; set; }

        /// <summary>
        /// Буквенный код.
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Наименование валюты на русском языке.
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Наименование на белорусском языке.
        /// </summary>
        public string Cur_Name_Bel { get; set; }

        /// <summary>
        /// Наименование на английском языке.
        /// </summary>
        public string Cur_Name_Eng { get; set; }

        /// <summary>
        /// Наименование валюты на русском языке, содержащее количество единиц.
        /// </summary>
        public string Cur_QuotName { get; set; }

        /// <summary>
        /// Наименование на белорусском языке, содержащее количество единиц.
        /// </summary>
        public string Cur_QuotName_Bel { get; set; }

        /// <summary>
        /// Наименование на английском языке, содержащее количество единиц.
        /// </summary>
        public string Cur_QuotName_Eng { get; set; }

        /// <summary>
        /// Наименование валюты на русском языке во множественном числе.
        /// </summary>
        public string Cur_NameMulti { get; set; }

        /// <summary>
        /// Наименование валюты на белорусском языке во множественном числе.
        /// </summary>
        public string Cur_Name_BelMulti { get; set; }

        /// <summary>
        /// Наименование на английском языке во множественном числе.
        /// </summary>
        public string Cur_Name_EngMulti { get; set; }

        /// <summary>
        /// Количество единиц иностранной валюты.
        /// </summary>
        public int Cur_Scale { get; set; }

        /// <summary>
        /// Периодичность установления курса (0 – ежедневно, 1 – ежемесячно).
        /// </summary>
        public int Cur_Periodicity { get; set; }

        /// <summary>
        /// Дата включения валюты в перечень валют, к которым устанавливается официальный курс бел. рубля.
        /// </summary>
        public System.DateTime Cur_DateStart { get; set; }

        /// <summary>
        /// Дата исключения валюты из перечня валют, к которым устанавливается официальный курс бел. рубля.
        /// </summary>
        public System.DateTime Cur_DateEnd { get; set; }
    }
}