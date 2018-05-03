using System;
using System.Collections.Generic;

namespace GlobalX
{
    public interface INameSorter
    {
        List<PersonName> SortNames(List<PersonName> personNames);
    }
}
