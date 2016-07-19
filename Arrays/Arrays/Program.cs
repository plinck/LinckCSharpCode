using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myNumbers = new int[5];
            int[] myNewNumbers = new int[] { 4, 8, 12, 16, 55, 67, 88, 98 };  // Sets array size automatically
            string[] myString = new string[] { "Paul", "Lynn", "Spencer", "Andrew" }; 

            string myBiggie = "You cant always get what you want";

            char[] charArray = myBiggie.ToCharArray();

            // I edited this file on my MAC using Visual Studio Editor - Cool
            // Check out if line feeds etc worked with GIT
            myNumbers[0] = 4;
            myNumbers[1] = 8;
            myNumbers[2] = 15;
            myNumbers[3] = 16;
            myNumbers[4] = 23;

            Array.Reverse(charArray);
            foreach (char myChar in charArray)
            {
                Console.Write(myChar);
            }

            Console.WriteLine("Array Length is {0}", myNumbers.Length);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Array index {0} is {1}", i, myNumbers[i]);
            }
            foreach (var stringName in myString)
            {
                Console.WriteLine("Name {0}", stringName);

            }

            Console.ReadLine();
        }
    }
}
