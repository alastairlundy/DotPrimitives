# DotPrimitives
A C# primitives library that adds new types and features for working with strings, IO, and more.

## Primitives Included 

### DotPrimitives
* `DateSpan` - Represents a span of time in days, months, and years.
* `LineEndingDetector` - A utility for detecting line endings in strings and files.
* `PathEnvironmentVariable` - A utility for interacting with the system's PATH and PATHEXT environment variables.
* `WindowsFilePermissionManager` - A utility for managing file and directory permissions on Windows.

## Getting Started

### Compatibility
DotPrimitives supports a wide range of .NET versions:
* .NET Standard 2.0
* .NET 8
* .NET 9
* .NET 10

### Installation

You can install the library via [NuGet](https://nuget.org/packages/DotPrimitives) or using the .NET CLI.

To install the core library:
```bash
dotnet add package DotPrimitives
```

To install the collections library:
```bash
dotnet add package DotPrimitives.Collections
```

## Usage

### DateSpan
`DateSpan` allows you to represent and manipulate time spans in terms of years, months, and days.

```csharp
using DotPrimitives.Dates;

// Create a DateSpan of 1 year, 2 months, and 5 days
DateSpan span = new DateSpan(5, 2, 1);

// Calculate the difference between two dates
DateSpan diff = DateSpan.Difference(DateTime.Now, new DateTime(2020, 1, 1));
```

### LineEndingDetector
Detect the line ending format used in a string.

```csharp
using DotPrimitives.Text;

string text = "Line 1\r\nLine 2";
LineEndingFormat format = text.GetLineEndingFormat();

if (format == LineEndingFormat.CR_LF)
{
    // ...
}
```

## License
DotPrimitives is licensed under the MIT licence.

See `LICENSE.txt` for more information.

## Acknowledgements
Thanks to the following projects for their great work:

* [Polyfill](https://github.com/SimonCropp/Polyfill) for simplifying .NET Standard 2.0 support
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0
