using System.Collections.Generic;
using System.Linq;
using ChainLinq.Visitors;
using Xunit;

namespace ChainLinq.Tests
{
    public class FirstOrDefaultTests
    {
        class TestObj
        {
            public int Value { get; set; }
        }

        [Fact]
        public void ShouldCallback()
        {
            var builder = new QueryBuilder<TestObj>();

            int val = 0;
            builder.Add(LinqMethods.FirstOrDefault(new Equals<TestObj, int>(p => p.Value, value => val = value)));

            var query = builder.Build().FirstOrDefault(v => v.Value == 5);

            Assert.Equal(5, val);
        }
    }
}