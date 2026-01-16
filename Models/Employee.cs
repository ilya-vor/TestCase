using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    public class Employee
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("surname")]
        public string Surname { get; set; }

        [XmlElement("salary")]
        public ObservableCollection<Salary> Salaries { get; set; } = new ObservableCollection<Salary>();

        public string FullName => $"{Name} {Surname}";

        public decimal TotalSalary => Salaries.Sum(s => s.Amount);
    }
}
