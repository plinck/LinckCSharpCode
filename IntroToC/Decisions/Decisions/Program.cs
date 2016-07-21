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
                    message = "Car";
                }
                else if (userValue == "2")
                {
                    message = "Boat";
                }
                else if (userValue == "3")
                {
                    message = "Sucky Egg";
                }
                else
                {
                    message = "Nothing";
                }
                Console.WriteLine("You Entered {0}, so you won a {1}", userValue, message);
            }
            while (userValue != "4");

            while (userValue == "1")
                userValue = "2";
        }
    }
}
