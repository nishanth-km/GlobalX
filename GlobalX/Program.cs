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
            NameSorterProgram sorterProgram = new NameSorterProgram(new ConsoleFileLocationInputReader(), new FileOutput());
            sorterProgram.RunProgram();
        }
    }

    public class NameSorterProgram
    {
        INameSorter _nameSorter;
        IConsoleInputReader _consoleInputReader;
        IConsoleOutput _consoleOutput;

        List<PersonName> personNames;

        public NameSorterProgram(IConsoleInputReader consoleInputReader, IConsoleOutput consoleOutput)
        {
            _consoleInputReader = consoleInputReader;
            _consoleOutput = consoleOutput;
        }

        public void RunProgram()
        {
            personNames = _consoleInputReader.InputPeopleNames();
            _nameSorter = _consoleInputReader.SortOrderType();
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

    public class NameSorterAsc_LastNameGivenNames : INameSorter
    {
        public List<PersonName> SortNames(List<PersonName> personNames)
        {
            List<PersonName> sortedPersonNames = new List<PersonName>();

            sortedPersonNames = personNames.OrderBy(png => string.Join(",", png.GivenName)).OrderBy(pn => pn.LastName).ToList();

            return sortedPersonNames;
        }
    }
    public class NameSorterDesc_LastName : INameSorter
    {
        public List<PersonName> SortNames(List<PersonName> personNames)
        {
            List<PersonName> sortedPersonNames = new List<PersonName>();

            sortedPersonNames = personNames.OrderByDescending(pn => pn.LastName).ToList();

            return sortedPersonNames;
        }
    }

}
