# DotPrimitives.Collections
A collection primitives library for .NET that adds new Collection types and making it easier to work with existing ones (like IGrouping<TKey, TElement>).

## Primitives Included

* `CachedEnumerable` - Caches an `IEnumerable`'s values for inexpensive re-use when required, and allows specifying when materialization should occur.
* `RefreshableCachedEnumerable` - Like `CachedEnumerable`, but allows cache invalidation and updating the cache.
* `GroupingEnumerable<TKey, TElement>` - Deferred grouping of a sequence by key without intermediate collections.
* `GroupingCollection<TKey, TElement>` - A read-only, enumerated collection of elements grouped by a common key.
* `FlexibleKeyValuePair<TKey, TValue>` - A key-value pair where the key is immutable but the value can be modified.

## Getting Started

### Compatibility
DotPrimitives.Collections supports a wide range of .NET versions:
* .NET Standard 2.0
* .NET 8
* .NET 9
* .NET 10

### Installation

You can install the library via [NuGet](https://nuget.org/packages/DotPrimitives.Collections) or using the .NET CLI.

```bash
dotnet add package DotPrimitives.Collections
```

## Usage

### CachedEnumerable
`CachedEnumerable` is useful when you want to ensure an `IEnumerable` is only evaluated once, even if it's requested multiple times.

```csharp
using DotPrimitives.Collections.Enumerables.Cached;

IEnumerable<int> slowSource = GetSlowEnumerable(); // e.g., from a database or API
CachedEnumerable<int> cached = new CachedEnumerable<int>(slowSource);

// First enumeration - evaluates slowSource
foreach (var item in cached) { /* ... */ }

// Subsequent enumerations - uses cached results without re-evaluating slowSource
foreach (var item in cached) { /* ... */ }
```

### RefreshableCachedEnumerable
`RefreshableCachedEnumerable` allows you to refresh the cache when the underlying data source has changed.

```csharp
using DotPrimitives.Collections.Enumerables.Cached;

var source = GetSource();
var refreshable = new RefreshableCachedEnumerable<int>(source);

// Use the cached data
foreach (var x in refreshable) { /* ... */ }

// Update the source and refresh the cache
source = GetUpdatedSource();
refreshable.RefreshCache(source);

// Subsequent enumerations will now use the new data
foreach (var x in refreshable) { /* ... */ }
```

### GroupingEnumerable
`GroupingEnumerable` provides a way to group elements from a sequence without creating intermediate collections, which is more memory-efficient for large datasets.

```csharp
using DotPrimitives.Collections.Groupings;

var items = new[] { ("a", 1), ("b", 2), ("a", 3) };
GroupingEnumerable<string,(string,int)> groups = new GroupingEnumerable<string, (string, int)>(
    items, item => item.Item1, item => item.Item2
);

foreach (var group in groups)
{
    Console.WriteLine($"Key: {group.Key}");
    foreach (var value in group) 
    {
        Console.WriteLine($"  {value}");
    }
}
```

## License
DotPrimitives.Collections is licensed under the MIT licence.

See `LICENSE.txt` for more information.

## Acknowledgements
Thanks to the following projects for their great work:

* [Polyfill](https://github.com/SimonCropp/Polyfill) for simplifying .NET Standard 2.0 support
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0
