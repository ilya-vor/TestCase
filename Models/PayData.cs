using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    [XmlRoot("Pay")]
    public class PayData
    {
        [XmlElement("item")]
        public List<PayItem> Items { get; set; }

        [XmlAttribute("total")]
        public string Total { get; set; }

        public void CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                string amountString = item.Amount.Replace(".", ",");
                if (decimal.TryParse(amountString, out decimal amount))
                {
                    total += amount;
                }
            }
            Total = total.ToString("F2").Replace(".", ",");
        }
    }
}
