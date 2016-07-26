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

            myURLString = URLRequest.formatURL(currentBibId);
            // myHTMLPageString = HTMLPageString.getHTMLPageString(myURLString);
            myHTMLPageString = HTMLPageString.readPageFromFile(myURLString);  // for testing get from file

            HTMLPage myHTMLPage = new HTMLPage(myHTMLPageString);

            // From HtmlAgilityPack
            HTMLPage myHTMLPage = new HTMLPage(htmlPageString); 

            Console.ReadLine();

        }
    }
    class MyClass
    {


    }


    /****************************************************************
    // URLRequest - STATIC -   
    // This class contains helper methods to build the properly
    // formatted URL request depending on various inputs
    *****************************************************************/
    static class URLRequest
    {
        static private string myURLString;

        public static string formatURL (string bib)
        {
            myURLString = @"http://www.ironman.com/triathlon/events/americas/ironman-70.3/augusta/results.aspx?rd=20150927&race=augusta70.3" + 
                            @"&bidid=" + bib +
                            @"&detail=1#axzz4FGGcjBOn";
            
            return myURLString;
        }
    }

    /****************************************************************
    // HTMLPageString static class
    // This class gets the page from the web and returns the
    // contents in string format that can be used for later parsing
    *****************************************************************/
    static class HTMLPageString
    {
        private string myHTMLPageString;

        public string getHTMLPageString(string url)
        {
            // From System.net
            WebClient myWebClient = new WebClient();
            myHTMLPageString = myWebClient.DownloadString(url);

            return myHTMLPageString;
        }

        public void savePageToFile(htmlPageString)
        {
            string currentDirectory = @"C:\Users\paul\Documents\GitRepos\LinckCSharpCode\ConsoleApplication1\ConsoleApplication1\";
            string currentFile = @"files\results.txt";

            // Write the HTML to a file for offline viewing / analysis
            // First, get the current directory and the
            try
            {
                File.WriteAllText(currentDirectory + currentFile, htmlPageString);
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
        }
        
        // use this for testing without internet connection
        public string readPageFromFile()
        {
            string currentDirectory = @"C:\Users\paul\Documents\GitRepos\LinckCSharpCode\ConsoleApplication1\ConsoleApplication1\";
            string currentFile = @"files\results.txt";

            try
            {
                File.ReadAllText(currentDirectory + currentFile, htmlPageString);
                return htmlPageString;
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
        }

    }

   /****************************************************************
    // HTMLPage class
    // This class has all the result data on the page
    // handles the HTML parsing and saves the results
    // All the HtmlAgilityPack classes are encapsulated in this class
    *****************************************************************/
    static class HTMLPage
    {
        private HtmlDocument myHtmlDocument;

        private int athleteDivisionRank;
        private int athleteGenderRank;
        private int athleteOverallRank;
        private string athleteBib;
        private string athleteName;
        private string athleteDivision;
        private string athleteAge;
        private string athleteSwimTime;
        private string athleteBikeTime;
        private string athleteRunTime;
        private string athleteOverallTime;
        private string athleteT1Time;
        private string athleteT2Time;

        public HtmlDocument HTMLPage(htmlPageString)
        {
            // From HtmlAgilityPack
            HtmlDocument myHtmlDocument = new HtmlDocument();
            myHtmlDocument.LoadHtml(htmlPageString);

            string myCurrentResultField = "";
            int myResultsFieldsFound = 0;
            bool foundDivisionRank = false, foundGenderRank = false, foundOverallRank = false;
            
            HtmlAgilityPack.HtmlNode myRanksNode;

            // First, get the node for the division class that contains the results
            HtmlNode myDivisionNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@class='moduleContentInner clear']");

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
                        athleteDivisionRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                    }

                    // GENDER RANK <div id=gen-rank is gender
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='gen-rank']");
                    if (myRanksNode != null && !foundGenderRank)
                    {
                        foundGenderRank = true;                      // Found valid results node
                        athleteGenderRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
                    }

                    // OVERALL RANK <div id=div-rank is actually overall rank (error on website)
                    myRanksNode = myHtmlDocument.DocumentNode.SelectSingleNode("//div[@id='div-rank']");
                    if (myRanksNode != null)
                    {
                        foundOverallRank = true;                    // Found valid results node
                        athleteOverallRank = int.Parse(Regex.Match(myRanksNode.InnerText, @"\d+").Value);
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
                            athleteBib = cell.InnerText;
                        else if (myCurrentResultField == "DIVISION")
                            athleteDivision cell.InnerText;
                        else if (myCurrentResultField == "AGE")
                            athleteAge = cell.InnerText;
                        else if (myCurrentResultField == "SWIM")
                            athleteSwimTime = cell.InnerText;
                        else if (myCurrentResultField == "BIKE")
                            athleteBikeTime = cell.InnerText;
                        else if (myCurrentResultField == "RUN")
                            athleteRunTime = cell.InnerText;
                        else if (myCurrentResultField == "OVERALL")
                            athleteOverallTime = cell.InnerText;
                        else if (myCurrentResultField == "T1: SWIM-TO-BIKE")
                            athleteT1Time = cell.InnerText;
                        else if (myCurrentResultField == "T2: BIKE-TO-RUN")
                            athleteT2Time = cell.InnerText;

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

        }
        

    }

    /****************************************************************
    // HTMLPage AthleteResult
    // Contains a single Athlete's results
    *****************************************************************/
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
