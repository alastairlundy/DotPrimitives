/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.ComponentModel.DataAnnotations;
using AlastairLundy.DotPrimitives.Internal;

namespace AlastairLundy.DotPrimitives.Annotations.Attributes.Deprecations;

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
    public string DeprecationMessage { get; private set; }
    
    /// <summary>
    /// The version in which the deprecated element will be removed.
    /// </summary>
    /// <remarks>Is null if not set by the developer user.</remarks>
    public Version? DeprecationVersion { get; private set; }
    
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
    /// <param name="deprecationMessage"></param>
    public DeprecatedAttribute(string? deprecationMessage)
    {
        DeprecationMessage = deprecationMessage ??
                             Resources.Attributes_Deprecations_Deprecated_FutureGeneric;
        DeprecationVersion = null;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="deprecationMessage"></param>
    /// <param name="removalVersion"></param>
    public DeprecatedAttribute(string? deprecationMessage = null,
        Version? removalVersion = null)
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