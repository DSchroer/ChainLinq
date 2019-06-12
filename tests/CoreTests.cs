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
         var builder = new QueryBuilder<int>(() => values);

         Assert.Equal(values, builder.Build());
    }

    [Fact]
    public void QueryReplacer(){
        var values = new List<int>{1,2,3,4,5,6,7,8,9,10};
        var builder = new QueryBuilder<int>(() => values);

        int skip = 0;
        builder.Add(LinqMethods.Skip(v => skip = v));

        var query = builder.Build();
        query.Skip(5).Where(t => true).ToList();

        Assert.Equal(5, skip);
    }

    [Fact]
    public void CanReact(){
        var values = new List<int>{1,2,3,4,5,6,7,8,9,10};
        var builder = new QueryBuilder<int>(() => values);

        builder.Add(LinqMethods.Skip(s => values = new List<int>()));

        var query = builder.Build().Skip(5);
        Assert.Empty(query);
    }

    [Fact]
    public void ShouldNotHandleUnknowns(){
        var query = new QueryBuilder<int>().Count(() => 5).Build();

        Assert.Throws<Exception>(() => query.Where(a => a > 5).Count());
    }
}