using System;
using System.Collections.Generic;

namespace GlobalX
{
    
    public interface IConsoleInputReader
    {
        List<PersonName> InputPeopleNames();
    }

    public interface IConsoleOutput
    {
        void ShowSortedList(List<PersonName> personNames);
    }
}
