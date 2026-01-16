using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class EmployeeData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Dictionary<string, decimal> MonthlySalaries { get; set; }
        public decimal TotalSalary { get; set; }

        public EmployeeData()
        {
            MonthlySalaries = new Dictionary<string, decimal>();
        }
    }
}
