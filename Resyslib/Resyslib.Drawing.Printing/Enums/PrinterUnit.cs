/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
        
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */


namespace Resyslib.Drawing.Printing.Enums
{
    /// <summary>
    /// Specifies several of the units of measure used for printing.
    /// </summary>
    public enum PrinterUnit
    {
        /// <summary>
        /// The default unit (0.01 in.).
        /// </summary>
        Display = 0,
        /// <summary>
        /// One-thousandth of an inch (0.001 in.).
        /// </summary>
        ThousandthsOfAnInch = 1,
        /// <summary>
        /// One-hundredth of a millimeter (0.01 mm).
        /// </summary>
        HundredthsOfAMillimeter = 2,
        /// <summary>
        /// One-tenth of a millimeter (0.1 mm).
        /// </summary>
        TenthsOfAMillimeter = 3
    }
}