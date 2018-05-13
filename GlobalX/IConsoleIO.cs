using System;
using System.Collections.Generic;

namespace GlobalX
{

    public interface IConsoleInputReader
    {
        List<PersonName> InputPeopleNames();
        INameSorter SortOrderType();
    }

    public interface IConsoleOutput
    {
        void ShowSortedList(List<PersonName> personNames);
    }
}
