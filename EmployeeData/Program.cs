using System;

namespace EmployeeData
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isAdmin = false;

            Console.WriteLine();
            Console.WriteLine((String.Format("{0," + ((Console.WindowWidth / 2) + (new string("Employee Data").Length / 2)) + "}", new string("Employee Data"))));
            Console.WriteLine();

            Console.Write("Are you an admin? If yes, enter the admin PIN code: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int PIN = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.ResetColor();

            if(PIN == 7347)
            {
                isAdmin = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Logged in as an admin.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Invalid PIN. ");
                Console.ResetColor();
                Console.WriteLine("Logged in as a standard user.");
            }

            Console.WriteLine();
            EmployeeApp.Run(isAdmin); 
        }
    }
}
