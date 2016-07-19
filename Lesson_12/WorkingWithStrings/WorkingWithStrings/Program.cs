using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            //string myString = "My \"so-called\" life";
            //string myString = "What if I need a\nnew line?";
            //string myString = "Go to your c:\\ drive";
            //string myString = @"Go to your c:\ drive";

            string myString = String.Format("{1} = {0}", "First", "Second");
            
            // Currency formatting
            myString = string.Format("{0:C}", 123.45);
            Console.WriteLine(myString);

            // Long number formatting with commas
            myString = string.Format("{0:N}", 1234567890);
            Console.WriteLine(myString);

            // Show as precentage
            myString = string.Format("Percentage: {0:P}", .123);
            Console.WriteLine(myString);

            // Custom Format
            myString = string.Format("Phone Number: {0:(###) ###-####}", 1234567890);  // fills right to left and pushes left if too many
            Console.WriteLine(myString);

            myString = " That summer we took threes across the board  ";
            myString = myString.Substring(6, 14);
            myString = myString.ToUpper();
            myString = myString.Replace(" ", "--");
            myString = myString.Remove(6, 4);
            Console.WriteLine(myString);

            myString = " That summer we took threes across the board  ";
            myString = String.Format("Length before: {0} -- Length after: {1}",
                myString.Length, 
                myString.Trim().Length);
            Console.WriteLine(myString);


            myString = "";
            for (int i = 0; i < 100; i++)
            {
                myString += "--" + i.ToString();
            }
            Console.WriteLine(myString);

            StringBuilder myStringB = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                myStringB.Append("--");
                myStringB.Append(i);
            }

            Console.WriteLine(myString);
            Console.ReadLine();
        }
    }
}
