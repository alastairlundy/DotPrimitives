using System;
using System.Collections.ObjectModel;
using Resyslib.Drawing.Printing.Enums;

namespace Resyslib.Drawing.Printing.Models
{
    public class PrinterSettings : ICloneable, IEquatable<PrinterSettings>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool CanDuplex { get; set; }
        
        public bool Collate { get; set; }
        
        public PageSettings DefaultPageSettings { get; set; }
        
        public Duplex Duplex { get; set; }

        public int FromPage { get; set; }

        public Collection<string> InstalledPrinters { get; }
        
        public bool IsDefaultPrinter { get; }
        
        public bool IsPlotter { get; }
        
        public bool IsValid { get; }
        
        public int MaximumCopies { get; }
        
        public int MinimumPage { get; set; }
        
        public PaperSizesCollection PaperSizes { get; }
        
        public PaperSourcesCollection PaperSources { get; }

        public string PrinterName { get; set; }
        
        public PrinterResolutionCollection PrinterResolutions { get; }

        public string PrintFileName { get; set; }
        
        public bool PrintToFile { get; set; }
        
        public bool SupportsColor { get; }
        
        public int ToPage { get; set; }
        
        public PrintRange PrintRange { get; set; }
        
        public PrinterSettings()
        {
            
        }
        
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool Equals(PrinterSettings other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is PrinterSettings printerSettings)
            {
                return Equals(printerSettings);
            }
            else
            {
                return false;
            }
        }
    }
}