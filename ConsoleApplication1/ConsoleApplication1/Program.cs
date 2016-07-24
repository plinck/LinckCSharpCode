using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;
using System.IO;

// Wiring this to grab the ironman website and parse it
// eventually want to stuff it into a database and view results on phone and webpage
// for specific bib numbers and have soring capabilites - like what charlie wrote
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string myURLString;
            string myHTMLPageString;

            myURLString = @"http://www.ironman.com/triathlon/events/americas/ironman-70.3/augusta/results.aspx?rd=20150927&race=augusta70.3&bidid=93&detail=1#axzz4FGGcjBOn";

            // From System.net  - get the webpage
            WebClient myWebClient = new WebClient();
            myHTMLPageString = myWebClient.DownloadString(myURLString);
 
            // From HtmlAgilityPack - parse the HTML file
            HtmlDocument myHtmlDocument = new HtmlDocument();
            myHtmlDocument.LoadHtml(myHTMLPageString);
            // Console.WriteLine(myHTMLPageString);
            // Console.ReadLine();
            try
            {
                File.WriteAllText(@"C:\Users\paul\Documents\GitRepos\LinckCSharpCode\ConsoleApplication1\ConsoleApplication1\files\results.txt", myHTMLPageString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was a problem writing the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Clean Up
            }

            Console.WriteLine("---------");

            string myCurrentCell = "";
            int myFieldsFound = 0;
            foreach (HtmlNode header in myHtmlDocument.DocumentNode.SelectNodes("//header"))
            {
                Console.WriteLine("Header: " + header.InnerText);
            }

            foreach (HtmlNode table in myHtmlDocument.DocumentNode.SelectNodes("//table[@id='general-info']"))
            {
                Console.WriteLine("Found: " + table.Id);
                foreach (HtmlNode row in table.SelectNodes("//tr"))
                {
                    foreach (HtmlNode cell in table.SelectNodes("//th|//td"))
                    {
                        if (myCurrentCell == "BIB")
                            Console.WriteLine("BIB: " + cell.InnerText);
                        else if (myCurrentCell == "DIVISION")
                            Console.WriteLine("DIVISION: " + cell.InnerText);
                        else if (myCurrentCell == "AGE")
                            Console.WriteLine("AGE: " + cell.InnerText);
                        else if (myCurrentCell == "SWIM")
                            Console.WriteLine("SWIM: " + cell.InnerText);
                        else if (myCurrentCell == "BIKE")
                            Console.WriteLine("BIKE: " + cell.InnerText);
                        else if (myCurrentCell == "RUN")
                            Console.WriteLine("RUN: " + cell.InnerText);
                        else if (myCurrentCell == "T1: SWIM-TO-BIKE")
                            Console.WriteLine("T1: SWIM-TO-BIKE: " + cell.InnerText);
                        else if (myCurrentCell == "T2: BIKE-TO-RUN")
                            Console.WriteLine("T2: BIKE-TO-RUN: " + cell.InnerText);

                        // If 8 items found, you are done
                        if (myFieldsFound >= 8)
                            break;

                        // Save this Label to see the value is next cell
                        if (cell.InnerText.ToUpper() == "BIB")
                            myCurrentCell = "BIB";
                        else if (cell.InnerText.ToUpper() == "DIVISION")
                            myCurrentCell = "DIVISION";
                        else if (cell.InnerText.ToUpper() == "AGE")
                            myCurrentCell = "AGE";
                        else if (cell.InnerText.ToUpper() == "SWIM")
                            myCurrentCell = "SWIM";
                        else if (cell.InnerText.ToUpper() == "BIKE")
                            myCurrentCell = "BIKE";
                        else if (cell.InnerText.ToUpper() == "RUN")
                            myCurrentCell = "RUN";
                        else if (cell.InnerText.ToUpper() == "T1: SWIM-TO-BIKE")
                            myCurrentCell = "T1: SWIM-TO-BIKE";
                        else if (cell.InnerText.ToUpper() == "T2: BIKE-TO-RUN")
                            myCurrentCell = "T2: BIKE-TO-RUN";
                        else
                            myCurrentCell = "";

                        // If anything found, add one to count so when all found, quit
                        if (myCurrentCell != "")
                            myFieldsFound += 1;

                    }//cols
                     // If 8 items found, you are done
                    if (myFieldsFound >= 8)
                        break;
                }//rows
                 // If 8 items found, you are done
                if (myFieldsFound >= 8)
                    break;
            }//table

            Console.ReadLine();

        }
    }
}
