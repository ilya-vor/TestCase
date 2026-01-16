using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    public class PayItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("surname")]
        public string Surname { get; set; }

        [XmlAttribute("amount")]
        public string Amount { get; set; }

        [XmlAttribute("mount")]
        public string Month { get; set; }
    }
}
