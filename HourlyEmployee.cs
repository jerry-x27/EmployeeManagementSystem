using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF
{
    public class HourlyEmployee : Employee
    {
        public decimal HourlyWage { get; set; }
        public decimal HoursWorked { get; set; }
        public HourlyEmployee(string empName, decimal hourlyWage, decimal hoursWorked) : base(empName, EmployeeType.HourlyEmployee)
        {
            HourlyWage = hourlyWage;
            HoursWorked = hoursWorked;
        }

        public override decimal GrossEarnings
        {
            get
            {
                decimal overTime = HoursWorked - 40;
                if (overTime <= 0)
                {
                    return HoursWorked * HourlyWage;
                }
                else
                {
                    return (40 * HourlyWage) + (overTime * (HourlyWage * 1.5M));
                }
            }
        }
    }
}
