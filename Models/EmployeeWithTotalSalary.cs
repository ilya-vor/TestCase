using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    public class EmployeeWithTotalSalary
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("surname")]
        public string Surname { get; set; }

        [XmlAttribute("totalSalary")]
        public decimal TotalSalary { get; set; }

        [XmlElement("salary")]
        public ObservableCollection<Salary> Salaries { get; set; }
    }
}
