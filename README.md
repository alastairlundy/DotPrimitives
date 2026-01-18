# DotPrimitives
My C# primitives library for adding new types and features.

## Primitives Included

### Added Types
* ``DateSpan`` - Represents a span of time as represented by days, months, or years, between two dates.
* ``WindowsFilePermission`` - Represents Windows file permissions.
* ``LineEndingFormat`` - Enum for representing line endings.
* ``DeperecatedAttribute`` - Attribute for marking types and members as deprecated for eventual removal.

### Static Helpers
* ``ExceptionThrower`` - Throws exceptions cleanly with or without a message.
* ``LineEndingDetector`` - Detect line endings in a string.
* ``StorageDrives`` - Detect physical and logical storage drives
* ``PathEnvironmentVariable`` - Retrieve path environment variables
* ``WindowsFilePermissionManager`` - Set or Get Windows file permissions for a file.

## Getting Started

### Support
This can be added to any .NET Standard 2.0, .NET 8, .NET 9, or .NET 10 supported project.

### Pre-requisites

### Installation
* [Nuget](https://nuget.org/packages/AlastairLundy.DotPrimitives) or ``dotnet add package DotPrimitives``


## Usage


## License
DotPrimitives is licensed under the MIT licence.

See ``LICENSE.txt`` for more information.

## Acknowledgements
Thanks to the following projects for their great work:

* [Polyfill](https://github.com/SimonCropp/Polyfill) for simplifying .NET Standard 2.0 support
* Microsoft's [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) package for .NET Standard - This is used to enable .NET Standard 2.0 support on DotPrimitives's attributes.
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0
