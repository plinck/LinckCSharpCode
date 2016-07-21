using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            int x;
            int y;

            x = 7;
            y = x + 3;
            Console.WriteLine(y);
            Console.ReadLine();

            Console.WriteLine("What is your name?");
            Console.Write("What is your first name:");

            String myFirstName;
            myFirstName = Console.ReadLine();

            String myLastName;
            Console.Write("What is your last name:");

            myLastName = Console.ReadLine();
            Console.WriteLine("Hello, " + myFirstName + " " + myLastName);
            Console.ReadLine();


        }
    }
}
