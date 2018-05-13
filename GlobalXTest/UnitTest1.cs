using System;
using GlobalX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GlobalXTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InputReaderInputNamesTest()
        {
            ConsoleFileLocationInputReaderTest cfirt = new ConsoleFileLocationInputReaderTest();
            List<PersonName> personNames = cfirt.InputPeopleNames();

            List<PersonName> ExpectedNames = new List<PersonName>();
            ExpectedNames.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Sumathi" } });
            ExpectedNames.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Shashikala" } });
            ExpectedNames.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Modini" } });
            ExpectedNames.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Nishanth" } });
            ExpectedNames.Add(new PersonName { LastName = "Should", GivenName = new List<string> { "catch", "this" } });

            PersonNameListComparer pnc = new PersonNameListComparer();

            bool equalresults = pnc.Equals(ExpectedNames, personNames);
            Assert.IsTrue(equalresults);
        }

        [TestMethod]
        public void InputReaderSortOrderTest()
        {
            ConsoleFileLocationInputReaderTest cfirt = new ConsoleFileLocationInputReaderTest();
            INameSorter a = cfirt.SortOrderType();
            Assert.IsTrue(a.GetType() == typeof(NameSorterAsc_LastNameGivenNames));
        }

        public class ConsoleFileLocationInputReaderTest : IConsoleInputReader
        {
            public List<PersonName> InputPeopleNames()
            {
                string path = "C:\\Users\\Smaugy\\Desktop\\unsorted-names-list.txt"; // For test just enter location hard coded..
                List<string> Names;
                List<PersonName> PeopleNames = new List<PersonName>();
                while (true)
                {
                    try
                    {
                        string[] FileText = System.IO.File.ReadAllLines(@path);
                        Names = new List<string>(FileText);
                        if (Names != null || path.ToLower() == "exit")
                            break;
                        else
                            throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        //unable to open the file. Maybe permissions or invalid path.
                        Console.WriteLine("Unable to open file in given path. Check path and permissions again. Enter 'exit' to quit the program.");
                        path = Console.ReadLine();
                    }
                }
                foreach (string fullname in Names)
                {
                    PersonName person = new PersonName();
                    string[] allnames = fullname.Split(' ');
                    if (allnames.Length > 1 && allnames.Length <= 4)
                    {
                        person.LastName = allnames[0];
                        for (int i = 1; i < allnames.Length; i++)
                        {
                            person.GivenName.Add(allnames[i]);
                        }
                    }
                    else // Skip lines where there is either only last name or more than 3 given names.
                        continue;
                    PeopleNames.Add(person);
                }
                return PeopleNames;
            }

            public Dictionary<int, string> SortType = new Dictionary<int, string>()
        {
            {1,"Sort Last Name Ascending First, Given Names Ascending next."},
            {2,"Sort only on Last Name Descending."}
        };

            public INameSorter SortOrderType()
            {
                INameSorter nameSorter;
                while (true)
                {
                    string InputKey = "2";
                    if (!SortType.ContainsKey(Convert.ToInt32(InputKey)))
                        Console.WriteLine("Enter just the number from the menu.");
                    else
                    {
                        switch (Convert.ToInt32(InputKey))
                        {
                            case 1:
                                nameSorter = new NameSorterAsc_LastNameGivenNames();
                                break;
                            case 2:
                                nameSorter = new NameSorterDesc_LastName();
                                break;
                            default:
                                nameSorter = new NameSorterAsc_LastNameGivenNames();
                                break;
                        }
                        break;
                    }
                }
                return nameSorter;
            }
        }

        [TestMethod]
        public void NameSorterTest()
        {
            NameSorterAsc_LastNameGivenNames nsl = new NameSorterAsc_LastNameGivenNames();


            List<PersonName> PersonNames = new List<PersonName>();
            PersonNames.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Sumathi" } });
            PersonNames.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Shashikala" } });
            PersonNames.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Modini" } });
            PersonNames.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Nishanth" } });

            List<PersonName> sortedNames = nsl.SortNames(PersonNames);

            List<PersonName> ExpectedResult = new List<PersonName>();
            ExpectedResult.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Nishanth" } });
            ExpectedResult.Add(new PersonName { LastName = "Krishnamurthy", GivenName = new List<string> { "Sumathi" } });
            ExpectedResult.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Modini" } });
            ExpectedResult.Add(new PersonName { LastName = "Srinivasan", GivenName = new List<string> { "Shashikala" } });

            PersonNameListComparer pnc = new PersonNameListComparer();

            bool equalresults = pnc.Equals(ExpectedResult, sortedNames);

            Assert.IsTrue(equalresults);
        }


        public class PersonNameListComparer : IEqualityComparer<List<PersonName>>
        {

            public bool Equals(List<PersonName> x, List<PersonName> y)
            {
                if (x.Count != y.Count)
                    return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i].LastName.ToLower() == y[i].LastName.ToLower() && x[i].GivenName.SequenceEqual(y[i].GivenName))
                        continue;
                    else
                        return false;
                }
                return true;
            }

            public int GetHashCode(List<PersonName> obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
