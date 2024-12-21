/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
    
    Comments in this file been altered to remove typos and remove subset information.
    
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums
{
    public enum PrintingPermissionLevel
    {
        /// <summary>
        /// Prevents access to printers.
        /// </summary>
        NoPrinting = 0,
        /// <summary>
        /// Provides printing only from a restricted dialog box.
        /// </summary>
        SafePrinting = 1,
        /// <summary>
        /// Provides printing programmatically to the default printer, along with safe printing through semi-restricted dialog box.
        /// </summary>
        DefaultPrinting = 2,
        /// <summary>
        /// Provides full access to all printers.
        /// </summary>
        AllPrinting = 3,
    }
}