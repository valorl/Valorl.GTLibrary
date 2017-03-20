using FluentAssertions;
using System;
using Xunit;

namespace Valorl.GTLibrary.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 1;
            var y = 2;
            x.ShouldBeEquivalentTo(y);
        }
    }
}
