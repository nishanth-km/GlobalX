﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalX
{
    public class ConsoleOutput : IConsoleOutput
    {
        public void ShowSortedList(List<PersonName> SortedPersonNames)
        {
            foreach (PersonName pn in SortedPersonNames)
            {
                String FullName = pn.LastName + " " + String.Join(" ", pn.GivenName);
                Console.WriteLine(FullName);
            }
        }
    }
    public class FileOutput: IConsoleOutput
    {
        public void ShowSortedList(List<PersonName> SortedPersonNames)
        {
            // this will do both console output and add into a file
            List<string> FullNamesList = new List<string>();
            foreach (PersonName pn in SortedPersonNames)
            {
                String FullName = pn.LastName + " " + String.Join(" ", pn.GivenName);
                Console.WriteLine(FullName);
                FullNamesList.Add(FullName);
            }
            string path = "";
            try
            {

                path = "/Users/micky/Desktop/sorted-names-list.txt";
                System.IO.File.WriteAllLines(path, FullNamesList.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while writing into line - incorrect path or no permission.")
            }
        }
    }
}