using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.DirectoryServices.AccountManagement;


namespace MVATwentyQuestions
{
    class Program
    {

        #region Point struct

        public struct PointStruct
        {
            public int x;
            public int y;

            public PointStruct(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        #endregion

        #region MinMax struct
        struct MinMax
        {
            public int min;
            public int max;
        }
        #endregion

        static void Main(string[] args)
        {
                        
            #region Structs vs Classes
            //PointStruct structPoint = new PointStruct();
            //structPoint.x = 10;
            //structPoint.y = 10;

            //Console.WriteLine("Initial struct values are " + structPoint.x + ", " + structPoint.y);
           ModifyStructPoint(structPoint);
            //Console.WriteLine("After ModifyStructPoint, struct values are " + structPoint.x + ", " + structPoint.y);
            //Console.WriteLine();


            //PointClass classPoint = new PointClass(10, 10);
            //Console.WriteLine("Initial class values are " + classPoint.x + ", " + classPoint.y);
            //ModifyClassPoint(classPoint);
            //Console.WriteLine("After ModifyClassPoint, class values are " + classPoint.x + ", " + classPoint.y);
            #endregion

            #region string vs String
            //StringFunctions();
            #endregion

            #region Base Constructor
            //Console.WriteLine();
            //Test nTest = new Test("Base");
            //Console.WriteLine(nTest._name);

            //InheritTest iTest = new InheritTest("NewName");
            //Console.WriteLine(iTest._name);

            #endregion

            #region Counting Strings
                
                //CountStrings();

             #endregion

            #region Break vs Continue

            breakvsContinue();

            #endregion

            #region ref vs out

            //string outString = "This is the original outString";
            //Console.WriteLine(outString);
            //outMethod(out outString);
            //Console.WriteLine(outString);

            //string refString = "This is the original ref string";
            //Console.WriteLine(refString);
            //refMethod(ref refString);
            //Console.WriteLine(refString);

            #endregion

            #region Abstract vs Virtual

            // comment the abstract method in the class

            
            #endregion

            #region String Encryption
            //string plainText = "This is plain text that we will encrypt";
            //string password = "P@$$w0rd";

            //Console.WriteLine(plainText);
            //Console.WriteLine();
            
            //// create a new instance of our encryption class passing in the password as the key
            //DESEncrypt testEncrypt = new DESEncrypt();

            //string encText = testEncrypt.EncryptString(plainText, password);
            //Console.WriteLine("****** Encrypted text ******");
            //Console.WriteLine(encText);
            //Console.WriteLine();

            //Console.WriteLine("****** Decrypted text ******");
            //string plain = testEncrypt.DecryptString(encText, password);
            //Console.WriteLine(plain);
            //Console.WriteLine();

            #endregion

            #region Get IP Address

            //GetPrivateIP();
            //string publicIP = GetPublicIP();
            //Console.WriteLine("Public IP is: {0}", publicIP);

            #endregion


            #region Parse XML

//            String xmlString =
//                @"<bookstore>
//                    <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
//                        <title>The Autobiography of Benjamin Franklin</title>
//                        <author>
//                            <first-name>Benjamin</first-name>
//                            <last-name>Franklin</last-name>
//                        </author>
//                        <price>8.99</price>
//                    </book>
//                </bookstore>";

//            ParseXml(xmlString);

            #endregion

            #region Get App Path

            //GetAppPath();

            #endregion

            #region Power of 2
            bool result = false;

            result = CheckPowerOfTwo(8);
            Console.WriteLine(result.ToString());

            #endregion

            #region Get index of foreach iteration

            //ForEachIndex();

            #endregion

            #region Calculate Age

            //CalcAge(DateTime.Parse("10/13/1998"));

            #endregion

            #region String enums
            //StringEnums();
            #endregion

            #region Round Decimal to 2 places for output
            //RoundDecimal();
            #endregion

            #region Sort Dictionary by Values
            //SortDictionary();
            #endregion

            #region Remove Duplicates in Array
            //string[] myArray = new string[] { "First", "First", "Second", "Third", "4th", "4th" };
            //RemoveDups(myArray);
            #endregion

            #region Return Multiple Values in a Function

            //int[] myArray = new int[] { 12, 34, 56, 90 };
            //MinMax results = MultipleReturns(myArray);
            //int arrayMin = results.min;
            //int arrayMax = results.max;

            //Console.WriteLine("Array min is {0}, and array max is {1}", arrayMin, arrayMax);

            #endregion

        }

        #region Struct vs Classes
        static void ModifyStructPoint(PointStruct newStruct)
        {
            newStruct.x = 20;
            newStruct.y = 20;
            Console.WriteLine("Inside ModifyStructPoint()");
            Console.WriteLine("Modified values are " + newStruct.x + ", " + newStruct.y);
        }

        static void ModifyClassPoint(PointClass newPoint)
        {
            newPoint.x = 20;
            newPoint.y = 20;
            Console.WriteLine("Inside ModifyClassPoint()");
            Console.WriteLine("Modified values are " + newPoint.x + ", " + newPoint.y);
        }

        #endregion

        #region String vs string

        static void StringFunctions()
        {
            Console.WriteLine(typeof(string));
            Console.WriteLine(typeof(String));

            string name = "Gerry";
            String name2 = "Gerry 2";

            Console.WriteLine(name);
            Console.WriteLine(name2);

            Console.WriteLine(string.Concat(name, name2));
            Console.WriteLine(String.Concat(name, name2));
        }
        #endregion

        #region Couting Strings
        static void CountStrings()
        {
            string test = "Hello, how are youyou you you you, you you yy?";
            int wordCount = 0;
            int charCount = 0;

            foreach (Match m in Regex.Matches(test, "you"))
            {
                wordCount++;
            }

            foreach (char value in test)
            {
                if (value == 'y')
                {
                    charCount++;
                }
            }

            Console.WriteLine("Word count: {0}", wordCount);
            Console.WriteLine("Character count: {0}", charCount);
        }

        #endregion

        #region Break vs Continue
        static void breakvsContinue()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    if (i == 0) break;
            //    DoSomeThingWith(i);
            //}

            //the break will cause the loop to exit on the first iteration - DoSomeThingWith will never be executed.
            //for (int i = 0; i < 10; i++)
            //{
            //    if (i == 0) continue;
            //    DoSomeThingWith(i);
            //}
        }

