/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0.
    Please see THIRD_PARTY_NOTICES.txt file for more details.

    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums;

/// <summary>
/// Specifies the part of the document to print.
/// </summary>
public enum PrintRange
{
    /// <summary>
    /// All pages are printed.
    /// </summary>
    AllPages = 0,
    /// <summary>
    /// The selected pages are printed.
    /// </summary>
    Selection = 1,
    /// <summary>
    /// The pages between FromPage and ToPage are printed.
    /// </summary>
    SomePages = 2,
    /// <summary>
    /// The currently displayed page is printed.
    /// </summary>
    CurrentPage = 4194304,
}