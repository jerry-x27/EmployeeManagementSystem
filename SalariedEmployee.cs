using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF
{
    public class SalariedEmployee : Employee
    {
        public decimal WeeklySalary {  get; set; }
        public SalariedEmployee(string empName, decimal weeklySalary) : base(empName, EmployeeType.SalariedEmployee)
        {
            WeeklySalary = weeklySalary;
        }
        public override decimal GrossEarnings
        {
            get { return WeeklySalary; }
        }
    }
}
