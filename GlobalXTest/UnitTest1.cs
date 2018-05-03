using System;
using Xunit;
using GlobalX;

namespace GlobalXTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ConsoleFileLocationInputReader cfir = new ConsoleFileLocationInputReader();
            cfir.InputPeopleNames();
        }
    }
}
