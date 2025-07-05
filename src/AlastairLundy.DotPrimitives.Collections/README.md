# DotPrimitives.Collections
A collection primitives/utilities library for .NET.

Supported TFMs: .NET Standard 2.0, .NET Standard 2.1, .NET 8.0, .NET 9.0

## Features

- **CachedEnumerable**  
  Caches an IEnumerable's values for inexpensive re-use when required.

- **RefreshableCachedEnumerable**  
  Like `CachedEnumerable`, but allows cache invalidation, along with the ability to update the cache.

- **HashMap<TKey, TValue>**  
  A type that provides an API similar to Java's HashMap but uses ``Dictionary<TKey,TValue>`` under the hood.

- **GroupByEnumerable<TKey, TElement>**  
  Deferred grouping of a sequence by key without intermediate collections.

## Getting Started

Install via NuGet:

```
dotnet add package AlastairLundy.DotPrimitives.Collections
```

### Usage Examples

```csharp
using AlastairLundy.DotPrimitives.Collections.Groupings;
using AlastairLundy.DotPrimitives.Collections.HashMaps;
using AlastairLundy.DotPrimitives.Collections.CachedEnumerables;


// CachedEnumerable
var source = Enumerable.Range(1, 5);
var cached = new CachedEnumerable<int>(source);
foreach (var x in cached) Console.WriteLine(x);  // Source is enumerated once
foreach (var x in cached) Console.WriteLine(x);  // Call the cached results without enumerating the IEnumerable again!

// RefreshableCachedEnumerable
var refreshable = new RefreshableCachedEnumerable<int>(source);

source = source.Select(x => x + 1);
// Refresh the cache when the IEnumerable<T> contents changes.
refreshable.RefreshCache(source);

// Then call the cached results whilst avoiding multiple enumeration!
foreach (var x in refreshable) Console.WriteLine(x);

// HashMap
var map = new HashMap<string, int>();
map.Put("apple", 3);
map.GetValueOrDefault("apple", 0);

// GroupByEnumerable
var items = new[] { ("a", 1), ("b", 2), ("a", 3) };
var groups = new GroupByEnumerable<string, (string, int)>(
    items, item => item.Item1, item => item.Item2
);
foreach (var group in groups)
{
    Console.WriteLine($"Key: {group.Key}");
    foreach (var value in group) Console.WriteLine($"  {value}");
}
```

## License

This project is licensed under the Mozilla Public License 2.0.  
See [the LICENSE text](https://www.mozilla.org/en-US/MPL/2.0/) for more details.

## Acknowledgements
Thanks to the following projects for their great work:

* Polyfill for simplifying .NET Standard 2.0 & 2.1 support
* Microsoft's [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) package for .NET Standard - This is used to enable .NET Standard 2.0 and 2.1 support on AlastairLundy.DotPrimitives's attributes.
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0
