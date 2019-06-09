using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq;
using ChainLinq.Core;
using ChainLinq.Visitors;
using Xunit;

public class CoreTests{

    [Fact]
    public void ShouldDefault(){
         var values = new List<int>{1,2,3,4,5,6,7,8,9,10};
         var builder = new QueryBuilder();

         Assert.Equal(values, builder.Build(() => values.AsQueryable()));
    }

    [Fact]
    public void QueryReplacer(){
        var values = new List<int>{1,2,3,4,5,6,7,8,9,10};
        var builder = new QueryBuilder();

        int skip = 0;
        builder.Add(new Skip(v => {
            skip = v;
        }));

        var query = builder.Build(() => new List<int>());
        query.Skip(5).Where(t => true).ToList();

        Assert.Equal(5, skip);
    }

    [Fact]
    public void CanReact(){
        var values = new List<int>{1,2,3,4,5,6,7,8,9,10};
        var builder = new QueryBuilder();

        builder.Add(new Skip(v => {
            values = new List<int>();
        }));

        var query = builder.Build(() => new List<int>().AsQueryable()).Skip(5);
        Assert.Empty(query);
    }
}