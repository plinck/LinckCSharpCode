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

            string currentBibId = "93";

            myURLString = @"http://www.ironman.com/triathlon/events/americas/ironman-70.3/augusta/results.aspx?rd=20150927&race=augusta70.3" +
                                @"&bidid=" + currentBibId + 
                                @"&detail=1#axzz4FGGcjBOn";

            // From System.net
            WebClient myWebClient = new WebClient();
            myHTMLPageString = myWebClient.DownloadString(myURLString);

            // From HtmlAgilityPack
            HtmlDocument myHtmlDocument = new HtmlDocument();
            myHtmlDocument.LoadHtml(myHTMLPageString);

            /*
            // Write the HTML to a file for offline viewing / analysis
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
            */

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
                        // Since you found a valid results node, the header node must be correct so grab name
                        Console.WriteLine("NAME: {0}", myH1.InnerText);

                        foundDivisionRank = true;                      // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        myDivisionRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                        Console.WriteLine("DIVISION RANK: {0}", myDivisionRank);
                    }

                    // <div id=gen-rank is gender
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='gen-rank']");
                    if (myRanksNode != null && !foundGenderRank)
                    {
                        foundGenderRank = true;                      // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        myGenderRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                        Console.WriteLine("GENDER RANK: {0}", myGenderRank);
                    }

                    // OVERALL RANK <div id=div-rank is actually overall rank (error on website)
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='div-rank']");
                    if (myRanksNode != null)
                    {
                        foundOverallRank = true;                    // Found valid results node
                        // Separate Label and Field - using System.Text.RegularExpressions.Regex
                        myOverallRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
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
                        else if (myCurrentResultField == "OVERALL")
                            Console.WriteLine("OVERALL: " + cell.InnerText);
                        else if (myCurrentResultField == "T1: SWIM-TO-BIKE")
                            Console.WriteLine("T1: SWIM-TO-BIKE: " + cell.InnerText);
                        else if (myCurrentResultField == "T2: BIKE-TO-RUN")
                            Console.WriteLine("T2: BIKE-TO-RUN: " + cell.InnerText);

                        // If 8 items found, you are done
                        if (myResultsFieldsFound >= 9)
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
                        else if (cell.InnerText.ToUpper() == "OVERALL")
                            myCurrentResultField = "OVERALL";
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
                    if (myResultsFieldsFound >= 9)
                        break;
                }//rows
                 // If 8 items found, you are done
                if (myResultsFieldsFound >= 9)
                    break;
            }//table

            Console.ReadLine();

        }
    }

    class AthleteResult
    {
        public string Name { get; set; }
        public int BibNbr { get; set; }
        public string Gender { get; set; }
        public string Division { get; set; }
        public int Age { get; set; }
        public int DivisionRank { get; set; }
        public int GenderRank { get; set; }
        public int OverallRank { get; set; }
        public string SwimTime { get; set; }
        public string BikeTime { get; set; }
        public string RunTime { get; set; }
        public string T1Time { get; set; }
        public string T2Time { get; set; }
        public string OverallTime { get; set; }

        public void BibNbrStringToInt(String bib)
        {
            BibNbr = int.Parse(bib);
        }

        public void AgreStringToInt(String age)
        {
            Age = int.Parse(age);
        }

    }
}
