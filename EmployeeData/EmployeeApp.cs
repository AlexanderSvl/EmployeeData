using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmployeeData
{
    public class EmployeeApp
    {
        private static List<Employee> employees;

        public static void Run(bool isAdmin)
        {
            employees = new List<Employee>();

            Console.Write("Enter one of the following commands: ");
            Console.WriteLine();

            string infoToDisplay = "";

            if (isAdmin)
            {
                infoToDisplay = "'sort' - Sort the data by a given parameter; 'add' - Add an employee; 'remove' - Remove an employee; 'print' - Print the data; 'read' - Read info from file; 'write' - Write info to file;";
                Console.WriteLine(infoToDisplay);
                Console.WriteLine();
            }
            else
            {
                infoToDisplay = "'sort' - Sort the data by a given parameter; 'print' - Print the data; 'read' - Read info from file; 'write' - Write info to file;";
                Console.WriteLine(infoToDisplay);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            string command = Console.ReadLine();
            Console.ResetColor();

            while (command.ToLower() != "end")
            {
                if (command.ToLower() == "sort")
                {
                    Console.WriteLine();
                    Console.Write("Enter parameter to sort by (name, family, city, salary): ");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    string propToSortBy = Console.ReadLine();
                    Console.ResetColor();

                    Sort(propToSortBy);
                }
                else if (command.ToLower() == "add")
                {
                    AddEmployee();
                }
                else if (command.ToLower() == "remove")
                {
                    Console.WriteLine("Enter ID of the user you want to remove: ");
                    int employeeID = int.Parse(Console.ReadLine());

                    RemoveEmployee(employeeID);
                }
                else if (command.ToLower() == "print")
                {
                    PrintInfo(isAdmin);
                }
                else if (command.ToLower() == "write")
                {
                    WriteInfo(isAdmin);
                }
                else if (command.ToLower() == "read")
                {
                    ReadInfo();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid command.");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine(infoToDisplay);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                command = Console.ReadLine();
                Console.ResetColor();
            }

            WriteInfo(isAdmin);
            Console.WriteLine("Thank you for using our application!");
        }

        public static void WriteInfo(bool isAdmin)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("No data to write.");
                Console.ResetColor();
            }
            else
            {
                FileStream file = File.Open("data.txt", FileMode.Create);

                using (StreamWriter writer = new StreamWriter(file))
                {
                    if (isAdmin)
                    {
                        foreach (Employee employee in employees)
                        {
                            writer.WriteLine(employee.ToStringAdmin());
                        }
                    }
                    else
                    {
                        foreach (Employee employee in employees)
                        {
                            writer.WriteLine(employee.ToStringStandard());
                        }
                    }
                }

                file.Close();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data is ready and located in the file 'data.txt'.");
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        public static void ReadInfo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("The data should be in the following format: {name}, {surname}, {family}, {country}, {city}, {street}, {building number}, {apartment}, {ID}, {salary}");
            Console.ResetColor();
            Console.Write("Enter the name of the file, containing the data: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string fileName = Console.ReadLine();
            Console.ResetColor();

            if (!File.Exists(fileName))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("File with that name does not exist!");
                Console.ResetColor();
            }
            else
            {
                FileStream file = File.Open(fileName, FileMode.Open);

                using (StreamReader reader = new StreamReader(file))
                {
                    try
                    {
                        bool infoExists = false;

                        string line = reader.ReadLine();

                        while (line != null)
                        {
                            if (line != "")
                            {
                                string[] employeeData = line.Split(", ");

                                Employee employeeToAdd = new Employee(employeeData[0], employeeData[1], employeeData[2], new Address(employeeData[3], employeeData[4], employeeData[5], int.Parse(employeeData[6]), int.Parse(employeeData[7])), int.Parse(employeeData[8]), decimal.Parse(employeeData[9]));

                                if (employees.Find(item => item.ID == employeeToAdd.ID) == null)
                                {
                                    employees.Add(employeeToAdd);
                                    infoExists = true;
                                }

                                line = reader.ReadLine();
                            }
                            else
                            {
                                line = reader.ReadLine();
                            }
                        }

                        if (infoExists)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Info is read.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No info provided in the file.");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(exp.Message);
                        Console.ResetColor();
                    }
                }

                file.Close();
            }
        }

        public static void PrintInfo(bool isAdmin)
        {
            Console.WriteLine();

            if(employees.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("No data to print.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (isAdmin)
                {
                    foreach (Employee employee in employees)
                    {
                        Console.WriteLine(employee.ToStringAdmin());
                    }
                }
                else
                {
                    foreach (Employee employee in employees)
                    {
                        Console.WriteLine(employee.ToStringStandard());
                    }
                }

                Console.ResetColor();
            }
        }

        public static void Sort(string prop)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("No data to print.");
                Console.ResetColor();
            }
            else
            {
                if (prop == "name" || prop == "family" || prop == "salary")
                {
                    employees = employees.OrderBy(x => x.GetType().GetProperty(prop).GetValue(x)).ToList();
                }
                else if (prop == "city")
                {
                    employees = employees.OrderBy(x => x.address.city).ToList();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine();
                    Console.WriteLine($"Parameter {prop} is not a valid parameter!");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine($"Employees have been sorted by {prop.ToUpper()}");
                Console.ResetColor();
            }
           
        }

        public static void AddEmployee()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter employee data in the following format: {name}, {surname}, {family}, {country}, {city}, {street}, {building number}, {apartment}, {ID}, {salary}");
            Console.ForegroundColor = ConsoleColor.Blue;
            string[] employeeData = Console.ReadLine().Split(", ");
            Console.ResetColor();

            if(employeeData.Length != 10)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid employee format!");
                Console.ResetColor();
            }
            else
            {
                Employee employeeToAdd = new Employee(employeeData[0], employeeData[1], employeeData[2], new Address(employeeData[3], employeeData[4], employeeData[5], int.Parse(employeeData[6]), int.Parse(employeeData[7])), int.Parse(employeeData[8]), decimal.Parse(employeeData[9]));

                if (employees.Find(item => item.ID == employeeToAdd.ID) == null)
                {
                    employees.Add(employeeToAdd);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Employee added.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("User with the same ID already exists!");
                    Console.ResetColor();
                }
            }            
        }

        public static void RemoveEmployee(int ID)
        {
            Employee employeeToRemove = employees.SingleOrDefault(x => x.ID == ID);

            if (!employees.Contains(employeeToRemove))
            {
                Console.WriteLine($"Error! Employee with ID '{ID}' does not exist!");
            }
            else
            {
                employees.Remove(employeeToRemove);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Employee removed.");
                Console.ResetColor();
            }
        }
    }
}
