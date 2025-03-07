using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF
{
    public class CommissionEmployee : Employee
    {
        public decimal GrossSales { get; set; }
        public decimal CommissionRate { get; set; }
        public CommissionEmployee(string empName, decimal grossSales, decimal commRate) : base(empName, EmployeeType.CommissionEmployee)
        {
            GrossSales = grossSales;
            CommissionRate = commRate;
        }
        public override decimal GrossEarnings
        {
            get { return GrossSales * CommissionRate; }
        }
    }
}
