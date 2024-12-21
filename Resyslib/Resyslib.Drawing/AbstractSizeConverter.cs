using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Resyslib.Drawing;

public abstract class AbstractSizeConverter : TypeConverter
{
    public new abstract bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
    public new abstract bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
    
    public abstract bool CanConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
    public abstract bool CanConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value);
    
    public new abstract object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues);
    
    public new abstract PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes);

    public new abstract bool GetPropertiesSupported(ITypeDescriptorContext context);
}