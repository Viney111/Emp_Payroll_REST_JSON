using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll_REST
{
    public class Employee
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string salary { get; set; }

        public override string ToString()
        {
            return $"EmployeeID = {ID}, Name = {name},Salary = {salary} ";
        }
    }
}
