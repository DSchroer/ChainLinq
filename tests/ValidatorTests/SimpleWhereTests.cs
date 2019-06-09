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
            var builder = new QueryBuilder();

            builder.Add(new SimpleWhere<TestObj, int>(v => v.Value, value => {

            }));

            var query = builder.Build(() => new List<TestObj>().AsQueryable())
                .Where(v => v.Value == 5).ToList();
        }
    }
}