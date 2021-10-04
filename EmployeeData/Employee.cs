using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeData
{
    public class Employee
    {
        public string name { get; private set; }
        public string surname { get; private set; }
        public string family { get; private set; }
        public Address address { get; private set; }
        public int ID { get; private set; }
        public decimal salary { get; private set; }

        public Employee(string Name, string Surname, string Family, Address Address, int id, decimal Salary)
        {
            this.name = Name;
            this.surname = Surname;
            this.family = Family;
            this.address = Address;
            this.ID = id;
            this.salary = Salary;
        }

        public string ToStringAdmin()
        {
            return $"{ID} | {name} {surname} {family} | {address.ToString()} | Salary: {String.Format("{0:0.00}", salary)}";
        }

        public string ToStringStandard()
        {
            return $"{ID} | {name} {surname} {family} | {address.ToString()}";
        }
    }
}
