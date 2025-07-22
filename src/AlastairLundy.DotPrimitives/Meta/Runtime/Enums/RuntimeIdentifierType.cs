
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
    Specific,
    /// <summary>
    /// This is meant for Linux use only. DO NOT USE ON WINDOWS or macOS.
    /// </summary>
    DistroSpecific,
    /// <summary>
    /// This is meant for Linux use only. DO NOT USE ON WINDOWS or macOS.
    /// </summary>
    VersionLessDistroSpecific
}