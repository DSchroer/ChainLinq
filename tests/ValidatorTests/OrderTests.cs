using Xunit;
using System.Linq;
using ChainLinq.Visitors;

namespace ChainLinq.Tests
{
    public class OrderTests
    {
        private class TestObj
        {
            public string Data { get; set; }
            public TestObj Sub { get; set; }
        }

        [Fact]
        public void CallbackTest()
        {
            var hits = 0;
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => hits++);
            builder.Build().OrderBy(o => o.Data);

            Assert.Equal(1, hits);
        }

        [Fact]
        public void ThenByCallbackTest()
        {
            var hits = 0;
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => {});
            builder.ThenBy(o => hits++);
            builder.Build().OrderBy(o => o.Data).ThenBy(o => o.Sub);

            Assert.Equal(1, hits);
        }

        [Fact]
        public void PropertyNameTest()
        {
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => Assert.Equal(nameof(TestObj.Data), o.PropertyName));
            builder.Build().OrderBy(o => o.Data);
        }

        [Fact]
        public void AscendingTest()
        {
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => Assert.Equal(OrderDirection.Ascending, o.Direction));
            builder.Build().OrderBy(o => o.Data);
        }

        [Fact]
        public void DescendingTest()
        {
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => Assert.Equal(OrderDirection.Descending, o.Direction));
            builder.Build().OrderByDescending(o => o.Data);
        }

        [Fact]
        public void SupportsSubNames(){
            var builder = new QueryBuilder<TestObj>();

            builder.OrderBy(o => Assert.Equal("Sub.Data", o.PropertyName));
            builder.Build().OrderByDescending(o => o.Sub.Data);
        }
    }
}