using System;
using Resyslib.Drawing.Printing.Enums;

namespace Resyslib.Drawing.Printing.Models
{
    public class PaperSize : IEquatable<PaperSize>
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public PaperKind Kind { get; }

        public string PaperName { get; set; }

        public int RawKind { get; set; }

        
        
        public override bool Equals(object obj)
        {
            if (obj is PaperSize paperSize)
            {
                return Equals(paperSize);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(PaperSize other)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                
            }
        }
    }
}