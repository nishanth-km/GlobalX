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
        public void InputReaderTest()
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
        public void NameSorterTest()
        {
            NameSorter_Linq nsl = new NameSorter_Linq();


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
