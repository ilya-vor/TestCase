using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class MonthlySalarySummary
    {
        public string Month { get; set; }
        public decimal TotalAmount { get; set; }
        public string AllPayments { get; set; }
    }
}
