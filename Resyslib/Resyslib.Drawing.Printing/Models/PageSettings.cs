using System;

namespace Resyslib.Drawing.Printing.Models;

public class PageSettings : ICloneable
{
    public PageSettings()
    {
            
    }

    public PageSettings(PrinterSettings printerSettings)
    {
            
    }
        
    public object Clone()
    {
        throw new NotImplementedException();
    }
}