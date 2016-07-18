using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bobs big giveaway");
            string userValue;
            string message = "";

            do
            {
                Console.Write("Choose Door 1, 2, 3 or 4");
                userValue = Console.ReadLine();

                if (userValue == "1")
                {
                    message = "You won a new car";
                }
                else if (userValue == "2")
                {
                    message = "You won a new boat";
                }
                else if (userValue == "3")
                {
                    message = "You suck";
                }
                else
                {
                    message = "You must pick 1, 2, 3, or 4";
                }
                Console.WriteLine(message);
            }
            while (userValue != "4");
        }
    }
}
