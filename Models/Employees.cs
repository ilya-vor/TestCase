using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestCase.Models
{
    [XmlRoot("Employees")]
    public class Employees
    {
        [XmlElement("Employee")]
        public ObservableCollection<Employee> EmployeeList { get; set; } = new ObservableCollection<Employee>();
    }
}
