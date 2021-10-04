using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeData
{
    public class Address
    {
        public string country;
        public string city;
        public string street;
        public int buildingNumber;
        public int apartment;

        public Address(string Country, string City, string Street, int BuildingNumber, int Apartment)
        {
            this.country = Country;
            this.city = City;
            this.street = Street;
            this.buildingNumber = BuildingNumber;
            this.apartment = Apartment;
        }

        public override string ToString()
        {
            return $"{country} {city} {street} {buildingNumber} {apartment}";
        }
    }
}
