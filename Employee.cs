using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWPF
{
    public abstract class Employee
    {
        private static int empId = 10000;
        public int EmployeeId { get; private set; }
        public string EmployeeName { get; set; }
        public EmployeeType Type { get; set; }
        public Employee(string empName, EmployeeType empType)
        {
            Type = empType;
            EmployeeId = empId++;
            EmployeeName = empName;
        }

        public abstract decimal GrossEarnings { get; }
        public decimal Tax => GrossEarnings * 0.20M;
        public decimal NetEarnings => GrossEarnings - Tax;
    }
}
