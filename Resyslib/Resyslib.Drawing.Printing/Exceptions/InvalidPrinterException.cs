using System;
using Resyslib.Drawing.Printing.Models;

namespace Resyslib.Drawing.Printing.Exceptions;

/// <summary>
/// Represents the exception that is thrown when you try to access a printer using printer settings that are not valid.
/// </summary>
public class InvalidPrinterException : Exception
{
    public InvalidPrinterException(PrinterSettings printerSettings) : base()
    {
            
    }
}