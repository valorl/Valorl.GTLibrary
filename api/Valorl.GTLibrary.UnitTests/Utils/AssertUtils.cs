using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Valorl.GTLibrary.UnitTests.Utils
{
    public static class AssertUtils
    {
        public static void AggregateMultiple(params Action[] asserts)
        {
            var exceptions = new List<XunitException>();
            foreach (var action in asserts)
            {
                try
                {
                    action();
                }
                catch (XunitException e)
                {
                    exceptions.Add(e);
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateAssertsException(exceptions);
            }
        }
    }

    public class AggregateAssertsException : AggregateException
    {
        public AggregateAssertsException(IEnumerable<Exception> exceptions) : base(exceptions)
        {
        }

        public override string Message => "One or more assertions failed";
    }

}
