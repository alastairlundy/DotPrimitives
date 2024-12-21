/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
    
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums
{
    /// <summary>
    /// Specifies the type of print operation occurring.
    /// </summary>
    public enum PrintAction
    {
        /// <summary>
        /// The print operation is printing to a file.
        /// </summary>
        PrintToFile = 0,
        /// <summary>
        /// The print operation is a print preview.
        /// </summary>
        PrintToPreview = 1,
        /// <summary>
        /// The print operation is printing to a printer.
        /// </summary>
        PrintToPrinter = 2
    }
}