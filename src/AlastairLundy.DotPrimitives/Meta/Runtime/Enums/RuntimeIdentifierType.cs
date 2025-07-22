
namespace AlastairLundy.DotPrimitives.Meta.Runtime;

/// <summary>
/// The type of RuntimeIdentifier generated or detected.
/// </summary>
public enum RuntimeIdentifierType
{
    /// <summary>
    /// A Runtime Identifier that is valid for all architectures of an operating system.
    /// </summary>
    AnyGeneric,
    /// <summary>
    /// A Runtime Identifier that is valid for all supported versions of the OS being run.
    /// </summary>
    Generic,
    /// <summary>
    /// A Runtime Identifier that is valid for the specified OS and specified OS version being run.
    /// </summary>
    OsSpecific,
    /// <summary>
    /// 
    /// </summary>
    FullySpecific
}