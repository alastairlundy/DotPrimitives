/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AlastairLundy.Resyslib.Internal.Localizations;

// ReSharper disable RedundantBoolCompare
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.Annotations.Arguments;

/// <summary>
/// An attribute to specify allowed parameter argument values for a parameter.
/// </summary>
/// <remarks>This attribute will validate the parameter value against the array of allowed values.</remarks>
[AttributeUsage(validOn: AttributeTargets.Parameter & AttributeTargets.GenericParameter, AllowMultiple = true)]
public class AllowedParameterValuesAttribute : ValidationAttribute
{
    private readonly object[] _allowedValues;

    /// <summary>
    /// The allowed values that the parameter must be set to.
    /// </summary>
    public object[] AllowedValues => _allowedValues;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="allowedValues"></param>
    public AllowedParameterValuesAttribute(object[] allowedValues) : base(Resources.Errors_Attributes_AllowedValues)
    {
        _allowedValues = allowedValues;
        allowedValues.CopyTo(_allowedValues, 0);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="allowedValues"></param>
    /// <param name="errorMessage"></param>
    public AllowedParameterValuesAttribute(object[] allowedValues, string errorMessage) : base(errorMessage)
    {
        _allowedValues = allowedValues;
        allowedValues.CopyTo(_allowedValues, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        bool isValid = false;

        if (value != null && value.Equals(validationContext.ObjectInstance))
        {
            isValid = AllowedValues.Any(x => x.Equals(value));
        }

        if (isValid == true)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(ErrorMessage);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns>True if the parameter's value is one of the </returns>
    public override bool IsValid(object? value)
    {
        bool isValid = false;

        if (value != null)
        {
            isValid = AllowedValues.Any(x => x.Equals(value));
        }

        return isValid;
    }
}