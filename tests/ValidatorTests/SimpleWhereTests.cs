using System.Collections.Generic;
using System.Linq;
using ChainLinq.Visitors;
using Xunit;

namespace ChainLinq.Tests
{
    public class SimpleWhereTests
    {
        class TestObj
        {
            public int Value { get; set; }
        }

        [Fact]
        public void ShouldCallback()
        {
            var builder = new QueryBuilder<TestObj>(() => new List<TestObj>());

            int val = 0;
            builder.Where(new Equals<TestObj, int>(v => v.Value, value => val = value));

            var query = builder
                .Build()
                .Where(v => v.Value == 5)
                .ToList();

            Assert.Equal(5, val);
        }

        [Fact]
        public void ShouldReverse()
        {
            var builder = new QueryBuilder<TestObj>(() => new List<TestObj>());

            int val = 0;
            builder.Where(new Equals<TestObj, int>(v => v.Value, value => val = value));

            var query = builder.Build().Where(v => 5 == v.Value).ToList();
            Assert.Equal(5, val);
        }
    }
}