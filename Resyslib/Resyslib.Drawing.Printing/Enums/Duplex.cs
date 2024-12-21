/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
    
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums
{
    /// <summary>
    /// Specifies the printer's duplex setting.
    /// </summary>
    public enum Duplex
    {
        /// <summary>
        /// The printer's default duplex setting.
        /// </summary>
        Default = -1,
        /// <summary>
        /// Single-sided printing
        /// </summary>
        Simplex = 1,
        /// <summary>
        /// Double-sided vertical printing
        /// </summary>
        Vertical = 2,
        /// <summary>
        /// Double-sided horizontal printing
        /// </summary>
        Horizontal = 3
    }
}