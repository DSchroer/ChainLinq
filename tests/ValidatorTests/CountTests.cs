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

        [Fact]
        public void ShouldGiveWrongCount(){
            var builder = new QueryBuilder<int>(() => new List<int>(){1,2,3,4,5});

            var query = builder.Build();
            Assert.Equal(5, query.Count());
        }
    }
}