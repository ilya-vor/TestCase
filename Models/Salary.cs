using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    public class Salary
    {
        [XmlAttribute("amount")]
        public string AmountString { get; set; }

        [XmlAttribute("mount")]
        public string Month { get; set; }

        // Свойство для получения суммы как числа
        public decimal Amount
        {
            get
            {
                // Заменяем запятую на точку для корректного парсинга
                var normalized = AmountString.Replace(',', '.');
                return decimal.Parse(normalized, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}
