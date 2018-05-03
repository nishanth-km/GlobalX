using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GlobalX
{
    class Program
    {
        static void Main(string[] args)
        {
            NameSorterProgram sorterProgram = new NameSorterProgram(new NameSorter_Linq(), new ConsoleFileLocationInputReader(), new FileOutput());
            sorterProgram.RunProgram();
        }
    }

    public class NameSorterProgram
    {
        INameSorter _nameSorter;
        IConsoleInputReader _consoleInputReader;
        IConsoleOutput _consoleOutput;

        List<PersonName> personNames;

        public NameSorterProgram(INameSorter nameSorter, IConsoleInputReader consoleInputReader, IConsoleOutput consoleOutput)
        {
            _nameSorter = nameSorter;
            _consoleInputReader = consoleInputReader;
            _consoleOutput = consoleOutput;
        }

        public void RunProgram()
        {
            personNames = _consoleInputReader.InputPeopleNames();
            List<PersonName> sortedNames = _nameSorter.SortNames(personNames);
            _consoleOutput.ShowSortedList(sortedNames);
        }

        public void SortNames(List<PersonName> personNames)
        {
            _nameSorter.SortNames(personNames);
        }
    }


    public class PersonName
    {
        public string LastName;
        public List<string> GivenName;
        public PersonName()
        {
            GivenName = new List<string>();
        }
    }

    public class NameSorter_Linq : INameSorter
    {
        public List<PersonName> SortNames(List<PersonName> personNames)
        {
            List<PersonName> sortedPersonNames = new List<PersonName>();
            List<string> LastNames = new List<string>();
            List<List<string>> GivenNames = new List<List<string>>();
            foreach(PersonName pn in personNames)
            {
                LastNames.Add(pn.LastName);
                GivenNames.Add(pn.GivenName);
            }
            LastNames.Sort();

            foreach (string lastname in LastNames.Distinct())
            {
                GivenNames = personNames.Where(p => p.LastName == lastname).Select(pn => pn.GivenName).ToList();
                GivenNames = GivenNames.OrderBy(p => String.Join(", ", p)).ToList();
                foreach(List<string> givenname in GivenNames)
                {
                    PersonName personName = new PersonName();
                    personName.GivenName = givenname;
                    personName.LastName = lastname;
                    sortedPersonNames.Add(personName);
                }
            }


            return sortedPersonNames;
        }
    }


}
