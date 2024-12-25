/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0.
    Please see THIRD_PARTY_NOTICES.txt file for more details.

    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums;

/// <summary>
/// Specifies a Printer Resolution
/// </summary>
public enum PrinterResolutionKind
{
    /// <summary>
    /// High resolution.
    /// </summary>
    High = -4,
    /// <summary>
    /// Medium resolution.
    /// </summary>
    Medium = -3,
    /// <summary>
    /// Low resolution.
    /// </summary>
    Low = -2,
    /// <summary>
    /// Draft-quality resolution.
    /// </summary>
    Draft = -1,
    /// <summary>
    /// Custom resolution.
    /// </summary>
    Custom = 0
}