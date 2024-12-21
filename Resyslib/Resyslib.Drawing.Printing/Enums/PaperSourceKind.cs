/*
    Comments, Enum Names, and Enum values by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
    
    Comments in this file have replaced references to a paper "bin" with a paper "tray" for clarification purposes.
    
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

namespace Resyslib.Drawing.Printing.Enums
{
    /// <summary>
    /// Standard paper sources.
    /// </summary>
    public enum PaperSourceKind
    {
        /// <summary>
        /// The upper tray of a printer (or the default tray, if the printer only has one tray).
        /// </summary>
        Upper = 1,
        /// <summary>
        /// The lower tray of a printer.
        /// </summary>
        Lower = 2,
        /// <summary>
        /// The middle tray of a printer.
        /// </summary>
        Middle = 3,
        /// <summary>
        /// Manually fed paper.
        /// </summary>
        Manual = 4,
        /// <summary>
        /// An envelope.
        /// </summary>
        Envelope = 5,
        /// <summary>
        /// Manually fed envelope.
        /// </summary>
        ManualFeed = 6,
        /// <summary>
        /// Automatically fed paper.
        /// </summary>
        AutomaticFeed = 7,
        /// <summary>
        /// A tractor feed.
        /// </summary>
        TractorFeed = 8,
        /// <summary>
        /// Small-format paper.
        /// </summary>
        SmallFormat = 9,
        /// <summary>
        /// Large-format paper.
        /// </summary>
        LargeFormat = 10,
        /// <summary>
        /// The printer's large-capacity tray.
        /// </summary>
        LargeCapacity = 11,
        /// <summary>
        /// A paper cassette.
        /// </summary>
        Cassette = 14,
        /// <summary>
        /// The printer's default input tray.
        /// </summary>
        FormSource = 15,
        /// <summary>
        /// A printer-specific paper source.
        /// </summary>
        Custom = 257
    }
}