        static void DoSomeThingWith(int i)
        {
            Console.WriteLine(i);
        }

        #endregion

        #region ref vs out
        static void outMethod(out string outString)
        {
            outString = "This is the new outString value";

        }

        static void refMethod(ref string refString)
        {
            refString = "This is the new refString value";
        }

        #endregion

        #region Get IP Address

        static string GetPublicIP()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        static void GetPrivateIP()
        {
            
            foreach (var interfaces in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (var address in interfaces.GetIPProperties().UnicastAddresses)
                {
                    if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine("IP Address:  " + address.Address.ToString());
                    }
                }
            }
        }

        #endregion

        #region Parse XML

        static void ParseXml(string xmlString)
        {
            // Create a Stringbuilder object to hold the text from the XML file 
            // that we will output later
            StringBuilder output = new StringBuilder();

            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("book");
                reader.MoveToFirstAttribute();
                string genre = reader.Value;
                output.AppendLine("The genre value: " + genre);

                reader.ReadToFollowing("title");
                output.AppendLine("Content of the title element: " + reader.ReadElementContentAsString());
            }

            
            Console.WriteLine(output);
        }

        #endregion

        #region Get App Path

        static void GetAppPath()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(System.IO.Path.GetDirectoryName(path));
        }
        #endregion

        #region Power of 2

        static bool CheckPowerOfTwo(ulong number)
        {
            return (number != 0) && ((number & (number - 1)) == 0);
        }

        #endregion

        #region Get index of foreach iteration
        static void ForEachIndex()
        {
            int curIndex = 0;

            //Dictionary<string, string> diction = new Dictionary<string,string>();
            //int counter = 0;

            //diction.Add("One", "One");
            //diction.Add("Two", "Two");
            //diction.Add("Three", "Three");
            //diction.Add("Four", "Four");
            //diction.Add("Five", "Five");
            //diction.Add("Six", "Six");
            //diction.Add("Seven", "Seven");

            //foreach (Object obj in diction)
            //{
            //    Console.WriteLine(counter);
            //    counter++;
            //}


            //List<String> stringList = new List<string>();
            //stringList.Add("One");
            //stringList.Add("Two");
            //stringList.Add("Three");
            //stringList.Add("Four");
            //stringList.Add("Five");
            //stringList.Add("Six");
            //stringList.Add("Seven");

            
            //foreach(String str in stringList)
            //{
            //    curIndex = stringList.IndexOf(str);
            //    Console.WriteLine(curIndex.ToString());
            //}

            //System.Collections.ArrayList arList = new ArrayList();

            //arList.Add("One");
            //arList.Add("Two");
            //arList.Add("Three");
            //arList.Add("Four");
            //arList.Add("Five");
            //arList.Add("Six");
            //arList.Add("Seven");

            //foreach (String str in arList)
            //{
            //    curIndex = arList.IndexOf(str);
            //    Console.WriteLine(curIndex.ToString());
            //}

        }
        #endregion

        #region Calculate Age

        static void CalcAge(DateTime birthday)
        {
            //get current date
            DateTime curDate = DateTime.Today;

            // check to ensure that we have a valid birthdate prior to today
            if(birthday > curDate)
            {
                Console.WriteLine("Birthday must be equal to or before today");
            }
            else
            {
                int age = curDate.Year - birthday.Year;

                // this is required to ensure the proper value if the month is later in the year than the current month
                if (birthday.Month > curDate.Month)
                {
                    age--;
                }

                Console.WriteLine("The person's age is: {0}", age);
            }
            
        }
        #endregion

        #region String enums
        public enum AuthMethod
        {
            FORMS = 1,
            WINDOWSAUTHENTICATION = 2,
            SINGLESIGNON = 3
        }
        static void StringEnums()
        {
            AuthMethod auth = AuthMethod.FORMS;
            string str = Enum.GetName(typeof(AuthMethod), auth);
            Console.WriteLine(str);

            str = AuthMethod.FORMS.ToString();
            Console.WriteLine(str);
        }
        #endregion

        #region Round Decimal to 2 places for output
        static void RoundDecimal()
        {
            decimal value = 45432456.2490850M;
            Console.WriteLine(value);
            
            Console.WriteLine(value.ToString("#.##"));
            Console.WriteLine(String.Format("{0:0.00}", value));
            Console.WriteLine(value.ToString("n2"));
            Console.WriteLine("{0:c}", value);
        }
        #endregion

        #region Sort Dictionary by Values
        static void SortDictionary()
        {
            Dictionary<string, string> diction = new Dictionary<string,string>();
            diction.Add("3", "three");
            diction.Add("1", "one");
            diction.Add("5", "five");
            diction.Add("2", "two");

            List<KeyValuePair<string, string>> sortList = diction.ToList();

            var sorted = from entry in diction orderby entry.Value ascending select entry;
            Console.ReadLine();
        }
        #endregion

        #region Remove Duplicates in Array
        static void RemoveDups(string[] myStringArray)
        {
            // LINQ
            string[] str = myStringArray.Distinct().ToArray();


            // Array to List to Array
            List<String> myStringList = new List<string>();

            foreach (string s in myStringArray)
            {
                if (!myStringList.Contains(s))
                {
                    myStringList.Add(s);
                }
            }

            string[] newArray = myStringList.ToArray();
            Console.ReadLine();
        }
        #endregion

        #region Return Multiple Values in a Function
        
        static MinMax MultipleReturns(int[] myArray)
        {

            MinMax values = new MinMax();
            values.min = myArray.Min();
            values.max = myArray.Max();

            return values;
        }
        #endregion
    }
}
