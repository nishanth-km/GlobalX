using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlobalX
{
    public class ConsoleInputReader : IConsoleInputReader
    {
        private int InputPeopleCount()
        {
            Console.WriteLine("Enter the number of people in the list");
            string input = Console.ReadLine();
            int PersonCount;

            while (true)
            {
                try
                {
                    PersonCount = Convert.ToInt32(input);
                    if (PersonCount < 0)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The Number of people has to be a number greater than 0");
                }
            }
            return PersonCount;
        }

        public List<PersonName> InputPeopleNames()
        {
            List<PersonName> InputNameList = new List<PersonName>();
            string input;
            PersonName personName = new PersonName();
            int personCount = InputPeopleCount();
            while (personCount > 0)
            {
                Console.WriteLine("Enter Last Name");
                input = Console.ReadLine();
                while (true)
                {
                    bool inputverified = VerifyInputNameString(input);
                    if (inputverified == false || string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Name string can only have String Literals. Please enter the Last name again.");
                        input = Console.ReadLine();
                    }
                    else
                    {
                        personName.LastName = input;
                        break;
                    }
                }
                Console.WriteLine("Enter Given Name");
                input = Console.ReadLine();
                while (true)
                {
                    bool inputverified = VerifyInputNameString(input);
                    if (string.IsNullOrWhiteSpace(input) && personName.GivenName.Count == 0)
                    {
                        Console.WriteLine("Atleast 1 given name is required. PLease Enter a given name");
                    }
                    else if (string.IsNullOrWhiteSpace(input) && personName.GivenName.Count > 0)
                    {
                        break;
                    }
                    else if (inputverified == false)
                    {
                        Console.WriteLine("Name string can only have String Literals. Please enter the Last name again.");
                        input = Console.ReadLine();
                    }
                    else
                    {
                        personName.GivenName.Add(input);
                    }
                }
                if (string.IsNullOrWhiteSpace(personName.LastName) == false && personName.GivenName.Count > 0)
                {
                    InputNameList.Add(personName);
                    personCount--;
                }
            }
            return InputNameList;
        }
        public bool VerifyInputNameString(string inputName)
        {
            string pattern = @"[^a-zA-Z]+";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = rgx.Match(inputName);
            return !match.Success;

        }
    }

    public class ConsoleFileLocationInputReader : IConsoleInputReader
    {
        public List<PersonName> InputPeopleNames()
        {
            Console.WriteLine("Enter the location where the file is located along with the filename and extension. Eg:- ");
            string path = Console.ReadLine();
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
                if (allnames.Length > 1)
                {
                    person.LastName = allnames[0];
                    for (int i = 1; i < allnames.Length; i++)
                    {
                        person.GivenName.Add(allnames[i]);
                    }
                }
                else if (allnames.Length == 1)
                    person.LastName = allnames[0];
                else
                    continue;
                PeopleNames.Add(person);
            }
            return PeopleNames;
        }
    }
}