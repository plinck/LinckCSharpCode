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

            myNumbers[0] = 4;
            myNumbers[1] = 8;
            myNumbers[2] = 15;
            myNumbers[3] = 16;
            myNumbers[4] = 23;

            Console.WriteLine("Array Length is {0}", myNumbers.Length);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Array index {0} is {1}", i, myNumbers[i]);
            }
            Console.ReadLine();
        }
    }
}
