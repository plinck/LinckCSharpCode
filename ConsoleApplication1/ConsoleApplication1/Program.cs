using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

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

            // From System.net
            WebClient myWebClient = new WebClient();
            myHTMLPageString = myWebClient.DownloadString(myURLString);
 
            // From HtmlAgilityPack
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

            string myCurrentResultField = "";
            int myResultsFieldsFound = 0;
            HtmlAgilityPack.HtmlNode myRanksNode;
            bool foundDivisionRank = false, foundGenderRank = false, foundOverallRank = false;
            int myDivisionRank, myGenderRank, myOverallRank;
            

            // First, get the node for the division class that contains the results
            HtmlAgilityPack.HtmlNode myDivisionNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='moduleContentInner clear']");

            // Find the correct header for the results 
            foreach (HtmlNode header in myDivisionNode.SelectNodes("//header"))
            {
                HtmlNode myH1 = header.SelectSingleNode("//h1");
                foreach (HtmlNode myRank in myH1.SelectNodes("//div"))
                {
                    // DIVISION RANK <div id=rank is actually division rank (error on website)
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='rank']");
                    if (myRanksNode != null && !foundDivisionRank)
                    {
                        foundDivisionRank = true;                      // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        myDivisionRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                        Console.WriteLine("DIVISION RANK: {0}", myDivisionRank);
                    }
                    else
                        myCurrentResultField = "";

                    // <div id=gen-rank is gender 
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='gen-rank']");
                    if (myRanksNode != null && !foundGenderRank)
                    {
                        foundGenderRank = true;                      // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        int myOverallRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                        Console.WriteLine("GENDER RANK: {0}", myOverallRank);
                    }
                    else
                        myCurrentResultField = "";

                    // OVERALL RANK <div id=div-rank is actually overall rank (error on website)
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='div-rank']");
                    if (myRanksNode != null)
                    {
                        myResultsFieldsFound += 1;                      // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        int myOverallRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                        Console.WriteLine("OVERALL RANK: {0}", myOverallRank);
                    }
                    else
                        myCurrentResultField = "";

 
                    // If all 3 items found, you are done
                    if (foundDivisionRank && foundGenderRank && foundOverallRank)
                        break;
                }
                // If all 3 items found, you are done
                if (foundDivisionRank && foundGenderRank && foundOverallRank)
                    break;
            }

            myCurrentResultField = "";
            myResultsFieldsFound = 0;

            // Find all the result details
            foreach (HtmlNode table in myHtmlDocument.DocumentNode.SelectNodes("//table[@id='general-info']"))
            {
                Console.WriteLine("Found: " + table.Id);
                foreach (HtmlNode row in table.SelectNodes("//tr"))
                {
                    foreach (HtmlNode cell in table.SelectNodes("//th|//td"))
                    {
                        if (myCurrentResultField == "BIB")
                            Console.WriteLine("BIB: " + cell.InnerText);
                        else if (myCurrentResultField == "DIVISION")
                            Console.WriteLine("DIVISION: " + cell.InnerText);
                        else if (myCurrentResultField == "AGE")
                            Console.WriteLine("AGE: " + cell.InnerText);
                        else if (myCurrentResultField == "SWIM")
                            Console.WriteLine("SWIM: " + cell.InnerText);
                        else if (myCurrentResultField == "BIKE")
                            Console.WriteLine("BIKE: " + cell.InnerText);
                        else if (myCurrentResultField == "RUN")
                            Console.WriteLine("RUN: " + cell.InnerText);
                        else if (myCurrentResultField == "T1: SWIM-TO-BIKE")
                            Console.WriteLine("T1: SWIM-TO-BIKE: " + cell.InnerText);
                        else if (myCurrentResultField == "T2: BIKE-TO-RUN")
                            Console.WriteLine("T2: BIKE-TO-RUN: " + cell.InnerText);

                        // If 8 items found, you are done
                        if (myResultsFieldsFound >= 8)
                            break;

                        // Save this Label to see the value is next cell
                        if (cell.InnerText.ToUpper() == "BIB")
                            myCurrentResultField = "BIB";
                        else if (cell.InnerText.ToUpper() == "DIVISION")
                            myCurrentResultField = "DIVISION";
                        else if (cell.InnerText.ToUpper() == "AGE")
                            myCurrentResultField = "AGE";
                        else if (cell.InnerText.ToUpper() == "SWIM")
                            myCurrentResultField = "SWIM";
                        else if (cell.InnerText.ToUpper() == "BIKE")
                            myCurrentResultField = "BIKE";
                        else if (cell.InnerText.ToUpper() == "RUN")
                            myCurrentResultField = "RUN";
                        else if (cell.InnerText.ToUpper() == "T1: SWIM-TO-BIKE")
                            myCurrentResultField = "T1: SWIM-TO-BIKE";
                        else if (cell.InnerText.ToUpper() == "T2: BIKE-TO-RUN")
                            myCurrentResultField = "T2: BIKE-TO-RUN";
                        else
                            myCurrentResultField = "";

                        // If anything found, add one to count so when all found, quit
                        if (myCurrentResultField != "")
                            myResultsFieldsFound += 1;

                    }//cols
                     // If 8 items found, you are done
                    if (myResultsFieldsFound >= 8)
                        break;
                }//rows
                 // If 8 items found, you are done
                if (myResultsFieldsFound >= 8)
                    break;
            }//table

            Console.ReadLine();

        }
    }
}
