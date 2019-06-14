# ChainLinq
ChainLinq is a library for building custom Linq adapters for anything that you may need. 

This library is made to allow any project to take advantage of the power in `IQyeryable<T>`. Making it easy to implement custom query optimizations and logic with only a `IQueryable` interface. 

The goals of the project are that it should be:
* Easy to extend
* Configurable
* Simple

## Installation

You can grab the latest build of ChainLinq from Nuget here:

## Usage

To create a new `IQueryable<T>`, create a `QueryBuilder` object. Then you can setup its behavior and finalize it by calling `Build`.

```C#
// Create the query builder that is built around a list of ints
var builder = new QueryBuilder<int>(() => new List<int>{ 1, 2, 3 });
// Call build to create a IQueryable<int>
return builder.Build();
```

### Building IQueryables

Here are some examples of building custom `IQueryables`. For more you can look at the test files in the project.

#### Paging
Lets assume that I have a method like `IEnumerable<object> LoadData(int skip, int take)` that makes a query to a server somewhere and returns results. Since Linq already has `Skip` and `Take` methods we can bind them to callbacks and pass the values to our method.
```C#
var toSkip = 0;
var toTake = 100;

var builder = new QueryBuilder<object>(() => LoadData(toSkip, toTake));
builder.Skip(s => toSkip = s); // Bind the Skip callback
builder.Take(t => toTake = t); // Bind the Take callback

var queryable = builder.Build();
// Make our query. Note that LoadData will be called with values 1 and 10  
var data = queryable.Skip(1).Take(10).ToList();
```

#### Querying

```C#
var data = new Dictionary<string, MyData>();

string key = null;
var builder = new QueryBuilder<MyData>(() => {
    // Faster lookup if the property queried is Id
    if(key != null && data.ContainsKey(key)){
        return data[key];
    }else{
        return data.Values;
    }
});
// Callback for Where(d => d.Id == SOMETHING)
builder.Where(new Equals<MyData, string>(d => d.Id, k => key = k));

var queryable = builder.Build();

// Will callback and set the key variable to "myId"
var data = queryable.Where(d => d.Id == "myId").First();
```

### Unknown Linq Nodes
Sometimes you may not know all of the ways that your `IQueryable` will be used. To handle this the constructor argument of the `QueryBuilder` defines its behavior. 

If you do not specify any arguments then the `IQueryable` will throw an exception once any unhandled expressions are encountered.

```C#
var queryable = new QueryBuilder<int>().Build();
// This will fail since Where was not accounted for
var data = queryable.Where(n => n > 1); // Throws UnhandledNodeException
```

If you specify a starting parameter, the queryable will fallback to this value. This will stop all custom behavior of your `IQueryable` andd fallback to regular Linq.
```C#
var queryable = new QueryBuilder<int>(() => new List<int>{ 1, 2, 3 }).Build();
// This will now fallback to querying the List
var data = queryable.Where(n => n > 1); // Returns [ 2, 3 ]
```

### Extending The QueryBuilder 

To extend the QueryBuilders functionality you can create a new class that inherits from `ILinqNode`. There are two callback methods that will be called when new Linq members are created or the expression is executed.

You may also add new fallback behaviors by passing a object that extends `Behavior` into the QueryBuilder constructor. 

```C#
var builder = new QueryBuilder<int>( YourCustomBehavior )
builder.Add( YourCustomLinqNode );
var queryable = builder.Build();
```

## Roadmap

* Add support for OrderBy and other property based Linq methods
* Add support for First, FirstOrDefault and other terminating Linq methods
* Add support for Select, SelectMany and other projecting methods
* Integrating with other query providers like LinqToSQL