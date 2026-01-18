/*
    MIT License
   
    Copyright (c) 2025 Alastair Lundy
   
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
   
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
   
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

using System.ComponentModel.DataAnnotations;

namespace DotPrimitives.Annotations;

/// <summary>
/// Marks an element as being Deprecated, with imminent or delayed removal in a future version of the software that currently contains the element.
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly |
                AttributeTargets.Class | AttributeTargets.Delegate |
                AttributeTargets.Constructor | AttributeTargets.Enum |
                AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface
                | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class DeprecatedAttribute : ValidationAttribute
{
    /// <summary>
    /// </summary>
    public static string DefaultDeprecationMessage => Resources.Attributes_Deprecations_Deprecated_FutureGeneric;
    
    /// <summary>
    /// 
    /// </summary>
    public string DeprecationMessage { get; private set; }
    
    /// <summary>
    /// The version in which the deprecated element will be removed.
    /// </summary>
    /// <remarks>Is null if not set by the developer user.</remarks>
    public string? DeprecationVersion { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DeprecatedAttribute()
    {
        DeprecationMessage = Resources.Attributes_Deprecations_Deprecated_FutureGeneric;
        DeprecationVersion = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="removalVersion"></param>
    public DeprecatedAttribute(string? removalVersion)
    {
        DeprecationMessage = 
                             Resources.Attributes_Deprecations_Deprecated_FutureGeneric;
        DeprecationVersion = removalVersion ?? null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deprecationMessage"></param>
    /// <param name="removalVersion"></param>
    public DeprecatedAttribute(string? deprecationMessage = null,
        string? removalVersion = null)
    {
        DeprecationVersion = removalVersion ?? null;
        
        if (DeprecationVersion is not null)
        {
            DeprecationMessage =
                Resources.Attributes_Deprecations_Deprecated_FutureSpecific.Replace("{x]", $"{DeprecationMessage}");
        }
        else
        {
            DeprecationMessage = deprecationMessage ??
                                 Resources.Attributes_Deprecations_Deprecated_FutureGeneric;
        }
    }
}