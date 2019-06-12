using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace ChainLinq.Tests
{
    public class UnknownNodeTests
    {

        [Fact]
        public void FallbackOnCreate()
        {
            var query = new QueryBuilder<int>(() => new List<int>() { 1 }).Build();
            Assert.Single(query.Where(n => n != 0));
        }

        [Fact]
        public void FallbackOnExecute()
        {
            var query = new QueryBuilder<int>(() => new List<int>() { 1 }).Build();
            Assert.Equal(1, query.Count());
        }

        [Fact]
        public void UnhandledCreate()
        {
            var query = new QueryBuilder<int>().Build();
            Assert.Throws<UnhandledNodeException>(() => query.Where(n => n != 0));
        }

        [Fact]
        public void UnhandledExecute()
        {
            var query = new QueryBuilder<int>().Build();
            Assert.Throws<UnhandledNodeException>(() => query.Count());
        }
    }
}