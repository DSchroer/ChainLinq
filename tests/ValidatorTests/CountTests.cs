using System.Collections.Generic;
using System.Linq;
using ChainLinq.Visitors;
using Xunit;

namespace ChainLinq.Tests
{
    public class CountTests
    {
        class TestObj
        {
            public int Value { get; set; }
        }

        [Fact]
        public void ShouldCount()
        {
            var builder = new QueryBuilder<TestObj>();

            builder.Add(LinqMethods.Count(() => 42));

            var count = builder.Build().Count();

            Assert.Equal(42, count);
        }
    }
}