# AlastairLundy.Primitives
My C# primitives library for adding new types and features.

## Primitives Included 
Some primitives added include:

### Process Primitives

#### Policy Types
* ``ProcessResourcerPolicy`` - A policy 
* ``ProcessTimeoutPolicy`` - A policy to allow configuring process timeout. 

#### Result Types
* ``ProcessResult`` - For basic process result information
* ``BufferedProcessResult`` - String copies of Standard Output and Standard Error + basic process result information.
* ``PipedProcessResult`` - Process result information + Standard output and Standard Error pipes for more advanced piping scenarios.

#### Other Primitives
* ``ProcessResultValidation`` - An enum representing whether process result validation should be performed or not.
* ``ProcessConfiguration`` - A model class for representing Process configurations.


## Getting Started

### Support
This can be added to any .NET Standard 2.0, .NET Standard 2.1, .NET 8, or .NET 9 supported project.

### Pre-requisites

### Installation
* [Nuget](https://nuget.org/packages/AlastairLundy.Primitives) or ``dotnet add package AlastairLundy.Primitives``


## Usage


## License
AlastairLundy.Primitives is licensed under the MPL 2.0 license.

See ``LICENSE.txt`` for more information.

## Acknowledgements
Thanks to the following projects for their great work:

* Polyfill for simplifying .NET Standard 2.0 & 2.1 support
* Microsoft's [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) package for .NET Standard - This is used to enable .NET Standard 2.0 and 2.1 support on AlastairLundy.Primitives's attributes.
* Microsoft's [Microsoft.Bcl.HashCode](https://github.com/dotnet/maintenance-packages) for providing a backport of the HashCode class and static methods to .NET Standard 2.0