# DotPrimitives.Collections
A collection primitives/utilities library for .NET.

Supported TFMs: .NET Standard 2.0, .NET 8.0, .NET 9.0, and .NET 10

## Features

- **CachedEnumerable**  
  Caches an IEnumerable's values for inexpensive re-use when required.

- **RefreshableCachedEnumerable**  
  Like `CachedEnumerable`, but allows cache invalidation, along with the ability to update the cache.

- **GroupEnumerable<TKey, TElement>**  
  Deferred grouping of a sequence by key without intermediate collections

## Getting Started

Install via NuGet:

```bash
dotnet add package DotPrimitives.Collections
```

### Usage Examples

```csharp
using DotPrimitives.Collections.Groupings;
using DotPrimitives.Collections.HashMaps;
using DotPrimitives.Collections.CachedEnumerables;


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

// GroupByEnumerable
var items = new[] { ("a", 1), ("b", 2), ("a", 3) };
var groups = new GroupEnumerable<string, (string, int)>(
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

* Polyfill for simplifying .NET Standard 2.0 support
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0
