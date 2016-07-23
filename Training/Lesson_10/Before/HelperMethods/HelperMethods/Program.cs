using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Name Game");

            Console.Write("What's your first name? ");
            string firstName = Console.ReadLine();

            Console.Write("What's your last name? ");
            string lastName = Console.ReadLine();

            Console.Write("In what city were you born?");
            string city = Console.ReadLine();

            Console.WriteLine("Results: ");
            /*
            string reversedFirstName = ReverseString(firstName);
            string reversedLastName = ReverseString(lastName);
            string reversedCity = ReverseString(city);
            DisplayResult(reversedFirstName, reversedLastName, reversedCity);
            */

            DisplayResult(ReverseString(firstName), ReverseString(lastName), ReverseString(city));
            Console.WriteLine();
            DisplayResult(ReverseString(firstName) + " " + ReverseString(lastName) + " " + ReverseString(city));
            
            Console.ReadLine();
        }

        private static string ReverseString(string message)
        {
            char[] messageArray = message.ToCharArray();
            Array.Reverse(messageArray);

            return String.Concat(messageArray);
        }

        private static void DisplayResult(string s1, string s2, string s3)
        {
            Console.Write(String.Format("{0} {1} {2}", s1, s2, s3));

        }

        // Overloaded method
        private static void DisplayResult(string s1)
        {
            Console.Write(String.Format("{0} ", s1));

        }

    }
}
